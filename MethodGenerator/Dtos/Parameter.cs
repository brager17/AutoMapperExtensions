using System.Collections.Generic;

namespace MethodGenerator
{
    public class Parameter:IGenericInfo,IArgumentInfo
    {
        public string GenericName { get; set; }
        public string ArgumentInfo { get; set; }
    }

    public class ToMethodParameter
    {
        public List<Parameter> Parameters { get; set; }
    }
}