using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public static partial class MethodExample
    {
        private static (Expression<Func<T1, object>>, Expression<Func<T2, object>>) ToObjectLambdas<T1, T2, T3>
            ((Expression<Func<T1, T3>>, Expression<Func<T2, T3>>) lambda)
        {
            var (l1, l2) = lambda;
            var l1Body = l1.Body;
            var l2Body = l2.Body;
            var l1Parameters = l1.Parameters;
            var l2Parameters = l2.Parameters;
            return
                (Expression.Lambda<Func<T1, object>>(Expression.Convert(l1Body, typeof(object)), l1Parameters),
                    Expression.Lambda<Func<T2, object>>(Expression.Convert(l2Body, typeof(object)), l2Parameters));
        }

        // example code
        //stub for code generate
        private static (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)
            Stub<TDest, TProjection>()
        {
            return (x => (object) 1, x => (object) 2);
        }


//        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
//            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
//        {
//            var parameters = new[] {Stub<TDest, TProjection>()};
//            return mapperExpressionWrapper.FixRules(parameters);
//        }


        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            return mapperExpressionWrapper.FixRules(Enumerable
                .Empty<(Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)>());
        }
    }
}