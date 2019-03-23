using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class GetTypeParameterListSyntax : GetStructureBase<TypeParameterSyntax, TypeParameterListSyntax>
    {
        public GetTypeParameterListSyntax(
            IQuery<IEnumerable<TypeParameterSyntax>, SyntaxNodeOrTokenList> concat)
            : base(concat)
        {
        }

        public override TypeParameterListSyntax GetStructure(IEnumerable<TypeParameterSyntax> enumerable)
        {
            return SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList<TypeParameterSyntax>(Concat.Handle(enumerable)));
        }
    }
}