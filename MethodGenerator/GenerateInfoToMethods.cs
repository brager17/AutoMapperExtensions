using System.Linq;

namespace MethodGenerator
{
    public class CountArguments
    {
        public CountArguments(int argumentCount)
        {
            ArgumentCount = argumentCount;
        }

        public int ArgumentCount { get;  }
    }

    public class GenerateInfoToMethods : IQueryHandler<CountArguments, GenerateMethodInfo>
    {
        public GenerateMethodInfo Handle(CountArguments input)
        {
            var list = new GenerateMethodInfo
            {
                AddedParameters = Enumerable.Range(1, input.ArgumentCount)
                    .Select(i =>
                    {
                        return new ToMethodParameter
                        {
                            Parameters = Enumerable.Range(0, i)
                                .Select(ii => new Parameter {Argument = $"arg{ii}", GenericName = $"T{ii}"}).ToList()
                        };
                    })
            };
            return list;
        }
    }
}