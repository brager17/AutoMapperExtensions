using MapperExtensions.Models;
using MethodGenerator;
using NUnit.Framework;

namespace Tests
{
    public class RoslynTest
    {
        private static IQueryHandler<CountArguments, GenerateMethodInfo> GenerateInfoToMethods =>
            new GenerateInfoToMethods();

        [Test]
        public void Test()
        {
            var methodGenerator = new ToMethodGenerator();
            var methodInfos = GenerateInfoToMethods.Handle(new CountArguments(10));

            var t = new GenerateMethodsInfo
            (
                @"/home/evgeny/Документы/automapper/MethodGenerator/MethodExample1.cs",
                @"/home/evgeny/Документы/automapper/MethodGenerator/MethodExample.cs",
                methodInfos
            );
            methodGenerator.Handle(t);
        }
    }
}