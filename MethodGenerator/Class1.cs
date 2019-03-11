using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public interface IQueryHandler<in T, out R>
    {
        R Handle(T input);
    }

    public interface IHandler<in T>
    {
        void Handle(T input);
    }


    public interface IGetSyntaxOrNodeToken
    {
        SyntaxNodeOrToken Node { get; }
    }

    public class SyntaxTypeAttribute : Attribute, IGetSyntaxOrNodeToken
    {
        public readonly SyntaxKind _kind;

        public SyntaxTypeAttribute(SyntaxKind kind)
        {
            _kind = kind;
        }

        public SyntaxNodeOrToken Node => PredefinedType(Token(_kind));
    }

    public class IdentifierTypeAttribute : Attribute, IGetSyntaxOrNodeToken
    {
        private string Kind;

        public IdentifierTypeAttribute(string kind)
        {
            Kind = kind;
        }

        public SyntaxNodeOrToken Node => IdentifierName(Kind);
    }


    public enum TypeEnum : byte
    {
        [SyntaxType(SyntaxKind.IntKeyword)] Int,
        [SyntaxType(SyntaxKind.BoolKeyword)] Bool,
        [SyntaxType(SyntaxKind.DoubleKeyword)] Double,
        [SyntaxType(SyntaxKind.FloatKeyword)] Float,
        [IdentifierType("DateTime")] DateTime,
        [SyntaxType(SyntaxKind.StringKeyword)] String
    }

    public class MethodGenerator : IHandler<IEnumerable<TypeEnum>>
    {
        private IQueryHandler<FileInfoDto, string> fileReader { get; set; }
        private IQueryHandler<IEnumerable<TypeEnum>, string> getGenerationCode { get; set; }

        public MethodGenerator()
        {
            fileReader = new FileReader();
            getGenerationCode = new GetGenerationCode();
        }

        public void Handle(IEnumerable<TypeEnum> input)
        {
            var path = "/home/evgeny/Документы/automapper/MethodGenerator";
            var examplecode =
                CSharpSyntaxTree
                    .ParseText(fileReader.Handle(new FileInfoDto() {Name = "MethodExample", PathToDirectory = path}))
                    .GetRoot();
            var root = CSharpSyntaxTree.ParseText(getGenerationCode.Handle(input)).GetRoot();
            new RoslynVisitor().Visit(examplecode,root);
        }
    }

    public class GetGenerationCode : IQueryHandler<IEnumerable<TypeEnum>, string>
    {
        private ParameterSyntax GetParameter(string parameterName, SyntaxNodeOrToken nodeOrTokentoken)
        {
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
        }

        public string Handle(IEnumerable<TypeEnum> input)
        {
            var nodes = input.Select(x =>
            {
                return typeof(TypeEnum).GetMembers().Single(xx => xx.Name == x.ToString())
                    .GetCustomAttributes(true)
                    .Cast<IGetSyntaxOrNodeToken>()
                    .Single().Node;
            }).ToList();
            var ss = nodes.Select((x, i) => GetParameter($"arg{i}", x));
            var list = new List<SyntaxNodeOrTokenList> { };
            foreach (var s in ss)
            {
                list.Add(NodeOrTokenList(s));
                list.Add(NodeOrTokenList(Token(SyntaxKind.CommaToken)));
            }

            return list.ToString();
        }
    }

    public class RoslynVisitor : CSharpSyntaxRewriter
    {
        private SyntaxNode Node;
        public SyntaxNode Visit(SyntaxNode node, SyntaxNode replaceNode)
        {
            Node = replaceNode;
            return base.Visit(node);
        }

        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            //вот тут
            Console.WriteLine(node);
            return Node;
            return base.VisitParameter(node);
        }
    }

    public class FileWriter : IHandler<WriteFileInfoDto>
    {
        public void Handle(WriteFileInfoDto input)
        {
            File.WriteAllText($"{input.PathToDirectory}/{input.Name}.cs", input.Code);
        }
    }

    public class FileReader : IQueryHandler<FileInfoDto, string>
    {
        public string Handle(FileInfoDto input)
        {
            return File.ReadAllText($"{input.PathToDirectory}/{input.Name}.cs");
        }
    }

    public class FileInfoDto
    {
        public string Name { get; set; }
        public string PathToDirectory { get; set; }
    }

    public class WriteFileInfoDto : FileInfoDto
    {
        public string Code { get; set; }
    }
}