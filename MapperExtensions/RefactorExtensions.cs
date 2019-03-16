using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;

namespace MapperExtensions.Models
{
    public class ConcatPropertyVisitor<TSource, TProjection, TDest> : ExpressionVisitor
    {
        private readonly Expression<Func<TSource, TProjection>> _projectionLambda;

        public ConcatPropertyVisitor(Expression<Func<TSource, TProjection>> projectionLambda)
        {
            _projectionLambda = projectionLambda;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node.Body is UnaryExpression unary && unary.Operand is ConditionalExpression conditional)
            {
                var newConditional = Helpers.ConcatConditionalExpressionAndLambda(conditional, _projectionLambda);
                var result1 =
                    Expression.Lambda<Func<TSource, object>>(newConditional, _projectionLambda.Parameters.First());
                return base.VisitLambda(result1);
            }

            var result = _projectionLambda.ConcatPropertyExpressionToLambda<TSource, object>(node);
            return base.VisitLambda(result);
        }
    }

    public static class RefactorExtensions
    {
        public static MapperExpressionWrapper<TSource, TDest, TProjection> From<TSource, TDest, TProjection>(
            this IMappingExpression<TSource, TDest> mapping, Expression<Func<TSource, TProjection>> expression)
            => new MapperExpressionWrapper<TSource, TDest, TProjection>(mapping, expression);

        public static IMappingExpression<TSource, TDest> FixRules<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            IEnumerable<(Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)> rules)
        {
            var rulesByConvention =
                Helpers.GetConventionMap<TSource, TDest, TProjection, Object>(mapperExpressionWrapper.Expression);

            var concatProjection = rules.Select(x =>
            {
                var (from, @for) = x;
                var t =
                    new ConcatPropertyVisitor<TSource, TProjection, TDest>(mapperExpressionWrapper.Expression)
                        .Visit(@for);
                var result =
                    (Expression<Func<TSource, object>>) t;
                return (from, result);
            }).ToList();

            var concatMapRules =
                concatProjection.LeftJoin(rulesByConvention, new ExpressionTupleComparer<TDest, TSource, object>());
            Register(mapperExpressionWrapper.MappingExpression, concatMapRules);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static MapperExpressionWrapper<TSource, TDest, TProjection> FixConditionalFormatRules<TSource,
            TProjection, TDest>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            Expression<Func<TDest, object>> mapExpression,
            Expression<Func<TProjection, bool>> Test,
            Expression<Func<TProjection, object>> ifTrue,
            Expression<Func<TProjection, object>> ifFalse)
        {
            var newTest =
                Test.ConcatPropertyExpressionToLambda<TSource, object>(mapperExpressionWrapper.Expression);
            var newIfTrue =
                ifTrue.ConcatPropertyExpressionToLambda<TSource, Object>(mapperExpressionWrapper.Expression);
            var newIfFalse =
                ifFalse.ConcatPropertyExpressionToLambda<TSource, object>(mapperExpressionWrapper.Expression);
            var exprConditional = Expression.Condition(newTest, newIfTrue, newIfFalse);
            var convertToObject = Expression.Convert(exprConditional, typeof(object));
            var lambda = Expression.Lambda<Func<TSource, object>>(convertToObject,
                mapperExpressionWrapper.Expression.Parameters.First());
            Register(mapperExpressionWrapper.MappingExpression, new[] {(mapExpression, lambda)});
            return mapperExpressionWrapper;
        }

        private static void Register<TSource, TDest, TProjection1>(
            IMappingExpression<TSource, TDest> MappingExpression,
            IEnumerable<(Expression<Func<TDest, TProjection1>>, Expression<Func<TSource, TProjection1>>)> expressions)
        {
            foreach (var expression in expressions)
            {
                var from = Expression.Lambda(expression.Item2.Body.ReplaceObjectConvert(),
                    expression.Item2.Parameters.First());
                var @for = expression.Item1.PropertiesStr().First();
                MappingExpression.ForMember(@for, s => s.MapFrom((dynamic) from));
                Console.WriteLine($"Registration for: {@for} from: {from}");
            }
        }
    }

    public static class Helpers
    {
        public static IEnumerable<(Expression<Func<TDest, TProjection1>>, Expression<Func<TSource, TProjection1>>)>
            GetConventionMap<TSource, TDest, TProjection, TProjection1>(
                Expression<Func<TSource, TProjection>> projectExpression)
        {
            var destProperties = typeof(TDest).GetProperties();
            var projectProperties = typeof(TProjection).GetProperties();
            var advancedMapRules = GetAdvancesConventionInfo(destProperties, projectProperties, projectExpression)
                .Where(x => x.PathToSourceProperty.All(xx => xx != null));
            var result = advancedMapRules
                .Select(x =>
                {
                    var @for = GetLambdaByPropertyNames<TDest, TProjection1>(new[] {x.DestinationPropertyName},
                        typeof(TDest));
                    var from = GetLambdaByPropertyNames<TSource, TProjection1>(x.PathToSourceProperty,
                        typeof(TSource));
                    return (@for, from);
                }).ToList();
            return result;
        }

        private static IEnumerable<MapPropertyInfo> GetSimpleConventionInfo<TSource, TProjection>(
            IEnumerable<PropertyInfo> destProperties, IEnumerable<PropertyInfo> projectProperties,
            Expression<Func<TSource, TProjection>> fromSourceToProjectionPath)
        {
            var sourceToProjections = fromSourceToProjectionPath?.PropertiesStr();
            var join = destProperties.Join(projectProperties, x => x.Name, x => x.Name, (x, y) => x.Name);
            var result = join.Select(x => new MapPropertyInfo(x, sourceToProjections?.Concat(new[] {x})));
            return result;
        }

        private static IEnumerable<MapPropertyInfo> GetAdvancesConventionInfo<TSource, TProjection>(
            IEnumerable<PropertyInfo> destProperties, IEnumerable<PropertyInfo> projectProperties,
            Expression<Func<TSource, TProjection>> fromSourceToProjectionPath)
        {
            var result = destProperties.Select(x =>
            {
                var result1 = AdvanceMapProperty(GetIEnumerableStringByUppLetter(x.Name), typeof(TProjection));
                return result1.All(xx => xx != null)
                    ? new MapPropertyInfo(x.Name, fromSourceToProjectionPath.PropertiesStr().Concat(result1))
                    : null;
            }).Where(x => x != null);


            return result;
        }

        public static IEnumerable<string> PropertiesStr(this LambdaExpression lambda)
        {
            var expression = ReplaceObjectConvert(lambda.Body);
            var result = expression.ToString().Split('.').Skip(1);
            return result;
        }

        public static Expression ReplaceObjectConvert(this Expression expression)
        {
            Expression result;
            //todo fix it

            switch (expression)
            {
                case UnaryExpression unary when unary.Operand is MemberExpression member:
                    result = member;
                    break;
                case MemberExpression member:
                    result = member;
                    break;
                case ParameterExpression parameter:
                    result = parameter;
                    break;
                case UnaryExpression unary when unary.Operand is ConditionalExpression conditional:
                    result = conditional;
                    break;
                default:
                    throw new ArgumentException();
            }

            return result;
        }

        public static MemberExpression MemberExpressionWithProjectionConcat(this MemberExpression e1,
            LambdaExpression projectionLambda)
        {
            var projectionProps = projectionLambda.Body.ToString().Split('.').Skip(1).ToList();
            var props = e1.ToString().Split('.').Skip(1).ToList();
            var allstrings = projectionProps.Concat(props);
            var t = projectionLambda.Parameters.First();
            var result =
                (MemberExpression) allstrings.Aggregate((Expression) t, Expression.Property);
            return result;
        }

        public static Expression<Func<T, R>> ConcatPropertyExpressionToLambda<T, R>(this LambdaExpression lambda1,
            LambdaExpression lambda2)
        {
            var parameterType = lambda1.Parameters.First().Type;
            var properties1 = lambda1.PropertiesStr();
            var properties2 = lambda2.PropertiesStr();
            var concatPropertiesName = properties1.Concat(properties2);
            var result = GetLambdaByPropertyNames<T, R>(concatPropertiesName, parameterType);
            return result;
        }


        public static Expression<Func<T, R>> GetLambdaByPropertyNames<T, R>(IEnumerable<string> properties,
            Type parameterType)
        {
            var parameter = Expression.Parameter(parameterType);

            var result = Expression.Lambda<Func<T, R>>(
                Expression.Convert(properties.Aggregate((Expression) parameter, Expression.Property),
                    typeof(object)), parameter);
            return result;
        }

        public static UnaryExpression ConcatConditionalExpressionAndLambda<TSource, R>(
            ConditionalExpression conditional, Expression<Func<TSource, R>> expression)
        {
            MemberExpression newLeft = null;
            MemberExpression newRight = null;
            MemberExpression newFalse = null;
            MemberExpression newTrue = null;
            BinaryExpression newTest = null;
            if (conditional.Test is BinaryExpression test)
            {
                if (test.Left is MemberExpression left)
                {
                    newLeft = left.MemberExpressionWithProjectionConcat(expression);
                }

                if (test.Right is MemberExpression right)
                {
                    newRight = right.MemberExpressionWithProjectionConcat(expression);
                }

                newTest = Expression.MakeBinary(test.NodeType, newLeft, newRight);
            }

            if (conditional.IfTrue is MemberExpression @true)
            {
                newTrue = @true.MemberExpressionWithProjectionConcat(expression);
            }

            if (conditional.IfFalse is MemberExpression @false)
            {
                newFalse = @false.MemberExpressionWithProjectionConcat(expression);
            }

            var result = Expression.Convert(Expression.Condition(newTest, newTrue, newFalse), typeof(object));

            return result;
        }


        public static IEnumerable<string> AdvanceMapProperty(IEnumerable<string> path, Type properType)
        {
            var properties = properType.GetProperties().ToList();
            var pathStr = path.Join("");
            foreach (var prop in properties)
            {
                if (!pathStr.StartsWith(prop.Name)) continue;
                var newPath = GetIEnumerableStringByUppLetter(pathStr.Remove(0, prop.Name.Length));
                if (!newPath.Any())
                    return new[] {prop.Name};
                var analyze = AdvanceMapProperty(newPath, prop.PropertyType);
                if (analyze.All(x => x != null))
                    return new[] {prop.Name}.Concat(analyze);
            }

            return new string[] {null};
        }

        private static IEnumerable<string> GetIEnumerableStringByUppLetter(string str)
        {
            return Regex.Matches(str, @"[A-Z][\w]+").Select(xx => xx.Value).ToList();
        }
    }

   
}