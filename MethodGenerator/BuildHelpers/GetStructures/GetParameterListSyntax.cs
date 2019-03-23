using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class GetParameterListSyntax : GetStructureBase<ParameterSyntax, ParameterListSyntax>
    {
        public GetParameterListSyntax(IQuery<IEnumerable<ParameterSyntax>, SyntaxNodeOrTokenList> concat)
            : base(concat)
        {
        }

        public override ParameterListSyntax GetStructure(IEnumerable<ParameterSyntax> enumerable)
        {
            return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>(Concat.Handle(enumerable)));
        }
    }
}