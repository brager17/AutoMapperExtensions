using System.Reflection;

namespace MapperExtensions
{
    public interface ICachedMethodInfo
    {
        MethodInfo GetMethod(string name);
    }
}