using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class InitialExpressionBuilder : IBuilder<InitialExpressionBuilderInfo, InvocationExpressionSyntax>
    {
        public InvocationExpressionSyntax Build(InitialExpressionBuilderInfo info)
        {
            return SyntaxFactory.InvocationExpression(SyntaxFactory.IdentifierName("Convert"))
                .WithArgumentList(SyntaxFactory.ArgumentList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Argument(SyntaxFactory.IdentifierName(info.ArgumentName)))));
        }
    }
}