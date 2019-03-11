using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace MethodGenerator
{
    public class IdentifierTypeAttribute : Attribute, IGetSyntaxOrNodeToken
    {
        private string Kind;

        public IdentifierTypeAttribute(string kind)
        {
            Kind = kind;
        }

        public SyntaxNodeOrToken Node => SyntaxFactory.IdentifierName(Kind);
    }
}