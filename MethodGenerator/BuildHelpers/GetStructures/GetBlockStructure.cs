using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class GetBlockStructure : IGetStructure<StatementSyntax, BlockSyntax>
    {
        public BlockSyntax GetStructure(IEnumerable<StatementSyntax> input)
        {
            return SyntaxFactory.Block(input);
        }
    }
}