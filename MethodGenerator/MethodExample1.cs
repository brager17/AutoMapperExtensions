using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Remotion.Linq.Clauses.ResultOperators;

namespace MethodGenerator
{
    public static partial class MethodExample
    {
        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0)
        {
            var parameters = new[] {ToObjectLambdas(arg0), ToObjectLambdas(arg0)};
            return ObjectTo(mapperExpressionWrapper, parameters);
        }


        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg0,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg0,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg0,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg0,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg0,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0,
            (Expression<Func<TDest, int>>, Expression<Func<TProjection, int>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0,
            (Expression<Func<TDest, bool>>, Expression<Func<TProjection, bool>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0,
            (Expression<Func<TDest, double>>, Expression<Func<TProjection, double>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0,
            (Expression<Func<TDest, float>>, Expression<Func<TProjection, float>>) arg1)
        {
            throw new NotImplementedException();
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0,
            (Expression<Func<TDest, DateTime>>, Expression<Func<TProjection, DateTime>>) arg1)
        {
            var parameters = new[] {ToObjectLambdas(arg0), ToObjectLambdas(arg1)};
            return ObjectTo(mapperExpressionWrapper, parameters);
        }


        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg0,
            (Expression<Func<TDest, string>>, Expression<Func<TProjection, string>>) arg1)
        {
            var parameters = new[] {ToObjectLambdas(arg0), ToObjectLambdas(arg1)};
            return ObjectTo(mapperExpressionWrapper, parameters);
        }


        private static IMappingExpression<TSource, TDest> ObjectTo<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            IEnumerable<(Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)> rules)
        {
            var rulesByConvention =
                Helpers.GetConventionMap<TSource, TDest, TProjection, Object>(mapperExpressionWrapper.Expression);
            var concatProjection = rules.Select(x =>
            {
                var (from, @for) = x;
                var result = mapperExpressionWrapper.Expression.ConcatPropertyExpressionToLambda<TSource, object>(@for);
                return (from, result);
            });
            var concatMapRules =
                concatProjection.LeftJoin(rulesByConvention, new ExpressionTupleComparer<TDest, TSource, object>());
            RefactorExtensions.Register(mapperExpressionWrapper.MappingExpression, concatMapRules);
            return mapperExpressionWrapper.MappingExpression;
        }

        public static IMappingExpression<TSource, TDest> To<TSource, TDest, TProjection>(
            this MapperExpressionWrapper<TSource, TDest, TProjection> mapperExpressionWrapper,
            params (Expression<Func<TDest, object>>, Expression<Func<TProjection, object>>)[] rules)
        {
            var rulesByConvention =
                Helpers.GetConventionMap<TSource, TDest, TProjection, Object>(mapperExpressionWrapper.Expression);
            var concatProjection = rules.Select(x =>
            {
                var (from, @for) = x;
                var result = mapperExpressionWrapper.Expression.ConcatPropertyExpressionToLambda<TSource, object>(@for);
                return (from, result);
            });
            var concatMapRules =
                concatProjection.LeftJoin(rulesByConvention, new ExpressionTupleComparer<TDest, TSource, object>());
            RefactorExtensions.Register(mapperExpressionWrapper.MappingExpression, concatMapRules);
            return mapperExpressionWrapper.MappingExpression;
        }
        
        
        public static (Expression<Func<T1, object>>, Expression<Func<T2, object>>) ToObjectLambdas<T1, T2, T3>
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
        
    }
}