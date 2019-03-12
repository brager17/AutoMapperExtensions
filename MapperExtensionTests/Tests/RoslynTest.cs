using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            var r = Enumerable.Range(1, 4).Select(x =>
            {
                var maxNumber = Convert.ToInt32(Math.Pow(10, x));
                var generateInfos = Enumerable.Range(0, maxNumber)
                    .Select(xx =>
                    {
                        if (xx.ToString().Any(xxx => int.Parse(xxx.ToString()) >= enumCounts))
                            return null;
                        var str = AddedEmptyElements(xx, maxNumber.ToString().Length - 1);
                        var enumerable = ListTypeEnumByString(str);
                        return enumerable;
                    })
                    .Where(xx => xx != null)
                    .ToList();
                return generateInfos;
            }).SelectMany(x => x.Select((xx, i) =>
            {
                return new GenerateMethodInfo
                {
                    AddedParameters = xx,
                    NewMethodName = "MethodExample" + i,
                    OldMethodName = "MethodExample0"
                };
            }));
            var t = new GenerateMethodsInfo
            {
                MethodsInfo = r,
                PathToExampleCodeFile =
                    @"/home/evgeny/Документы/automapper/MethodGenerator/MethodExample.cs",
                PathToDestinationFile =
                    @"/home/evgeny/Документы/automapper/MethodGenerator/MethodExample1.cs",
            };
            s.Handle(t);
        }

        private string AddedEmptyElements(int item, int length, char defaultSymbol = '0')
        {
            var s = item.ToString();
            var count = s.Length;
            var ss = Enumerable.Range(1, length - count).Select(x => defaultSymbol).Concat(s.ToCharArray());
            var addedEmptyElements = ss.Aggregate("", (a, c) => $"{a}{c}");
            return addedEmptyElements;
        }

        public IEnumerable<TypeEnum> ListTypeEnumByString(string str)
        {
            var t = str.Select(x => x.ToString());
            return str.Select(x => (TypeEnum) int.Parse(x.ToString()));
        }
    }
}