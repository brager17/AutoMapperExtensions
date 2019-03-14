using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class ReWriteMethodInfo
    {
        public ReWriteMethodInfo(ParameterListSyntax addedParameters,
            SeparatedSyntaxList<ExpressionSyntax> lambdaParameters)
        {
            AddedParameters = addedParameters;
            LambdaParameters = lambdaParameters;
        }

        public ParameterListSyntax AddedParameters { get; }

        public SeparatedSyntaxList<ExpressionSyntax> LambdaParameters { get; }
    }
}