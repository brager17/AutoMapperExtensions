//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text.RegularExpressions;
//using System.Xml.Linq;
//using AutoMapper;
//using AutoMapper.Configuration;
//using static MapperExtensions.Models.AuthMapperExtensionHelpers;
//
//namespace MapperExtensions.Models
//{
//    public static class AutoMapperExtensions
//    {
//        private static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
//            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
//            params Expression<Func<TDest, object>>[] expressions)
//        {
//            var from = expressions.Select(expression => Concat(mapperExpressionWrapper.Expression, expression));
//            var @for = expressions.Select(GetPropertyNamesInLambda);
//            var zip = @for.Zip(from, (x, y) => new {For = x, From = y}).ToList();
//            foreach (var info in zip)
//            {
//                mapperExpressionWrapper.MappingExpression
//                    .ForMember(info.For, ss => ss.MapFrom((dynamic) info.From));
//            }
//
//            return mapperExpressionWrapper.MappingExpression;
//        }
//
//        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
//            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
//            => mapperExpressionWrapper.To(GetLambdasByConvention<TDest, TProjection>().ToArray());
//
//        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
//            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
//            params (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)[] props)
//        {
//            //переменные для которых определены правилы маппинга
//
//            ConcatAndRegister(mapperExpressionWrapper.Expression, mapperExpressionWrapper.MappingExpression, props);
//            //переменные которые называются одинаково
//            var lambdas = GetLambdasByConvention<TDest, TProjection>();
//            /*var s = lambdas.Select(expression => Concat(mapperExpressionWrapper.Expression, expression));
//            var zip = lambdas.Zip(s,(x,y)=>{})
//            ConcatAndRegister(mapperExpressionWrapper.Expression, mapperExpressionWrapper.MappingExpression,s);*/
//            // переменные которые состоят из нескольких слов CamelCase
//
//            return null;
//        }
//
//        public static MapperExpressionWrapper<TSource, TDest, TProjection> From<TSource, TDest, TProjection>(
//            this IMappingExpression<TSource, TDest> mapping, Expression<Func<TSource, TProjection>> expression)
//            => new MapperExpressionWrapper<TSource, TDest, TProjection>(mapping, expression);
//    }
//
//    //todo make internal, only tests 
//    public static class AuthMapperExtensionHelpers
//    {
//        public static IEnumerable<Expression<Func<TDest, object>>> GetLambdasByConvention<TDest, TProjection>()
//        {
//            var destProperties = typeof(TDest).GetProperties();
//            var projectionProperties = typeof(TProjection).GetProperties();
//            var join = destProperties.Join(projectionProperties, x => new {x.Name, x.PropertyType},
//                x => new {x.Name, x.PropertyType}, (x, y) => x).ToList();
//            if (!join.Any())
//            {
//                throw new ArgumentException("совпадений не найдено");
//            }
//
//            var parameter = Expression.Parameter(typeof(TDest));
//            var properties = join.Select(x => Expression.Property(parameter, x));
//            var lambdas = properties.Select(x =>
//                Expression.Lambda<Func<TDest, object>>(Expression.Convert(x, typeof(object)), parameter));
//            return lambdas;
//        }
//
//        public static LambdaExpression Concat<TSource, TDest, TProjection>(
//            Expression<Func<TSource, TProjection>> fromExpression,
//            Expression<Func<TDest, object>> concatExpression)
//        {
//            var propName = GetPropertyNamesInLambda(concatExpression);
//            var result = propName.Aggregate(fromExpression.Body,Expression.Property);
//            return Expression.Lambda(result, fromExpression.Parameters.First());
//        }
//
//        //simple version todo: maybe fix
//        public static LambdaExpression Concat<TSource, TProjection>(
//            Expression<Func<TSource, TProjection>> fromExpression,
//            LambdaExpression concatExpression)
//        {
//            var propName = GetPropertyNamesInLambda(concatExpression);
//            var result = propName.Aggregate(fromExpression.Body,Expression.Property);
//            return Expression.Lambda(result, fromExpression.Parameters.First());
//        }
//
//        public static IEnumerable<string> GetPropertyNamesInLambda(LambdaExpression lambda)
//        {
//            var expression = lambda.Body;
//            if (lambda.Body is UnaryExpression unaryExpression &&
//                unaryExpression.Operand is MemberExpression memberExpression)
//            {
//                expression = memberExpression;
//            }
//
//            var names = Regex.Matches(expression.ToString(), @"\w+.").Select(x => x.Value).ToList();
//            var propName = new List<string>();
//            while (lambda.Body is MemberExpression || lambda.Body is UnaryExpression)
//            {
//                switch (lambda.Body)
//                {
//                    case UnaryExpression unary when unary.Operand is MemberExpression member1:
//                        propName.Add(member1.Member.Name);
//                        break;
//                    case MemberExpression member:
//                        propName.Add(member.Member.Name);
//                        break;
//                }
//
//            }
//
//            return propName;
//        }
//
//        public static Expression<Func<TMap, object>> PropertyMapper<TMap>(string destPropertyName)
//        {
//            var propertyNames = Regex.Matches(destPropertyName, @"[A-Z][a-z]+").Select(x => x.Value);
//            var parameter = Expression.Parameter(typeof(TMap));
//
//            var concatProperty = propertyNames.Aggregate((Expression) parameter, (a, c) =>
//                a?.Type.GetProperty(c) is var property && property != null
//                    ? Expression.Property(a, property)
//                    : null);
//
//            if (concatProperty == null)
//                return null;
//            var expressionLambda = Expression.Lambda<Func<TMap, object>>(concatProperty, parameter);
//            return expressionLambda;
//        }
//
//        private static void RegisterByStringLambda<TSource, TDest>(
//            IMappingExpression<TSource, TDest> mappingExpression,
//            IEnumerable<(string, Expression<Func<TSource, object>>)> forFroms)
//        {
//            foreach (var (@for, from) in forFroms)
//            {
//                mappingExpression.ForMember(@for, ss => ss.MapFrom((dynamic) from));
//            }
//        }
//
//        public static void RegisterByLambdas<TSource, TDest>(
//            IMappingExpression<TSource, TDest> mappingExpression,
//            IEnumerable<(Expression<Func<TDest, object>>, Expression<Func<TSource, object>>)> forFroms)
//        {
//            var lambdas = forFroms.Select(item =>
//            {
//                var (@for, from) = item;
//                return (GetPropertyNamesInLambda(@for).First(), from);
//            });
//            RegisterByStringLambda(mappingExpression, lambdas);
//        }
//
//        public static void ConcatAndRegister<TSource, TDest, TProjection>(
//            Expression<Func<TSource, TProjection>> fromExpression,
//            IMappingExpression<TSource, TDest> mappingExpression,
//            params (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)[] props)
//        {
//            var lambdas = props.Select(item =>
//            {
//                var (@for, from) = item;
//                return (@for, Concat(fromExpression, from));
//            }).ToList();
//            RegisterByLambdas(mappingExpression, (dynamic) lambdas);
//        }
//
//        //simple version todo: maybe fix
//        public static void ConcatAndRegister<TSource, TDest, TProjection>(
//            Expression<Func<TSource, TProjection>> fromExpression,
//            IMappingExpression<TSource, TDest> mappingExpression,
//            params (Expression<Func<TDest, object>>, LambdaExpression)[] props)
//        {
//            var lambdas = props.Select(item =>
//            {
//                var (@for, from) = item;
//                return (@for, Concat(fromExpression, from));
//            });
//            RegisterByLambdas(mappingExpression, (dynamic) lambdas);
//        }
//    }
//}