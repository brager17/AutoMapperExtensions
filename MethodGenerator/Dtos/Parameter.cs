using System.Collections.Generic;

namespace MethodGenerator
{
    public class Parameter
    {
        public string GenericName { get; set; }
        public string Argument { get; set; }

    }

    public class ToMethodParameter
    {
        public List<Parameter> Parameters { get; set; }
    }
}