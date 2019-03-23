using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using MapperExtensions.Models;
using MethodGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace Tests
{
    public class RoslynTest
    {
        private GenerateManager _generateManager { get; set; }
        private FileReader _fileReader { get; set; }

        [SetUp]
        public void Setup()
        {
            _generateManager = new GenerateManager();
            _fileReader = new FileReader();
        }

        [Test]
        public void Test()
        {
            var count = new CountArguments(10);
            _generateManager.Handle(count);
            var text = _fileReader.Handle(new FileInfoDto(((IHasDestFileInfo)_generateManager)._pathToDestinationFile));
            var toOverloadsCount = CSharpSyntaxTree.ParseText(text).GetRoot()
                .DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Count(x => x.Identifier.ValueText == "To");
            Assert.AreEqual(toOverloadsCount, count.ArgumentCount);
        }
    }
}