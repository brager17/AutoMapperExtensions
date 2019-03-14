using System;
using System.Collections.Generic;
using System.Linq;

namespace MethodGenerator
{
    public class CountArguments
    {
        public CountArguments(int argumentCount)
        {
            ArgumentCount = argumentCount;
        }
        public int ArgumentCount { get; set; }
    }
    public class GenerateInfoToMethods : IQueryHandler<CountArguments, IEnumerable<GenerateMethodInfo>>
    {
        private static int EnumLength => Enum.GetValues(typeof(TypeEnum)).Length;

        public GenerateInfoToMethods()
        {
        }

        public IEnumerable<GenerateMethodInfo> Handle(CountArguments input)
        {
            var result = Enumerable.Range(1, input.ArgumentCount).Select(x =>
                {
                    var maxNumber = Convert.ToInt32(Math.Pow(10, x));
                    var numberLength = maxNumber.ToString().Length - 1;
                    return Enumerable.Range(0, maxNumber).Select(xx =>
                            NoDigitGreaterEnumLength(xx)
                                ? null
                                : ListTypeEnumByString(AddedEmptyElements(xx, numberLength)))
                        .Where(xx => xx != null);
                })
                .SelectMany(x => x.Select((xx, i) => new GenerateMethodInfo(MakeToMethodParameters(xx))));
            return result;
        }

        private string AddedEmptyElements(int item, int length, char defaultSymbol = '0')
        {
            var str = item.ToString();
            var count = str.Length;
            var addedStr = Enumerable.Range(1, length - count).Select(x => defaultSymbol).Concat(str.ToCharArray());
            var addedEmptyElements = addedStr.Aggregate(string.Empty, (a, c) => $"{a}{c}");
            return addedEmptyElements;
        }

        private IEnumerable<ToMethodParameter> MakeToMethodParameters(IEnumerable<TypeEnum> enumerable)
        {
            return enumerable.Select((xxx, ii) => new ToMethodParameter($"arg{ii}", xxx));
        }

        private bool NoDigitGreaterEnumLength(int number)
        {
            return number.ToString().Any(xxx => int.Parse(xxx.ToString()) >= EnumLength);
        }

        private IEnumerable<TypeEnum> ListTypeEnumByString(string str)
        {
            return str.Select(x => (TypeEnum) int.Parse(x.ToString()));
        }
    }
}