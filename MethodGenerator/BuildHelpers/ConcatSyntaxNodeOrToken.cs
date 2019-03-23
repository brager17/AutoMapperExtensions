using System.Collections.Generic;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace MethodGenerator
{
    public class ConcatSyntaxNodeOrToken<T> : IQuery<IEnumerable<T>, SyntaxNodeOrTokenList>
        where T : SyntaxNode
    {
        public SyntaxNodeOrTokenList Handle(IEnumerable<T> enumerable)
        {
            return enumerable.JOIN<T, SyntaxToken, SyntaxNodeOrTokenList>
                ((a, c) => a.Add(c), (a, c) => a.Add(c), SyntaxFactory.Token(SyntaxKind.CommaToken));
        }
    }
}