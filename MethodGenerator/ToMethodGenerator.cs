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
        private IQuery<FileInfoDto, string> FileReader { get; }
        private IBuilder<Parameter, ParameterSyntax> ParameterBuilder { get; }
        private IHandler<WriteFileInfoDto> FileWriter { get; }

        private ClassReWriter ClassReWriter { get; }
        private ParameterReWriter MethodParametersReWriter { get; }

        private GetNodeStructure<Parameter, ParameterSyntax, ParameterListSyntax> GetParameters { get; }
        private GetNodeStructure<IGenericInfo, TypeParameterSyntax, TypeParameterListSyntax> GetGenerics { get; }
        private GetNodeStructure<IArgumentInfo, StatementSyntax, BlockSyntax> GetBlockStatement { get; }


        public ToMethodGenerator()
        {
            ParameterBuilder = new ParameterExpressionBuilder();
            MethodParametersReWriter = new ParameterReWriter();
            ClassReWriter = new ClassReWriter();
            FileReader = new FileReader();
            FileWriter = new FileWriter();
            GetParameters =
                new GetNodeStructure<Parameter, ParameterSyntax, ParameterListSyntax>(
                    new GetParameterListSyntax(new ConcatSyntaxNodeOrToken<ParameterSyntax>()),
                    new ParameterExpressionBuilder());
            GetGenerics = new GetNodeStructure<IGenericInfo, TypeParameterSyntax, TypeParameterListSyntax>(
                new GetTypeParameterListSyntax(new ConcatSyntaxNodeOrToken<TypeParameterSyntax>()),
                new GenericTypesBuilder());
            GetBlockStatement =
                new GetNodeStructure<IArgumentInfo, StatementSyntax, BlockSyntax>(new GetBlockStructure(),
                    new StatementBuilder());
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = FileReader.Handle(new FileInfoDto(input.PathToExampleCodeFile));

            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();

            var exampleMethodDeclarationSyntax = exampleCode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(x => x.Identifier.Value.ToString() == "To");

            var generics = input.MethodsInfo.AddedParameters
                .Select(x =>
                    new
                    {
                        GenericList = GetGenerics.Handle(x.Parameters),
                        ParameterList = GetParameters.Handle(x.Parameters),
                        StatementList = GetBlockStatement.Handle(x.Parameters)
                    })
                .Select(x => (MethodDeclarationSyntax) MethodParametersReWriter.Visit(exampleMethodDeclarationSyntax,
                    new ReWriteMethodInfo(x.ParameterList, x.StatementList, x.GenericList)))
                .ToList();

            var classNode = ClassReWriter.Visit(exampleCode, generics);
            FileWriter.Handle(new WriteFileInfoDto(input.PathToDestinationFile, classNode.ToString()));
        }
    }
}