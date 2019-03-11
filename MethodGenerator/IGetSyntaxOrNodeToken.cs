using Microsoft.CodeAnalysis;

namespace MethodGenerator
{
    public interface IGetSyntaxOrNodeToken
    {
        SyntaxNodeOrToken Node { get; }
    }
}