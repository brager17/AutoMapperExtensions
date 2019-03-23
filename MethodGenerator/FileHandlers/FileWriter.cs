using System.IO;

namespace MethodGenerator
{
    public class FileWriter : IHandler<WriteFileInfoDto>
    {
        public void Handle(WriteFileInfoDto input)
        {
            File.WriteAllText($"{input.PathToFile}", input.Code);
        }
    }
}