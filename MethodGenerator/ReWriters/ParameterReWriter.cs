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
    }

    public class ReWriteMethodInfo
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public ParameterListSyntax AddedParameters { get; set; }
    }
}