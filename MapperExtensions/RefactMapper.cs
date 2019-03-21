using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoMapper;
using MapperExtensions.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace MapperExtensions
{
    public interface ICachedMethodInfo
    {
        MethodInfo GetMethod(string name);
    }

    public class CachedMethodInfo : ICachedMethodInfo
    {
        Dictionary<string, MethodInfo> _dic = new Dictionary<string, MethodInfo>
        {
            {
                nameof(HelpersMethod.GetRuleByConvention).Split('.').Last(),
                typeof(HelpersMethod).GetMethod(nameof(HelpersMethod.GetRuleByConvention).Split('.').Last())
            },
        };

        public MethodInfo GetMethod(string name)
        {
            return _dic.First(x => x.Key == name).Value;
        }
    }

    public static class RefactMapper
    {
        private static ICachedMethodInfo _cachedMethodInfo = new CachedMethodInfo();


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
    }


    public static class HelpersMethod
    {
        public static IEnumerable<string> PropertiesStr(this LambdaExpression lambda)
        {
            var expression = lambda.Body;
            var result = expression.ToString().Split('.').Skip(1);
            return result;
        }

        public static object GetRuleByConvention<TSource, TProjection, TProjection1>
            (PropertyInfo property, Expression<Func<TSource, TProjection>> projectExpression)
        {
            var advancedMapRules = GetAdvancesConventionInfo<TProjection>(property);
            if (advancedMapRules == null)
                return null;
            var lambda =
                Concat<TSource, TProjection, TProjection1>
                    (advancedMapRules.PathToSourceProperty, projectExpression);
            return lambda;
        }


        private static MapPropertyInfo GetAdvancesConventionInfo<TProjection>(PropertyInfo destProperty)
        {
            var result1 = AdvanceMapProperty(CamelCaseMatch(destProperty.Name),
                typeof(TProjection));
            return result1.All(xx => xx != null)
                ? new MapPropertyInfo(destProperty.Name, result1)
                : null;
        }

        public static IEnumerable<string> AdvanceMapProperty(IEnumerable<string> path, Type properType)
        {
            var properties = properType.GetProperties().ToList();
            var pathStr = path.Join("");
            // path ["Identity","Passport","Name"]
            foreach (var prop in properties)
            {
                // 1 шаг
                //prop.Name !="Identity" -> continue
                if (!pathStr.StartsWith(prop.Name)) continue;
                // 1 шаг
                // удаляем строку с имененем найденного свойства:newPath = ["Passport","Name"]
                var newPath = CamelCaseMatch(pathStr.Remove(0, prop.Name.Length));
                // прошли весь путь
                if (!newPath.Any())
                    return new[] {prop.Name};
                // рекурсивно идем дальше, конкатенация с уже найденными свойствами
                var analyze = AdvanceMapProperty(newPath, prop.PropertyType);
                if (analyze.All(x => x != null))
                    return new[] {prop.Name}.Concat(analyze);
            }

            // если не удалось найти подходящего свойства возвращаем null и заканчиваем поиск
            return new string[] {null};
        }

        private static IEnumerable<string> CamelCaseMatch(string str)
        {
            return Regex.Matches(str, @"[A-Z][\w]+").Select(xx => xx.Value).ToList();
        }

        private static Expression<Func<S, P1>> GetLambda<S, P1>(IEnumerable<string> props,
            ParameterExpression expressionProperty)
        {
            return Expression.Lambda<Func<S, P1>>(props.Aggregate((Expression) expressionProperty,
                Expression.Property), expressionProperty);
        }

        private static Expression<Func<S, P1>> Concat<S, P, P1>(IEnumerable<string> props,
            Expression<Func<S, P>> fromLambda)
        {
            //fromlambda = x=>x.Identity.Passport,props = ["Name"]
            //propertyExpression = "x"
            var propertyExpression = fromLambda.Parameters.First();
            //props = ["Identity","Passport","Name"]
            props = fromLambda.PropertiesStr().Concat(props);
            // returned  Expression<Func<Pupil,string>> x=>x.Identity.Passport.Name
            return GetLambda<S, P1>(props, propertyExpression);
        }
    }
}