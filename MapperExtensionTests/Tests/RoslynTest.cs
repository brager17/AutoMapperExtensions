using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using MapperExtensionTests.Models;
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
            var r = Enumerable.Range(1, 2).Select(x =>
                {
                    var maxNumber = Convert.ToInt32(Math.Pow(10, x));
                    return Enumerable.Range(0, maxNumber).Select(xx =>
                    {
                        return xx.ToString().Any(xxx => int.Parse(xxx.ToString()) >= enumCounts)
                            ? null
                            : ListTypeEnumByString(AddedEmptyElements(xx, maxNumber.ToString().Length - 1));
                    }).Where(xx => xx != null);
                })
                .SelectMany(x => x.Select((xx, i) => new GenerateMethodInfo
                {
                    AddedParameters = xx,
                    NewMethodName = "MethodExample" + i,
                    OldMethodName = "MethodExample0"
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


        private IEnumerable<TypeEnum> ListTypeEnumByString(string str)
        {
            return str.Select(x => (TypeEnum) int.Parse(x.ToString()));
        }
    }
}