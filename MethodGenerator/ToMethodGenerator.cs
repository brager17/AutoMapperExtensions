using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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

        public ToMethodGenerator()
        {
            MethodParametersReWriter = new ParameterReWriter();
            ClassReWriter = new ClassReWriter();
            fileReader = new FileReader();
            getGenerationCode = new GetGenerationCode();
            fileWriter = new FileWriter();
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = fileReader.Handle(new FileInfoDto() {PathToFile = input.PathToExampleCodeFile});
            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();

            var exampleMethodDeclarationSyntax = exampleCode
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single();

            var methods = input.MethodsInfo.ToList().Select(x =>
                {
                    var nodeOrTokenList = getGenerationCode.Handle(x.AddedParameters);
                    var separatedList = SeparatedList<ParameterSyntax>(nodeOrTokenList);
                    return MethodParametersReWriter.Visit(exampleMethodDeclarationSyntax,
                        new ReWriteMethodInfo
                        {
                            NewName = x.NewMethodName,
                            AddedParameters = ParameterList(separatedList),
                            OldName = x.OldMethodName
                        });
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