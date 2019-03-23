using System.Collections.Generic;

namespace MethodGenerator
{
    public interface IGetStructure<TNode, TOut>
    {
        TOut GetStructure(IEnumerable<TNode> input);
    }
}