using System.Collections.Generic;
using System.Linq;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public interface IBuilder<TInfo, TNode>
    {
        TNode Build(TInfo info);
    }

    public interface IGetStructure<TNode, TOut>
    {
        TOut GetStructure(IEnumerable<TNode> input);
    }

    public class ConcatSyntaxNodeOrToken<T> : IQueryHandler<IEnumerable<T>, SyntaxNodeOrTokenList>
        where T : SyntaxNode
    {
        public SyntaxNodeOrTokenList Handle(IEnumerable<T> enumerable)
        {
            return enumerable.JOIN<T, SyntaxToken, SyntaxNodeOrTokenList>
                ((a, c) => a.Add(c), (a, c) => a.Add(c), Token(SyntaxKind.CommaToken));
        }
    }

    public class GenericTypesBuilder : IBuilder<IGenericInfo, TypeParameterSyntax>
    {
        public TypeParameterSyntax Build(IGenericInfo info)
        {
            return TypeParameter(Identifier(info.GenericName));
        }
    }

    public abstract class GetStructureBase<TIn, TOut> : IGetStructure<TIn, TOut>
        where TIn : SyntaxNode
    {
        protected readonly IQueryHandler<IEnumerable<TIn>, SyntaxNodeOrTokenList> _concatHandler;
        public abstract TOut GetStructure(IEnumerable<TIn> enumerable);

        protected GetStructureBase(IQueryHandler<IEnumerable<TIn>, SyntaxNodeOrTokenList> concatHandler)
        {
            _concatHandler = concatHandler;
        }
    }

    public class GetParameterListSyntax : GetStructureBase<ParameterSyntax, ParameterListSyntax>
    {
        public GetParameterListSyntax(IQueryHandler<IEnumerable<ParameterSyntax>, SyntaxNodeOrTokenList> concatHandler)
            : base(concatHandler)
        {
        }

        public override ParameterListSyntax GetStructure(IEnumerable<ParameterSyntax> enumerable)
        {
            return ParameterList(SeparatedList<ParameterSyntax>(_concatHandler.Handle(enumerable)));
        }
    }

    public class GetTypeParameterListSyntax : GetStructureBase<TypeParameterSyntax, TypeParameterListSyntax>
    {
        public GetTypeParameterListSyntax(
            IQueryHandler<IEnumerable<TypeParameterSyntax>, SyntaxNodeOrTokenList> concatHandler)
            : base(concatHandler)
        {
        }

        public override TypeParameterListSyntax GetStructure(IEnumerable<TypeParameterSyntax> enumerable)
        {
            return TypeParameterList(SeparatedList<TypeParameterSyntax>(_concatHandler.Handle(enumerable)));
        }
    }

    public class GetNodeStructureHandler<TP, TIn, TOut> : IQueryHandler<IEnumerable<TP>, TOut> where TIn : SyntaxNode
    {
        private readonly IGetStructure<TIn, TOut> _getStructure;
        private readonly IBuilder<TP, TIn> _builder;

        public GetNodeStructureHandler(IGetStructure<TIn, TOut> getStructure, IBuilder<TP, TIn> builder)
        {
            _getStructure = getStructure;
            _builder = builder;
        }

        public TOut Handle(IEnumerable<TP> input) => _getStructure.GetStructure(input.Select(_builder.Build));
    }

    public class StatementBuilder : IBuilder<IArgumentInfo, StatementSyntax>
    {
        public StatementSyntax Build(IArgumentInfo info)
        {
            return ExpressionStatement(
                InvocationExpression(
                        IdentifierName("RegisterRule"))
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList<ArgumentSyntax>(
                                new SyntaxNodeOrToken[]
                                {
                                    Argument(
                                        IdentifierName("mapperExpressionWrapper")),
                                    Token(SyntaxKind.CommaToken),
                                    Argument(
                                        IdentifierName(info.ArgumentInfo))
                                }))));
        }
    }

    public class GetBlockStructure : IGetStructure<StatementSyntax, BlockSyntax>
    {
        public BlockSyntax GetStructure(IEnumerable<StatementSyntax> input)
        {
            return Block(input);
        }
    }
}