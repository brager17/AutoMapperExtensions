namespace MethodGenerator
{
    public class FileInfoDto
    {
        public FileInfoDto(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        public string PathToFile { get; set; }
    }
}