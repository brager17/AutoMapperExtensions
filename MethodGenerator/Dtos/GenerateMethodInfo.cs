using System.Collections.Generic;

namespace MethodGenerator
{
    public class GenerateMethodInfo
    {
        public IEnumerable<ToMethodParameter> AddedParameters { get; set; }

        public GenerateMethodInfo(IEnumerable<ToMethodParameter> addedParameters)
        {
            AddedParameters = addedParameters;
        }
    }
}