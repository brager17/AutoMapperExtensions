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
        //


        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0)
        {
            var parameters = new[] {ToObjectLambdas(arg0)};
            return mapperExpressionWrapper.ObjectTo(parameters);
        }
    }
}