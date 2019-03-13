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
        private IQueryHandler<FileInfoDto, string> fileReader { get; set; }
        private IQueryHandler<IEnumerable<TypeEnum>, SyntaxNodeOrTokenList> getGenerationCode { get; set; }
        private IHandler<WriteFileInfoDto> fileWriter { get; set; }

        private ClassReWriter ClassReWriter { get; set; }
        private ParameterReWriter MethodParametersReWriter { get; set; }

        private IQueryHandler<IEnumerable<Parameter>, SeparatedSyntaxList<ExpressionSyntax>> getParameterList
        {
            get;
            set;
        }

        public ToMethodGenerator()
        {
            MethodParametersReWriter = new ParameterReWriter();
            ClassReWriter = new ClassReWriter();
            fileReader = new FileReader();
            getGenerationCode = new GetGenerationMethodParametersList();
            fileWriter = new FileWriter();
            getParameterList = new GetGenerationArrayInitializer();
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = fileReader.Handle(new FileInfoDto() {PathToFile = input.PathToExampleCodeFile});
            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();

            var exampleMethodDeclarationSyntax = exampleCode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Last();
            var methods = input.MethodsInfo.ToList().Select(x =>
                {
                    var nodeOrTokenList = getGenerationCode.Handle(x.AddedParameters);
                    var separatedList = SeparatedList<ParameterSyntax>(nodeOrTokenList);
                    var arrayParameters = getParameterList.Handle(
                        // todo refactoring it
                        // names arg0,arg1,... должны задавать в input сущности GenerateMethodsInfo
                        // либо генерироваться в этом хэндлере, а не в каждом handler'e(getGenerationCode,getGenerationList)
                        Enumerable.Range(0, x.AddedParameters.Count()).Select(
                            xx => new Parameter()
                            {
                                ParameterName = $"arg{xx}"
                            }).ToList());
                    return MethodParametersReWriter.Visit(exampleMethodDeclarationSyntax,
                        new ReWriteMethodInfo(x.OldMethodName, x.NewMethodName, ParameterList(separatedList),
                            arrayParameters));
                })
                .OfType<MethodDeclarationSyntax>();
            var classReWriter = new ClassReWriter();
            var str = classReWriter.Visit(exampleCode, methods).ToString();

            fileWriter.Handle(new WriteFileInfoDto
            {
                Code = str,
                PathToFile = input.PathToDestinationFile
            });
        }
    }
}