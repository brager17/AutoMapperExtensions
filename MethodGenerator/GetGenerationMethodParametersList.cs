using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MethodGenerator.Extentions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public class GetGenerationMethodParametersList : IQueryHandler<IEnumerable<TypeEnum>, SyntaxNodeOrTokenList>
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

    public class Parameter
    {
        public string ParameterName { get; set; }
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
            var items = input.Select(x => GetInitializeItem(x.ParameterName)).ToList();
            var list = items.Aggregate(Enumerable.Empty<SyntaxNodeOrToken>(),
                (a, c) =>
                {
                    if (!a.Any())
                        return a.Concat(new SyntaxNodeOrToken[] {c});
                    return a.Concat(new SyntaxNodeOrToken[] {Token(SyntaxKind.CommaToken)})
                        .Concat(new SyntaxNodeOrToken[] {c});
                }).ToArray();
            var r = SeparatedList<ExpressionSyntax>(list);
            return r;
        }
    }
}