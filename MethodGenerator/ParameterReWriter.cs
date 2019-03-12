using System.Collections.Generic;
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

    public class ClassReWriter : CSharpSyntaxRewriter
    {
        private IEnumerable<MemberDeclarationSyntax> ReWriteMethods;

        public SyntaxNode Visit(SyntaxNode node, IEnumerable<MethodDeclarationSyntax> methods)
        {
            ReWriteMethods = methods;
            return base.Visit(node);
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            //todo refactring it
            return node.Update(
                node.AttributeLists,
                node.Modifiers,
                node.Keyword,
                node.Identifier,
                node.TypeParameterList,
                node.BaseList,
                node.ConstraintClauses,
                node.OpenBraceToken,
                new SyntaxList<MemberDeclarationSyntax>(ReWriteMethods),
                node.CloseBraceToken,
                node.SemicolonToken);
        }
    }

    public class ReWriteMethodInfo
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public ParameterListSyntax AddedParameters { get; set; }
    }
}