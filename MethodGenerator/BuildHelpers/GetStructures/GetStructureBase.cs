using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace MethodGenerator
{
    public abstract class GetStructureBase<TIn, TOut> : IGetStructure<TIn, TOut>
        where TIn : SyntaxNode
    {
        protected readonly IQuery<IEnumerable<TIn>, SyntaxNodeOrTokenList> Concat;
        public abstract TOut GetStructure(IEnumerable<TIn> enumerable);

        protected GetStructureBase(IQuery<IEnumerable<TIn>, SyntaxNodeOrTokenList> concat)
        {
            Concat = concat;
        }
    }
}