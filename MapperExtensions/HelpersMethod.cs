using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using MapperExtensions.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace MapperExtensions
{
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


        private static MapPropertyInfo GetAdvancesConventionInfo<TProjection>(MemberInfo destProperty)
        {
            var propertyPath = AdvanceMapProperty(CamelCaseMatch(destProperty.Name), typeof(TProjection))
                .ToList();
            return propertyPath.All(xx => xx != null)
                ? new MapPropertyInfo(destProperty.Name, propertyPath)
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
                var newPath = CamelCaseMatch(pathStr.Remove(0, prop.Name.Length)).ToList();
                // прошли весь путь
                if (!newPath.Any())
                    return new[] {prop.Name};
                // рекурсивно идем дальше, конкатенация с уже найденными свойствами
                var analyze = AdvanceMapProperty(newPath, prop.PropertyType).ToList();
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
            var parameter = fromLambda.Parameters.First();
            //props = ["Identity","Passport","Name"]
            props = fromLambda.PropertiesStr().Concat(props);
            // returned  Expression<Func<Pupil,string>> x=>x.Identity.Passport.Name
            return GetLambda<S, P1>(props, parameter);
        }
    }
}