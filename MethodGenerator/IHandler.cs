namespace MethodGenerator
{
    public interface IHandler<in T>
    {
        void Handle(T input);
    }
}