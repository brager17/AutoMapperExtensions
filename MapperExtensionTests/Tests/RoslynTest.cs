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
                @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample12.cs",
                @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample.cs",
                methodInfos
            );
            methodGenerator.Handle(t);
        }
    }
}