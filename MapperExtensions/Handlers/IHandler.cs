namespace MethodGenerator
{
    public interface IHandler<in T>
    {
        void Handle(T input);
    }
    
    public interface IQuery<in T, out R>
    {
        R Handle(T input);
    }
}