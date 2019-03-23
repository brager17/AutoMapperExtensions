using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions.Models;
using Tests;

namespace MapperExtensions
{
    public static partial class MapperExtensions
    {
        private static ICachedMethodInfo _cachedMethodInfo = new CachedMethodInfo();
        private static InterpolationStringReplacer _interpolationStringReplacer = new InterpolationStringReplacer();

        public static MapperExpressionWrapper<TSource, TDest, TProjection> From<TSource, TDest, TProjection>(
            this IMappingExpression<TSource, TDest> mapping, Expression<Func<TSource, TProjection>> expression)
            => new MapperExpressionWrapper<TSource, TDest, TProjection>(mapping, expression);

        public static IMappingExpression<TSource, TDest> FixRules<TSource, TDest, TProjection, T>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            IEnumerable<(Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>)> rules)
        {
            var properties = typeof(TDest).GetProperties().ToList();
            properties.ForEach(prop =>
            {
                var ruleByConvention = _cachedMethodInfo
                    .GetMethod(nameof(HelpersMethod.GetRuleByConvention))
                    .MakeGenericMethod(typeof(TSource), typeof(TProjection), prop.PropertyType)
                    .Invoke(null, new object[] {prop, mapperExpressionWrapper.FromExpression});

                if (ruleByConvention == null) return;
                mapperExpressionWrapper.MappingExpression.ForMember(prop.Name,
                    s => s.MapFrom((dynamic) ruleByConvention));
            });

            var list = rules.ToList();

            list.ForEach(x =>
            {
                var (from, @for) = x;
                var result = Expression.Lambda<Func<TSource, T>>(
                    Expression.Invoke(@for, mapperExpressionWrapper.FromExpression.Body),
                    mapperExpressionWrapper.FromExpression.Parameters.First());
                mapperExpressionWrapper.MappingExpression
                    .ForMember(string.Join('.', from.PropertiesStr()), s => s.MapFrom(result));
            });
            return mapperExpressionWrapper.MappingExpression;
        }
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
            //rule = (x=>x.Group,x=>x.Number)
            var (from, @for) = rule;
            // заменяем интерполяцию на конкатенацию строк
            @for = (Expression<Func<TProjection, T>>) _interpolationStringReplacer.Visit(@for);
            // mapperExpressionWrapper.FromExpression = (x=>x.EducationCard.StudyGroup
            var result = Expression.Lambda<Func<TSource, T>>(
                Expression.Invoke(@for, mapperExpressionWrapper.FromExpression.Body),
                mapperExpressionWrapper.FromExpression.Parameters.First());
            // destPropertyName = group
            var destPropertyName = from.PropertiesStr().First();
            // result = x => Invoke(x => x.Number, x.EducationCard.StudyGroup)
            mapperExpressionWrapper.MappingExpression
                .ForMember(destPropertyName, s => s.MapFrom(result));
        }

        private static void RegisterByConvention<TSource, TDest, TProjection>(
            MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            var properties = typeof(TDest).GetProperties().ToList();
            properties.ForEach(prop =>
            {
                // mapperExpressionWrapper.FromExpression = x=>x.Identity.Passport
                // prop.Name = Name
                // ruleByConvention Expression<Func<Pupil,string>> x=>x.Identity.Passport.Name
                var ruleByConvention = _cachedMethodInfo
                    .GetMethod(nameof(HelpersMethod.GetRuleByConvention))
                    .MakeGenericMethod(typeof(TSource), typeof(TProjection), prop.PropertyType)
                    .Invoke(null, new object[] {prop, mapperExpressionWrapper.FromExpression});
                if (ruleByConvention == null) return;
                //регистрируем
                mapperExpressionWrapper.MappingExpression.ForMember(prop.Name,
                    s => s.MapFrom((dynamic) ruleByConvention));
            });
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            RegisterByConvention(mapperExpressionWrapper);
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
            var projectParameter = mapperExpressionWrapper.FromExpression.Parameters.First();
            var projection = mapperExpressionWrapper.FromExpression;
            var newTest =
                Expression.Lambda<Func<TSource, bool>>(Expression.Invoke(Test, projection.Body), projectParameter);
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
        
    }
}