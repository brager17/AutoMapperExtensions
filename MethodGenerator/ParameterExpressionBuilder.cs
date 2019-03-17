using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class ParameterExpressionBuilder : IBuilder<Parameter, ParameterSyntax>
    {
        public ParameterSyntax Build(Parameter info)
        {
            return SyntaxFactory.Parameter(
                    SyntaxFactory.Identifier(info.ArgumentInfo))
                .WithType(
                    SyntaxFactory.TupleType(
                        SyntaxFactory.SeparatedList<TupleElementSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                SyntaxFactory.TupleElement(
                                    SyntaxFactory.GenericName(
                                            SyntaxFactory.Identifier("Expression"))
                                        .WithTypeArgumentList(
                                            SyntaxFactory.TypeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                    SyntaxFactory.GenericName(
                                                            SyntaxFactory.Identifier("Func"))
                                                        .WithTypeArgumentList(
                                                            SyntaxFactory.TypeArgumentList(
                                                                SyntaxFactory.SeparatedList<TypeSyntax>(
                                                                    new SyntaxNodeOrToken[]
                                                                    {
                                                                        SyntaxFactory.IdentifierName("TDest"),
                                                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                                        SyntaxFactory.IdentifierName(info.GenericName)
                                                                    }))))))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.TupleElement(
                                    SyntaxFactory.GenericName(
                                            SyntaxFactory.Identifier("Expression"))
                                        .WithTypeArgumentList(
                                            SyntaxFactory.TypeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                    SyntaxFactory.GenericName(
                                                            SyntaxFactory.Identifier("Func"))
                                                        .WithTypeArgumentList(
                                                            SyntaxFactory.TypeArgumentList(
                                                                SyntaxFactory.SeparatedList<TypeSyntax>(
                                                                    new SyntaxNodeOrToken[]
                                                                    {
                                                                        SyntaxFactory.IdentifierName("TProjection"),
                                                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                                        SyntaxFactory.IdentifierName(info.GenericName)
                                                                    })))))))
                            })));
        }
    }
}