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
            var s = new MethodGenerator.MethodGenerator();
            var input = new List<TypeEnum>()
            {
                TypeEnum.Int, TypeEnum.Bool, TypeEnum.Float, TypeEnum.Double, TypeEnum.String, TypeEnum.DateTime
            };
            s.Handle(input);
        }
    }
}