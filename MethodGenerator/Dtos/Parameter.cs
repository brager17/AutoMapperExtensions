namespace MethodGenerator
{
    public class Parameter
    {
        public string ParameterName { get; set; }

        public Parameter(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
    
    public class ToMethodParameter : Parameter
    {
        public TypeEnum TypeEnum { get; set; }

        public ToMethodParameter(string parameterName, TypeEnum typeEnum) : base(parameterName)
        {
            TypeEnum = typeEnum;
        }
    }
}