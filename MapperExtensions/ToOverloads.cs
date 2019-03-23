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
        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4, T5>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4, (Expression<Func<TDest, T5>>, Expression<Func<TProjection, T5>>) arg5)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            RegisterRule(mapperExpressionWrapper, arg5);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4, T5, T6>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4, (Expression<Func<TDest, T5>>, Expression<Func<TProjection, T5>>) arg5, (Expression<Func<TDest, T6>>, Expression<Func<TProjection, T6>>) arg6)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            RegisterRule(mapperExpressionWrapper, arg5);
            RegisterRule(mapperExpressionWrapper, arg6);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4, T5, T6, T7>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4, (Expression<Func<TDest, T5>>, Expression<Func<TProjection, T5>>) arg5, (Expression<Func<TDest, T6>>, Expression<Func<TProjection, T6>>) arg6, (Expression<Func<TDest, T7>>, Expression<Func<TProjection, T7>>) arg7)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            RegisterRule(mapperExpressionWrapper, arg5);
            RegisterRule(mapperExpressionWrapper, arg6);
            RegisterRule(mapperExpressionWrapper, arg7);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4, T5, T6, T7, T8>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4, (Expression<Func<TDest, T5>>, Expression<Func<TProjection, T5>>) arg5, (Expression<Func<TDest, T6>>, Expression<Func<TProjection, T6>>) arg6, (Expression<Func<TDest, T7>>, Expression<Func<TProjection, T7>>) arg7, (Expression<Func<TDest, T8>>, Expression<Func<TProjection, T8>>) arg8)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            RegisterRule(mapperExpressionWrapper, arg5);
            RegisterRule(mapperExpressionWrapper, arg6);
            RegisterRule(mapperExpressionWrapper, arg7);
            RegisterRule(mapperExpressionWrapper, arg8);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4, (Expression<Func<TDest, T5>>, Expression<Func<TProjection, T5>>) arg5, (Expression<Func<TDest, T6>>, Expression<Func<TProjection, T6>>) arg6, (Expression<Func<TDest, T7>>, Expression<Func<TProjection, T7>>) arg7, (Expression<Func<TDest, T8>>, Expression<Func<TProjection, T8>>) arg8, (Expression<Func<TDest, T9>>, Expression<Func<TProjection, T9>>) arg9)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            RegisterRule(mapperExpressionWrapper, arg5);
            RegisterRule(mapperExpressionWrapper, arg6);
            RegisterRule(mapperExpressionWrapper, arg7);
            RegisterRule(mapperExpressionWrapper, arg8);
            RegisterRule(mapperExpressionWrapper, arg9);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection, T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper, (Expression<Func<TDest, T>>, Expression<Func<TProjection, T>>) arg0, (Expression<Func<TDest, T1>>, Expression<Func<TProjection, T1>>) arg1, (Expression<Func<TDest, T2>>, Expression<Func<TProjection, T2>>) arg2, (Expression<Func<TDest, T3>>, Expression<Func<TProjection, T3>>) arg3, (Expression<Func<TDest, T4>>, Expression<Func<TProjection, T4>>) arg4, (Expression<Func<TDest, T5>>, Expression<Func<TProjection, T5>>) arg5, (Expression<Func<TDest, T6>>, Expression<Func<TProjection, T6>>) arg6, (Expression<Func<TDest, T7>>, Expression<Func<TProjection, T7>>) arg7, (Expression<Func<TDest, T8>>, Expression<Func<TProjection, T8>>) arg8, (Expression<Func<TDest, T9>>, Expression<Func<TProjection, T9>>) arg9, (Expression<Func<TDest, T10>>, Expression<Func<TProjection, T10>>) arg10)
        {
            RegisterByConvention(mapperExpressionWrapper);
            RegisterRule(mapperExpressionWrapper, arg0);
            RegisterRule(mapperExpressionWrapper, arg1);
            RegisterRule(mapperExpressionWrapper, arg2);
            RegisterRule(mapperExpressionWrapper, arg3);
            RegisterRule(mapperExpressionWrapper, arg4);
            RegisterRule(mapperExpressionWrapper, arg5);
            RegisterRule(mapperExpressionWrapper, arg6);
            RegisterRule(mapperExpressionWrapper, arg7);
            RegisterRule(mapperExpressionWrapper, arg8);
            RegisterRule(mapperExpressionWrapper, arg9);
            RegisterRule(mapperExpressionWrapper, arg10);
            return mapperExpressionWrapper.MappingExpression;
        }
    }
}