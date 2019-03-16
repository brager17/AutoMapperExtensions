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

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // this is not the "To" method
            if (node.Identifier.Value.ToString() != "To") return base.VisitMethodDeclaration(node);
            var body = (BlockSyntax) base.Visit(node.Body);
            //todo ref it
            return node.Update(
                node.AttributeLists, node.Modifiers,
                node.ReturnType,
                node.ExplicitInterfaceSpecifier,
                node.Identifier,
                node.TypeParameterList.AddParameters(ReWriteMethodInfo.Generics.Parameters.ToArray()),
                // to add parameters to the method
                node.ParameterList.AddParameters(ReWriteMethodInfo.AddedParameters.Parameters.ToArray()),
                node.ConstraintClauses, body, node.SemicolonToken);
        }

        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            // to add parameters to the array initializer
            //todo ref it
            return node.Update(
                node.OpenBraceToken,
                node.Expressions.AddRange(ReWriteMethodInfo.LambdaParameters),
                node.CloseBraceToken);
        }
    }
}