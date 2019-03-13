using System.Collections.Generic;
using System.Linq;
using MethodGenerator.Extentions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{

    public class GetGenerationCode : IQueryHandler<IEnumerable<TypeEnum>, SyntaxNodeOrTokenList>
    {
        private ParameterSyntax GetParameter(string parameterName, SyntaxNodeOrToken nodeOrTokentoken)
        {
            #region TupleParameterStructure

            return SyntaxFactory.Parameter(
                    SyntaxFactory.Identifier(parameterName))
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
                                                                        nodeOrTokentoken
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
                                                                        nodeOrTokentoken
                                                                    })))))))
                            })));

            #endregion
        }

        public SyntaxNodeOrTokenList Handle(IEnumerable<TypeEnum> input)
        {
            var nodes = input.Select(x => x.GetAttributes<IGetSyntaxOrNodeToken>().Single().Node);
            var parameters = nodes.Select((x, i) => GetParameter($"arg{i}", x));
            //todo fix if
            return parameters.Aggregate(new SyntaxNodeOrTokenList(), (a, c) =>
            {
                if (a.Count == 0)
                    return a.Add(c);
                return a.Add(SyntaxFactory.Token(SyntaxKind.CommaToken)).Add(c);
            });
        }
    }
}