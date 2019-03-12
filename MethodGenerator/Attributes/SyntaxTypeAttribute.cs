using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace MethodGenerator
{
    public class SyntaxTypeAttribute : Attribute, IGetSyntaxOrNodeToken
    {
        public readonly SyntaxKind _kind;

        public SyntaxTypeAttribute(SyntaxKind kind)
        {
            _kind = kind;
        }

        public SyntaxNodeOrToken Node => SyntaxFactory.PredefinedType(SyntaxFactory.Token(_kind));
    }
}