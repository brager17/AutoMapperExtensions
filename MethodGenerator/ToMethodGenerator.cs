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
        private readonly ParameterReWriter _methodParametersReWriter;
        private readonly ClassReWriter _classReWriter;
        private readonly IQuery<FileInfoDto, string> _fileReader;
        private readonly IHandler<WriteFileInfoDto> _fileWriter;
        private readonly IQuery<IEnumerable<Parameter>, ParameterListSyntax> _getParametersQuery;
        private readonly IQuery<IEnumerable<IGenericInfo>, TypeParameterListSyntax> _getGenericsQuery;
        private readonly IQuery<IEnumerable<IArgumentInfo>, BlockSyntax> _getBlockStatementQuery;


        public ToMethodGenerator(
            ParameterReWriter methodParametersReWriter,
            ClassReWriter classReWriter,
            IQuery<FileInfoDto, string> fileReader,
            IHandler<WriteFileInfoDto> fileWriter,
            IQuery<IEnumerable<Parameter>, ParameterListSyntax> getParametersQuery,
            IQuery<IEnumerable<IGenericInfo>, TypeParameterListSyntax> getGenericsQuery,
            IQuery<IEnumerable<IArgumentInfo>, BlockSyntax> getBlockStatementQuery)
        {
            _methodParametersReWriter = methodParametersReWriter;
            _classReWriter = classReWriter;
            _fileReader = fileReader;
            _fileWriter = fileWriter;
            _getParametersQuery = getParametersQuery;
            _getGenericsQuery = getGenericsQuery;
            _getBlockStatementQuery = getBlockStatementQuery;
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = _fileReader.Handle(new FileInfoDto(input.PathToExampleCodeFile));

            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();

            var exampleMethodDeclarationSyntax = exampleCode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(x => x.Identifier.Value.ToString() == "To");

            var generics = input.MethodsInfo.AddedParameters
                .Select(x =>
                    new
                    {
                        GenericList = _getGenericsQuery.Handle(x.Parameters),
                        ParameterList = _getParametersQuery.Handle(x.Parameters),
                        StatementList = _getBlockStatementQuery.Handle(x.Parameters)
                    })
                .Select(x => (MethodDeclarationSyntax) _methodParametersReWriter.Visit(exampleMethodDeclarationSyntax,
                    new ReWriteMethodInfo(x.ParameterList, x.StatementList, x.GenericList)))
                .ToList();

            var classNode = _classReWriter.Visit(exampleCode, generics).NormalizeWhitespace();
            _fileWriter.Handle(new WriteFileInfoDto(input.PathToDestinationFile, classNode.ToString()));
        }
    }
}