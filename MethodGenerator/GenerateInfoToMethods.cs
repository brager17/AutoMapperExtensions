using System.Linq;

namespace MethodGenerator
{
    public class CountArguments
    {
        public CountArguments(int argumentCount)
        {
            ArgumentCount = argumentCount;
        }

        public int ArgumentCount { get; }
    }

    public class GenerateInfoToMethods : IQuery<CountArguments, GenerateMethodInfo>
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
                            Parameters = Enumerable.Range(1, i)
                                .Select(ii => new Parameter {ArgumentInfo = $"arg{ii}", GenericName = $"T{ii}"})
                                .ToList()
                        };
                    })
            };
            return list;
        }
    }
}