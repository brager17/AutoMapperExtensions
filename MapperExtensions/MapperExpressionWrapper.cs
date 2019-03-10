using System;
using System.Linq.Expressions;
using AutoMapper;

namespace MapperExtensions.Models
{
    public class MapperExpressionWrapper<TSource, TDest, TProjection>
    {
        public IMappingExpression<TSource, TDest> MappingExpression { get; }
        public Expression<Func<TSource, TProjection>> Expression { get; }

        public MapperExpressionWrapper(IMappingExpression<TSource, TDest> mappingExpression,
            Expression<Func<TSource, TProjection>> func)
        {
            MappingExpression = mappingExpression;
            Expression = func;
        }
    }
}