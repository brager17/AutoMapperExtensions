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
        //stub for code generate
        private static (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>) Stub<TDest,TProjection>()
        {
            return (x => (object) 1, x => (object) 2);
        }
        //
        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper)
        {
            var parameters = new[] {Stub<TDest, TProjection>()};
            return ObjectTo(mapperExpressionWrapper, parameters);
        }
    }
}