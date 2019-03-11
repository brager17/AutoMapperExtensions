using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public static partial class MethodExample
    {
        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg2,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg3,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg4,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg5)
        {
            throw new NotImplementedException();
        }
    }
}