using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions;
using MapperExtensions.Models;

namespace MethodGenerator
{
    public static partial class RefactMapper
    {
        private static ICachedMethodInfo _cachedMethodInfo = new CachedMethodInfo();

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            return mapperExpressionWrapper.MappingExpression;
        }

        private static void RegisterRule<TSource, TDest, TProjection, T>(
            MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) rule)
        {
            var (from, @for) = rule;
            var result = Expression.Lambda<Func<TSource, T>>(
                Expression.Invoke(@for, mapperExpressionWrapper.Expression.Body),
                mapperExpressionWrapper.Expression.Parameters.First());
            mapperExpressionWrapper.MappingExpression
                .ForMember(string.Join('.', from.PropertiesStr()), s => s.MapFrom(result));
        }

        private static void RegisterByConvention<TSource, TDest, TProjection>(
            MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            var properties = typeof(TDest).GetProperties().ToList();
            properties.ForEach(prop =>
            {
                var ruleByConvention = _cachedMethodInfo
                    .GetMethod(nameof(HelpersMethod.GetRuleByConvention).Split('.').Last())
                    .MakeGenericMethod(typeof(TSource), typeof(TProjection), prop.PropertyType)
                    .Invoke(null, new object[] {prop, mapperExpressionWrapper.Expression});
                if (ruleByConvention == null) return;
                mapperExpressionWrapper.MappingExpression.ForMember(prop.Name,
                    s => s.MapFrom((dynamic) ruleByConvention));
            });
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            return mapperExpressionWrapper.FixRules(Enumerable
                .Empty<(Expression<Func<TDest, TProjection>>, Expression<Func<TProjection, TProjection>>)>());
        }

        public static MapperExpressionWrapper<TSource, TDest, TProjection> ToIf<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            Expression<Func<TDest, string>> @for,
            Expression<Func<TProjection, bool>> Test,
            Expression<Func<TProjection, string>> IfTrue,
            Expression<Func<TProjection, string>> IfFalse)
        {
            var projectParameter = mapperExpressionWrapper.Expression.Parameters.First();
            var projection = mapperExpressionWrapper.Expression;
            var newTest =
                Expression.Lambda<Func<TSource, Boolean>>(Expression.Invoke(Test, projection.Body), projectParameter);
            var newIfTrue =
                Expression.Lambda<Func<TSource, string>>(Expression.Invoke(IfTrue, projection.Body), projectParameter);
            var newIfFalse =
                Expression.Lambda<Func<TSource, string>>(Expression.Invoke(IfFalse, projection.Body), projectParameter);

            var condition = Expression.Lambda<Func<TSource, string>>(
                Expression.Condition(newTest.Body, newIfTrue.Body, newIfFalse.Body),
                projectParameter);
            mapperExpressionWrapper.MappingExpression.ForMember(@for.PropertiesStr().First(),
                s => s.MapFrom((dynamic) condition));
            return mapperExpressionWrapper;
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