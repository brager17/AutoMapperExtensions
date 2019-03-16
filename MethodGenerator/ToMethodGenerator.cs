using System.Collections.Generic;
using System.Linq;
using MapperExtensions.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public class ToMethodGenerator : IHandler<GenerateMethodsInfo>
    {
        private IQueryHandler<FileInfoDto, string> FileReader { get; }
        private IBuilder<ParameterBuildInfo, ParameterSyntax> ParameterBuilder { get; }
        private IHandler<WriteFileInfoDto> FileWriter { get; }

        private ClassReWriter ClassReWriter { get; }
        private ParameterReWriter MethodParametersReWriter { get; }

        private IBuilder<InitialExpressionBuilderInfo, InvocationExpressionSyntax> InitialExpressionBuilder { get; }

        public ToMethodGenerator()
        {
            ParameterBuilder = new ParameterExpressionBuilder();
            InitialExpressionBuilder = new InitialExpressionBuilder();
            MethodParametersReWriter = new ParameterReWriter();
            ClassReWriter = new ClassReWriter();
            FileReader = new FileReader();
            FileWriter = new FileWriter();
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = FileReader.Handle(new FileInfoDto(input.PathToExampleCodeFile));
            
            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();

            var exampleMethodDeclarationSyntax = exampleCode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(x => x.Identifier.Value.ToString() == "To");
            ;

            var generics = input.MethodsInfo.AddedParameters
                .Select(x =>
                    new
                    {
                        GenericList = TypeParameterList(
                            SeparatedList<TypeParameterSyntax>(Concat(x.Parameters
                                .Select(xx => TypeParameter(Identifier(xx.GenericName)))))),

                        ParameterList = ParameterList(
                            SeparatedList<ParameterSyntax>(
                                Concat(x.Parameters.Select(xx =>
                                    ParameterBuilder.Build(new ParameterBuildInfo
                                        {ArgumentName = xx.Argument, GenericName = xx.GenericName}))))),

                        initialItems = SeparatedList<ExpressionSyntax>(Concat(x.Parameters
                            .Select(xx => InitialExpressionBuilder.Build(new InitialExpressionBuilderInfo
                                {ArgumentName = xx.Argument}))))
                    })
                .Select(x =>
                    (MethodDeclarationSyntax) MethodParametersReWriter.Visit(exampleMethodDeclarationSyntax,
                        new ReWriteMethodInfo(x.ParameterList, x.initialItems, x.GenericList))).ToList();

            var classNode = ClassReWriter.Visit(exampleCode, generics);

            FileWriter.Handle(new WriteFileInfoDto(input.PathToDestinationFile, classNode.ToString()));
        }

        private static SyntaxNodeOrTokenList Concat<T>(IEnumerable<T> enumerable) where T : SyntaxNode
        {
            return enumerable.JOIN<T, SyntaxToken, SyntaxNodeOrTokenList>
                ((a, c) => a.Add(c), (a, c) => a.Add(c), Token(SyntaxKind.CommaToken));
        }
    }
}