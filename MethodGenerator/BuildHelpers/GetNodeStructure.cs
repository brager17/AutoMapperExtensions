using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MethodGenerator
{
    public class GetNodeStructure<TP, TIn, TOut> : IQuery<IEnumerable<TP>, TOut> where TIn : SyntaxNode
    {
        private readonly IGetStructure<TIn, TOut> _getStructure;
        private readonly IBuilder<TP, TIn> _builder;

        public GetNodeStructure(IGetStructure<TIn, TOut> getStructure, IBuilder<TP, TIn> builder)
        {
            _getStructure = getStructure;
            _builder = builder;
        }

        public TOut Handle(IEnumerable<TP> input) => _getStructure.GetStructure(input.Select(_builder.Build));
    }
}