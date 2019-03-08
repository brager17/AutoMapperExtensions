using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace MapperExtensions.Models
{
    public static class AutoMapperExtensions
    {
        private static string GetPropertyNameInLambda(LambdaExpression lambda)
        {
            string propName = null;
            switch (lambda.Body)
            {
                case UnaryExpression unary when unary.Operand is MemberExpression member1:
                    propName = member1.Member.Name;
                    break;
                case MemberExpression member:
                    propName = member.Member.Name;
                    break;
            }

            return propName;
        }

        private static Expression Concat<TSource, TDest, TProjection>(
            this Expression<Func<TSource, TProjection>> fromExpression,
            Expression<Func<TDest, object>> concatExpression)
        {
            var propName = GetPropertyNameInLambda(concatExpression);
            var result = Expression.Property(fromExpression.Body, propName);
            return Expression.Lambda(result, fromExpression.Parameters.First());
        }

        private static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            params Expression<Func<TDest, object>>[] expressions)
        {
            if (!expressions.Any())
            {
                var destProperties = typeof(TDest).GetProperties();
                var projectionProperties = typeof(TProjection).GetProperties();
                var join = destProperties.Join(projectionProperties, x => new {x.Name, x.PropertyType},
                    x => new {x.Name, x.PropertyType}, (x, y) => x).ToList();
                var parameter = Expression.Parameter(typeof(TDest));
                var properties = join.Select(x => Expression.Property(parameter, x));
                var lambdas = properties.Select(x =>
                    Expression.Lambda<Func<TDest, object>>(Expression.Convert(x, typeof(object)), parameter)).ToArray();
                expressions = lambdas;
            }

            var from = expressions.Select(expression => mapperExpressionWrapper.Expression.Concat(expression));
            var @for = expressions.Select(GetPropertyNameInLambda);
            var zip = @for.Zip(from, (x, y) => new {For = x, From = y}).ToList();
            foreach (var info in zip)
            {
                mapperExpressionWrapper.MappingExpression
                    .ForMember(info.For, ss => ss.MapFrom((dynamic) info.From));
            }

            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            var destProperties = typeof(TDest).GetProperties();
            var projectionProperties = typeof(TProjection).GetProperties();
            var join = destProperties.Join(projectionProperties, x => new {x.Name, x.PropertyType},
                x => new {x.Name, x.PropertyType}, (x, y) => x).ToList();
            if (!join.Any())
            {
                throw new ArgumentException("совпадений не найдено");
            }

            var parameter = Expression.Parameter(typeof(TDest));
            var properties = join.Select(x => Expression.Property(parameter, x));
            var lambdas = properties.Select(x =>
                Expression.Lambda<Func<TDest, object>>(Expression.Convert(x, typeof(object)), parameter)).ToArray();
            return mapperExpressionWrapper.To(lambdas);
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            params (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)[] props)
        {
            var s = props.Select(x =>
            {
                var (@for, from) = x;
                var invoke = Expression.Invoke(from,mapperExpressionWrapper.Expression.Body);
                var lambda = Expression.Lambda(invoke, from.Parameters.First());
                return lambda;
            }).ToList();
            return null;
        }

        public static MapperExpressionWrapper<TSource, TDest, TProjection> From<TSource, TDest, TProjection>(
            this IMappingExpression<TSource, TDest> mapping, Expression<Func<TSource, TProjection>> expression)
        {
            return new MapperExpressionWrapper<TSource, TDest, TProjection>(mapping, expression);
        }
    }
}