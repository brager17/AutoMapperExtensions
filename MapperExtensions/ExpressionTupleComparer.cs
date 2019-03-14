using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MapperExtensions.Models
{
    public class ExpressionTupleComparer<TDest, TProjection, TProjection1>
        : IEqualityComparer<(Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>)>
    {
        public bool Equals((Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>) x,
            (Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>) y)
        {
            return x.Item1.ToString() == y.Item1.ToString();
        }

        public int GetHashCode((Expression<Func<TDest, TProjection1>>, Expression<Func<TProjection, TProjection1>>) obj)
        {
            return string.Join('.', SkipParameter(obj.Item1)).GetHashCode();
        }

        private static IEnumerable<string> SkipParameter(LambdaExpression lambdaExpression)
        {
            return lambdaExpression.Body.ReplaceObjectConvert().ToString().Split('.').Skip(1);
        }
    }
}