using System.IO;

namespace MethodGenerator
{
    public class FileReader : IQueryHandler<FileInfoDto, string>
    {
        public string Handle(FileInfoDto input)
        {
            return File.ReadAllText($"{input.PathToFile}");
        }
    }
}