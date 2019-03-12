using System;
using System.Collections.Generic;
using System.Linq;
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

            var enumCounts = Enum.GetValues(typeof(TypeEnum)).Length;
            var r = Enumerable.Range(1, enumCounts).Select(x =>
            {
                var generateInfos = Enumerable.Range(1,
                        Convert.ToInt32(Math.Pow(x, 10)))
                    .Select(xx =>
                    {
                        var str = AddedEmptyElements(xx, 6);
                        var enumerable = ListTypeEnumByString(str);
                        return enumerable;
                    });
                return generateInfos.SelectMany(xx => xx);
            }).SelectMany((x, i) => new []{new GenerateMethodInfo
            {
                AddedParameters = x,
                NewMethodName = "MethodExample" + i,
                OldMethodName = "MethodExample0"
            }}).Aggregate(new List<GenerateMethodInfo>(),(a,c)=>a.Prepend(c).ToList());

            var t = new GenerateMethodsInfo
            {
                MethodsInfo =r,
                PathToExampleCodeFile =
                    @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample.cs",
                PathToDestinationFile =
                    @"C:\Users\evgeniy\RiderProjects\MapperExtensionTests\MethodGenerator\MethodExample1.cs",
            };
            s.Handle(t);
        }

        private string AddedEmptyElements(int item, int length)
        {
            var s = item.ToString();
            var count = s.Length;
            var ss = Enumerable.Range(1, length - count).Select(x => '').Concat(s.ToCharArray());
            var addedEmptyElements = ss.Aggregate("", (a, c) => $"{a}{c}");
            return addedEmptyElements;
        }

        public IEnumerable<TypeEnum> ListTypeEnumByString(string str)
        {
            return str.Select(x => (TypeEnum) int.Parse(x.ToString()));
        }
    }
}