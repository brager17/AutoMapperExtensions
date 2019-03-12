using System.Collections.Generic;

namespace MethodGenerator
{
    public class GenerateMethodInfo
    {
        public string NewMethodName { get; set; }
        public string OldMethodName { get; set; }
        public IEnumerable<TypeEnum> AddedParameters { get; set; }
    }
}