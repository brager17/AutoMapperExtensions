using System;
using System.Collections.Generic;
using System.Linq;
using MethodGenerator.Extentions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public interface IMonoid<R>
    {
        R Add(R input);
    }

    public abstract class Monoid<T, R> : IMonoid<R> where R : new()
    {
        protected T item { get; set; }
        public abstract R Add(R input);
    }

    public class ParameterSyntaxMonoid : Monoid<ParameterSyntax, SyntaxNodeOrTokenList>
    {
        public ParameterSyntax item { get; set; }

        public ParameterSyntaxMonoid(ParameterSyntax item)
        {
            this.item = item;
        }

        public override SyntaxNodeOrTokenList Add(SyntaxNodeOrTokenList input)
        {
            return input.Add(item);
        }
    }

    public static class AggregateExtension
    {
        public static R Aggregate<T, R>(this IEnumerable<Monoid<T, R>> enumerable, T Separator)
            where R : IEquatable<R>, new()
        {
            var list = enumerable.ToList();
            enumerable.Aggregate(new R(), (a, c) =>
            {
                var tt = list.add
            })
        }
    }

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
            var parameters = nodes.Select((x, i) => (GetParameter($"arg{i}", x)));
            // please watch AggregateExtension
            return parameters.Aggregate()
        }
    }
}