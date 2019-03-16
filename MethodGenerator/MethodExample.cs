using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions.Models;

namespace MethodGenerator
{
    public static partial class MethodExample
    {
        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0)
        {
            var parameters = new[] {Convert(arg0)};
            return mapperExpressionWrapper.FixRules(parameters);
        }

        public static (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)
            Convert<TDest, T, TProjection>(
                (Expression<Func<TDest, T>>,
                    Expression<Func<TProjection, T>>) arg)
        {
            var convertLambda = Expression.Lambda<Func<TDest, object>>(
                Expression.Convert(arg.Item1.Body, typeof(object)), arg.Item1.Parameters.First());
            var convertLambda1 = Expression.Lambda<Func<TProjection, object>>(
                Expression.Convert(arg.Item2.Body, typeof(object)), arg.Item2.Parameters.First());
            return (convertLambda, convertLambda1);
        }
    }
}