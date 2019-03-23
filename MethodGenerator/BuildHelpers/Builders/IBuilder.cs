namespace MethodGenerator
{
    public interface IBuilder<TInfo, TNode>
    {
        TNode Build(TInfo info);
    }
}