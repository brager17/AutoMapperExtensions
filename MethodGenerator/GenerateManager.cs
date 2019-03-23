using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public interface IHasDestFileInfo
    {
        string _pathToDestinationFile { get; }
    }

    public class GenerateManager : IHandler<CountArguments>,IHasDestFileInfo
    {
        private IQuery<CountArguments, GenerateMethodInfo> GenerateInfoToMethods { get; set; }

        private IQuery<FileInfoDto, string> FileReader { get; set; }
        private ParameterReWriter MethodParametersReWriter { get; set; }
        private ClassReWriter ClassReWriter { get; set; }
        private IHandler<WriteFileInfoDto> FileWriter { get; set; }
        private IQuery<IEnumerable<Parameter>, ParameterListSyntax> GetParameters { get; set; }
        private IQuery<IEnumerable<IGenericInfo>, TypeParameterListSyntax> GetGenerics { get; set; }
        private IQuery<IEnumerable<IArgumentInfo>, BlockSyntax> GetBlockStatement { get; set; }

        private IHandler<GenerateMethodsInfo> ToMethodGenerator { get; set; }

        private string _pathToExampleCodeFile => @"..\..\..\..\MapperExtensions\MapperExtensions.cs";

        public string _pathToDestinationFile => @"..\..\..\..\MapperExtensions\ToOverloads.cs";

        public GenerateManager()
        {
            GenerateInfoToMethods = new GenerateInfoToMethods();
            FileReader = new FileReader();
            ClassReWriter = new ClassReWriter();
            MethodParametersReWriter = new ParameterReWriter();
            FileWriter = new FileWriter();
            GetParameters = new GetNodeStructure<Parameter, ParameterSyntax, ParameterListSyntax>(
                new GetParameterListSyntax(new ConcatSyntaxNodeOrToken<ParameterSyntax>()),
                new ParameterExpressionBuilder());
            GetGenerics = new GetNodeStructure<IGenericInfo, TypeParameterSyntax, TypeParameterListSyntax>(
                new GetTypeParameterListSyntax(new ConcatSyntaxNodeOrToken<TypeParameterSyntax>()),
                new GenericTypesBuilder());
            GetBlockStatement =
                new GetNodeStructure<IArgumentInfo, StatementSyntax, BlockSyntax>(new GetBlockStructure(),
                    new StatementBuilder());
            ToMethodGenerator = new ToMethodGenerator(MethodParametersReWriter, ClassReWriter, FileReader, FileWriter,
                GetParameters, GetGenerics, GetBlockStatement);
        }

        public void Handle(CountArguments input)
        {
            var methodInfos = GenerateInfoToMethods.Handle(input);
            var generateInfo = new GenerateMethodsInfo(_pathToDestinationFile, _pathToExampleCodeFile, methodInfos);
            ToMethodGenerator.Handle(generateInfo);
        }
    }
}