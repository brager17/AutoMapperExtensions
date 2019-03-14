using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using MapperExtensionTests.Models;
using MethodGenerator;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Tests
{
    public class RoslynTest
    {
        private static IQueryHandler<CountArguments, IEnumerable<GenerateMethodInfo>> GenerateInfoToMethods =>
            new GenerateInfoToMethods();

        [Test]
        public void Test()
        {
            var methodGenerator = new ToMethodGenerator();
            var methodInfos = GenerateInfoToMethods.Handle(new CountArguments(2));

            var t = new GenerateMethodsInfo
            (
                @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample1.cs",
                @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample.cs",
                methodInfos
            );
            methodGenerator.Handle(t);
        }
    }
}