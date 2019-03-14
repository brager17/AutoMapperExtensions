namespace MethodGenerator
{
    public class WriteFileInfoDto : FileInfoDto
    {
        public string Code { get; set; }

        public WriteFileInfoDto(string pathToFile, string code) : base(pathToFile)
        {
            Code = code;
        }
    }
}