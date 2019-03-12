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
    public class ToMethodGenerator : IHandler<GenerateMethodsInfo>
    {
        private IQueryHandler<FileInfoDto, string> fileReader { get; set; }
        private IQueryHandler<IEnumerable<TypeEnum>, SyntaxNodeOrTokenList> getGenerationCode { get; set; }
        private IHandler<WriteFileInfoDto> fileWriter { get; set; }

        public ToMethodGenerator()
        {
            fileReader = new FileReader();
            getGenerationCode = new GetGenerationCode();
            fileWriter = new FileWriter();
        }

        public void Handle(GenerateMethodsInfo input)
        {
            var exampleMethodCode = fileReader.Handle(new FileInfoDto() {PathToFile = input.PathToExampleCodeFile});
            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();


            var methods =
                input.MethodsInfo.ToList().Select(x =>
                {
                    var nodeOrTokenList = getGenerationCode.Handle(x.AddedParameters);
                    var separatedList = SeparatedList<ParameterSyntax>(nodeOrTokenList);
                    return new ParameterReWriter().Visit(exampleCode, new ReWriteMethodInfo
                    {
                        NewName = x.NewMethodName,
                        AddedParameters = ParameterList(separatedList),
                        OldName = x.OldMethodName
                    });
                });
            var str = methods.Aggregate(string.Empty, (a, c) => $"{a}\n\n{c.NormalizeWhitespace().ToString()}");

            fileWriter.Handle(new WriteFileInfoDto
            {
                Code = str,
                PathToFile = input.PathToDestinationFile
            });
        }
    }

    public class FileWriter : IHandler<WriteFileInfoDto>
    {
        public void Handle(WriteFileInfoDto input)
        {
            File.WriteAllText($"{input.PathToFile}", input.Code);
        }
    }

    public class FileReader : IQueryHandler<FileInfoDto, string>
    {
        public string Handle(FileInfoDto input)
        {
            return File.ReadAllText($"{input.PathToFile}");
        }
    }

    public class FileInfoDto
    {
        public string PathToFile { get; set; }
    }

    public class WriteFileInfoDto : FileInfoDto
    {
        public string Code { get; set; }
    }

    public class GenerateMethodsInfo
    {
        public string PathToDestinationFile { get; set; }
        public string PathToExampleCodeFile { get; set; }
        public IEnumerable<GenerateMethodInfo> MethodsInfo { get; set; }
    }


    public class GenerateMethodInfo
    {
        public string NewMethodName { get; set; }
        public string OldMethodName { get; set; }
        public IEnumerable<TypeEnum> AddedParameters { get; set; }
    }
}