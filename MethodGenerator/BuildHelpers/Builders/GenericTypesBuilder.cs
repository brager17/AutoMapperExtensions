using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class GenericTypesBuilder : IBuilder<IGenericInfo, TypeParameterSyntax>
    {
        public TypeParameterSyntax Build(IGenericInfo info)
        {
            return SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(info.GenericName));
        }
    }
}