using System;
using System.Linq.Expressions;
using AutoMapper;

namespace MapperExtensions.Models
{
    public class MapperExpressionWrapper<TSource, TDest, TProjection>
    {
        internal IMappingExpression<TSource, TDest> MappingExpression { get; }
        internal Expression<Func<TSource, TProjection>> Expression { get; }

        public MapperExpressionWrapper(IMappingExpression<TSource, TDest> mappingExpression,
            Expression<Func<TSource, TProjection>> func)
        {
            MappingExpression = mappingExpression;
            Expression = func;
        }
    }
}