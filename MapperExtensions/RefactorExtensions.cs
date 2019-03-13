using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.Configuration.Conventions;

namespace MapperExtensions.Models
{
    public static class RefactorExtensions
    {
        public static MapperExpressionWrapper<TSource, TDest, TProjection> From<TSource, TDest, TProjection>(
            this IMappingExpression<TSource, TDest> mapping, Expression<Func<TSource, TProjection>> expression)
            => new MapperExpressionWrapper<TSource, TDest, TProjection>(mapping, expression);

//        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
//            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
//            params (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)[] rules)
//        {
//            var rulesByConvention =
//                Helpers.GetConventionMap<TSource, TDest, TProjection, Object>(mapperExpressionWrapper.Expression);
//            var concatProjection = rules.Select(x =>
//            {
//                var (from, @for) = x;
//                var result = mapperExpressionWrapper.Expression.ConcatPropertyExpressionToLambda<TSource, object>(@for);
//                return (from, result);
//            });
//            var concatMapRules =
//                concatProjection.LeftJoin(rulesByConvention, new ExpressionTupleComparer<TDest, TSource, object>());
//            Register(mapperExpressionWrapper.MappingExpression, concatMapRules);
//            return mapperExpressionWrapper.MappingExpression;
//        }


        public static void Register<TSource, TDest, TProjection1>(
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
            var simpleMapRules = GetSimpleConventionInfo(destProperties, projectProperties, projectExpression);
            var advancedMapRules = GetAdvancesConventionInfo(destProperties, projectProperties, projectExpression);
            var result = simpleMapRules.Concat(advancedMapRules)
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
            var sourceToProjections = fromSourceToProjectionPath?.PropertiesStr();
            var join = destProperties.Where(x =>
            {
                var propertyPath = Regex.Matches(x.Name, "[A-Z][a-z]+").Select(xx => xx.Value);
                var check = ContainsPropertyChecker(propertyPath, typeof(TProjection));
                return check;
            }).ToList();
            var result = join.Select(x =>
                new MapPropertyInfo(x.Name,
                    sourceToProjections?.Concat(Regex.Matches(x.Name, "[A-Z][a-z]+").Select(xx => xx.Value))));
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
                default:
                    throw new ArgumentException();
            }

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

        private static bool ContainsPropertyChecker(IEnumerable<string> propertyNames, Type property)
        {
            if (!propertyNames.Any())
                return true;
            var propertyType = property;
            if (propertyType.IsValueType)
                return false;
            var propertyName = propertyNames.First();
            var deepProperty = propertyType.GetProperties().SingleOrDefault(x => x.Name == propertyName);
            return deepProperty != null && ContainsPropertyChecker(propertyNames.Skip(1), deepProperty.PropertyType);
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> LeftJoin<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2,
            IEqualityComparer<T> equalityComparer)
        {
            var result = enumerable1.Union(enumerable2, equalityComparer).ToList();
            return result;
        }
    }

    public class ExpressionTupleComparer<TDest, TProjection, TProjection1>
        : IEqualityComparer<(Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>)>
    {
        public bool Equals((Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>) x,
            (Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>) y)
        {
            var equals = x.Item1.ToString() == y.Item1.ToString();
            return equals;
        }

        public int GetHashCode((Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>) obj)
        {
            var s = string.Join('.', obj.Item1.Body.ReplaceObjectConvert().ToString().Split('.').Skip(1));
            var hashCode = s.GetHashCode();
            return hashCode;
        }
    }

    public class MapPropertyInfo
    {
        public MapPropertyInfo(string destinationPropertyName, IEnumerable<string> pathToSourceProperty)
        {
            DestinationPropertyName = destinationPropertyName;
            PathToSourceProperty = pathToSourceProperty;
        }

        public string DestinationPropertyName { get; set; }
        public IEnumerable<string> PathToSourceProperty { get; set; }
    }
}