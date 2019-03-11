namespace MethodGenerator
{
    public interface IQueryHandler<in T, out R>
    {
        R Handle(T input);
    }
}