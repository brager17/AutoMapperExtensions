using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public class ToMethodGenerator : IHandler<GenerateMethodsInfo>
    {
        private IQueryHandler<FileInfoDto, string> FileReader { get; set; }
        private IQueryHandler<IEnumerable<ToMethodParameter>, SyntaxNodeOrTokenList> GetGenerationCode { get; set; }
        private IHandler<WriteFileInfoDto> FileWriter { get; set; }

        private ClassReWriter ClassReWriter { get; set; }
        private ParameterReWriter MethodParametersReWriter { get; set; }

        private IQueryHandler<IEnumerable<Parameter>, SeparatedSyntaxList<ExpressionSyntax>> GetParameterList { get; }

        public ToMethodGenerator()
        {
            MethodParametersReWriter = new ParameterReWriter();
            ClassReWriter = new ClassReWriter();
            FileReader = new FileReader();
            GetGenerationCode = new GetGenerationMethodParametersList();
            FileWriter = new FileWriter();
            GetParameterList = new GetGenerationArrayInitializer();
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = FileReader.Handle(new FileInfoDto(input.PathToExampleCodeFile));
            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();

            var exampleMethodDeclarationSyntax = exampleCode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Last();

            var methods = input.MethodsInfo.ToList().Select(x =>
                {
                    // method parameter generation
                    var nodeOrTokenList = GetGenerationCode.Handle(x.AddedParameters);
                    var separatedList = SeparatedList<ParameterSyntax>(nodeOrTokenList);
                    // generation of array 'parameters' initializer
                    var arrayParameters = GetParameterList.Handle(x.AddedParameters);
                    return MethodParametersReWriter.Visit(exampleMethodDeclarationSyntax,
                        new ReWriteMethodInfo(ParameterList(separatedList), arrayParameters));
                })
                .OfType<MethodDeclarationSyntax>();
            var code = ClassReWriter.Visit(exampleCode, methods).ToString();
            FileWriter.Handle(new WriteFileInfoDto(input.PathToDestinationFile, code));
        }
    }
}