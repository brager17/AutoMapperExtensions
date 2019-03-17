namespace MethodGenerator
{
    public class GenerateMethodsInfo
    {
        public string PathToDestinationFile { get; }
        public string PathToExampleCodeFile { get; }
        public GenerateMethodInfo MethodsInfo { get; }

        public GenerateMethodsInfo(string pathToDestinationFile, string pathToExampleCodeFile,
            GenerateMethodInfo methodsInfo)
        {
            PathToDestinationFile = pathToDestinationFile;
            PathToExampleCodeFile = pathToExampleCodeFile;
            MethodsInfo = methodsInfo;
        }
    }
}