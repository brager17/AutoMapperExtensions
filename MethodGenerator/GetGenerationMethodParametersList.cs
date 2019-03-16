using System;
using System.Collections.Generic;
using System.Linq;
using MapperExtensions.Models;
using MethodGenerator.Extentions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public class
        GetGenerationMethodParametersList : IQueryHandler<IEnumerable<ToMethodParameter>, SyntaxNodeOrTokenList>
    {
        private ParameterSyntax GetParameter(string parameterName, SyntaxNodeOrToken nodeOrTokentoken)
        {
            #region TupleParameterStructure

            return Parameter(
                    Identifier(parameterName))
                .WithType(
                    TupleType(
                        SeparatedList<TupleElementSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                TupleElement(
                                    GenericName(
                                            Identifier("Expression"))
                                        .WithTypeArgumentList(
                                            TypeArgumentList(
                                                SingletonSeparatedList<TypeSyntax>(
                                                    GenericName(
                                                            Identifier("Func"))
                                                        .WithTypeArgumentList(
                                                            TypeArgumentList(
                                                                SeparatedList<TypeSyntax>(
                                                                    new SyntaxNodeOrToken[]
                                                                    {
                                                                        IdentifierName("TDest"),
                                                                        Token(SyntaxKind.CommaToken),
                                                                        nodeOrTokentoken
                                                                    }))))))),
                                Token(SyntaxKind.CommaToken),
                                TupleElement(
                                    GenericName(
                                            Identifier("Expression"))
                                        .WithTypeArgumentList(
                                            TypeArgumentList(
                                                SingletonSeparatedList<TypeSyntax>(
                                                    GenericName(
                                                            Identifier("Func"))
                                                        .WithTypeArgumentList(
                                                            TypeArgumentList(
                                                                SeparatedList<TypeSyntax>(
                                                                    new SyntaxNodeOrToken[]
                                                                    {
                                                                        IdentifierName("TProjection"),
                                                                        Token(SyntaxKind.CommaToken),
                                                                        nodeOrTokentoken
                                                                    })))))))
                            })));

            #endregion
        }

        public SyntaxNodeOrTokenList Handle(IEnumerable<ToMethodParameter> input)
        {
            return new SyntaxNodeOrTokenList();
//            var parameters = nodes.Select((x, i) => GetParameter(x.ParameterName, x.Node));
//            // join separated by commas
//            // (two functions are passed because the SyntaxNodeOrTokenList function accepts SyntaxNodeOrToken
//            return parameters.Join<ParameterSyntax, SyntaxToken, SyntaxNodeOrTokenList>
//                ((a, c) => a.Add(c), (a, c) => a.Add(c), Token(SyntaxKind.CommaToken));
        }
    }

    public class
        GetGenerationArrayInitializer : IQueryHandler<IEnumerable<Parameter>, SeparatedSyntaxList<ExpressionSyntax>>
    {
        private InvocationExpressionSyntax GetInitializeItem(string itemName)
        {
            return InvocationExpression(IdentifierName("ToObjectLambdas"))
                .WithArgumentList(
                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName(itemName)))));
        }

        public SeparatedSyntaxList<ExpressionSyntax> Handle(IEnumerable<Parameter> input)
        {
            return new SeparatedSyntaxList<ExpressionSyntax>();
//            var items = input.Select(x => GetInitializeItem(x.ParameterName)).ToList();
//            // forming parameters of array 'parameters' initializer
//            var syntaxNodeOrTokens = items.Join<InvocationExpressionSyntax, SyntaxToken, SyntaxNodeOrTokenList>(
//                    (a, c) => a.Add(c), (a, c) => a.Add(c), Token(SyntaxKind.CommaToken))
//                .ToArray();
//            return SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens);
        }
    }
}