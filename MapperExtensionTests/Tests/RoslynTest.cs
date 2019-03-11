using System.Collections.Generic;
using MethodGenerator;
using NUnit.Framework;

namespace Tests
{
    public class RoslynTest
    {
        [Test]
        public void Test()
        {
            var s = new MethodGenerator.ToMethodGenerator();
            var parameters = new List<TypeEnum>()
            {
                TypeEnum.Int, TypeEnum.Bool, TypeEnum.Float, TypeEnum.Double, TypeEnum.String, TypeEnum.DateTime
            };
            s.Handle(new MethodGeneratorDto()
            {
                PathToExampleCodeFile =
                    @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample.cs",
                PathToDestinationFile =
                    @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample1.cs",
                AddedParameters = parameters,
                NewClassName = "MethodExample1",
                OldClassName = "MethodExample",
                
            });
        }
    }
}