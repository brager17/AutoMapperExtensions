using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
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
            //todo refactoring it
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
}