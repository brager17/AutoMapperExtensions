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

        public static IMappingExpression<TSource, TDest> ToIf<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            Expression<Func<TDest, string>> @for,
            Expression<Func<TProjection, Boolean>> Test,
            Expression<Func<TProjection, string>> IfTrue,
            Expression<Func<TProjection, string>> IfFalse)
        {
            var projectParameter = mapperExpressionWrapper.Expression.Parameters.First();
            var projectProperties = mapperExpressionWrapper.Expression.Body.ToString().Split('.').Skip(1);
            var ifTrueProperties = projectProperties.Concat(IfTrue.Body.ToString().Split('.').Skip(1));
            var ifFalseBody = projectProperties.Concat(IfFalse.Body.ToString().Split('.').Skip(1));
            var projectIfTrue =
                Expression.Lambda(ifTrueProperties.Aggregate(((Expression) projectParameter), Expression.Property),
                    projectParameter);
            var projectIfFalse =
                Expression.Lambda(ifFalseBody.Aggregate(((Expression) projectParameter), Expression.Property),
                    projectParameter);
            var condition = Expression.Condition(Test.Body, projectIfTrue, projectIfFalse);
            var lambda = Expression.Lambda(condition, projectParameter);
            mapperExpressionWrapper.MappingExpression.ForMember(@for.PropertiesStr().ToString(), 
                s => s.MapFrom((dynamic) lambda));
            return mapperExpressionWrapper.MappingExpression;
        }


        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            return mapperExpressionWrapper.FixRules(Enumerable
                .Empty<(Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)>());
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