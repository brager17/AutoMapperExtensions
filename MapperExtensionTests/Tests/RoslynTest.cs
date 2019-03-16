using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using MapperExtensions.Models;
using MapperExtensionTests.Models;
using MethodGenerator;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Tests
{
    public class RoslynTest
    {
        private static IQueryHandler<CountArguments, GenerateMethodInfo> GenerateInfoToMethods =>
            new GenerateInfoToMethods();

        [Test]
        public void TEST()
        {
            Expression<Func<Human, string>> expression = x => x != null ? x.AddressCard.Id : "";
            Expression<Func<Family, Human>> exp = x => x.Father;
        }

        [Test]
        public void Test()
        {
            var methodGenerator = new ToMethodGenerator();
            var methodInfos = GenerateInfoToMethods.Handle(new CountArguments(10));

            var t = new GenerateMethodsInfo
            (
                @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample12.cs",
                @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample.cs",
                methodInfos
            );
            methodGenerator.Handle(t);
        }
    }
}