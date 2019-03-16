using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic.CompilerServices;

namespace MethodGenerator
{
    public static partial class MethodExample
    {
        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            params (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)[] parameters)
        {
            return mapperExpressionWrapper.FixRules(parameters);
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            Expression<Func<TDest, object>> from, Expression<Func<TProjection, object>> @for)
        {
            return mapperExpressionWrapper.FixRules(new[] {(from, @for)});
        }

        public static MapperExpressionWrapper<TSource, TDest, TProjection> ConditionalFormat<TSource, TProjection,
            TDest>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            Expression<Func<TDest, object>> mapExpression,
            Expression<Func<TProjection, bool>> Test,
            Expression<Func<TProjection, object>> ifTrue,
            Expression<Func<TProjection, object>> ifFalse)
        {
            return mapperExpressionWrapper.FixConditionalFormatRules(mapExpression, Test, ifTrue, ifFalse);
        }


//        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
//            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
//        {
//            return mapperExpressionWrapper.FixRules(Enumerable
//                .Empty<(Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)>());
//        }
    }
}