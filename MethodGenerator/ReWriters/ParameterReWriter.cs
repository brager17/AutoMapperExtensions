using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class ParameterReWriter : CSharpSyntaxRewriter
    {
        private ReWriteMethodInfo ReWriteMethodInfo;

        public SyntaxNode Visit(MethodDeclarationSyntax node, ReWriteMethodInfo info)
        {
            ReWriteMethodInfo = info;
            return base.Visit(node);
        }

        public override SyntaxNode VisitParameterList(ParameterListSyntax node)
        {
            return node.AddParameters(ReWriteMethodInfo.AddedParameters.Parameters.ToArray());
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (node.ToString() == ReWriteMethodInfo.OldName)
                return SyntaxFactory.IdentifierName(ReWriteMethodInfo.NewName);
            return base.VisitIdentifierName(node);
        }

        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
           return node.Update(node.OpenBraceToken, ReWriteMethodInfo.lambdaParameters, node.CloseBraceToken);
        }
    }

    public class ReWriteMethodInfo
    {
        public ReWriteMethodInfo(string oldName, string newName, ParameterListSyntax addedParameters, SeparatedSyntaxList<ExpressionSyntax> lambdaParameters)
        {
            OldName = oldName;
            NewName = newName;
            AddedParameters = addedParameters;
            this.lambdaParameters = lambdaParameters;
        }
        public string OldName { get; }
        public string NewName { get; }
        public ParameterListSyntax AddedParameters { get; }

        public SeparatedSyntaxList<ExpressionSyntax> lambdaParameters { get; }
    }
}