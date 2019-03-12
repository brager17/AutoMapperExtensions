using Microsoft.CodeAnalysis.CSharp;

namespace MethodGenerator
{
    public enum TypeEnum : byte
    {
        [SyntaxType(SyntaxKind.IntKeyword)] Int,
        [SyntaxType(SyntaxKind.BoolKeyword)] Bool,
        [SyntaxType(SyntaxKind.DoubleKeyword)] Double,
        [SyntaxType(SyntaxKind.FloatKeyword)] Float,
        [IdentifierType("DateTime")] DateTime,
        [SyntaxType(SyntaxKind.StringKeyword)] String
    }
}