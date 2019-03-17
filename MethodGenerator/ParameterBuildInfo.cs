namespace MethodGenerator
{
   

    public interface IGenericInfo
    {
        string GenericName { get; set; }
    }

    public interface IArgumentInfo
    {
        string ArgumentInfo { get; set; }
    }
    

    public class GenericInfo : IGenericInfo
    {
        public string GenericName { get; set; }
    }
}