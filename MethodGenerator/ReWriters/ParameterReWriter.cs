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
            var returnStatement = node.Body.Statements.Last();
            var beforeReturnStatements = node.Body.Statements.SkipLast(1);
            var newBody = SyntaxFactory.Block(beforeReturnStatements.Concat(ReWriteMethodInfo.Block.Statements)
                .Concat(new[] {returnStatement}));
            return node.Update(
                node.AttributeLists, node.Modifiers,
                node.ReturnType,
                node.ExplicitInterfaceSpecifier,
                node.Identifier,
                node.TypeParameterList.AddParameters(ReWriteMethodInfo.Generics.Parameters.ToArray()),
                node.ParameterList.AddParameters(ReWriteMethodInfo.AddedParameters.Parameters.ToArray()),
                node.ConstraintClauses,
                newBody,
                node.SemicolonToken);
        }
    }
}