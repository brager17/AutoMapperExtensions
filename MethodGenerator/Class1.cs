using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MethodGenerator
{
    public class ToMethodGenerator : IHandler<MethodGeneratorDto>
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

        public void Handle(MethodGeneratorDto input)
        {
            var exampleMethodCode = fileReader.Handle(new FileInfoDto() {PathToFile = input.PathToExampleCodeFile});
            var exampleCode = CSharpSyntaxTree.ParseText(exampleMethodCode).GetRoot();
            var nodeOrTokenList = getGenerationCode.Handle(input.AddedParameters);
            var separatedList = SeparatedList<ParameterSyntax>(nodeOrTokenList);

            var ReWrittenCode = new ParameterReWriter().Visit(exampleCode, new ReWriteMethodInfo
            {
                NewName = input.NewClassName,
                AddedParameters = ParameterList(separatedList),
                OldName = input.OldClassName
            });

            fileWriter.Handle(new WriteFileInfoDto
            {
                Code = ReWrittenCode.NormalizeWhitespace().ToString(),
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

    public class MethodGeneratorDto : IReWriteMethodInfo
    {
        public string NewClassName { get; set; }
        public string OldClassName { get; set; }
        public string PathToDestinationFile { get; set; }
        public string PathToExampleCodeFile { get; set; }
        public IEnumerable<TypeEnum> AddedParameters { get; set; }
    }

    public interface IReWriteMethodInfo
    {
        string NewClassName { get; set; }
        string OldClassName { get; set; }
        IEnumerable<TypeEnum> AddedParameters { get; set; }
    }
}