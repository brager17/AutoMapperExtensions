using System.Collections.Generic;

namespace MethodGenerator
{
    public class GenerateMethodsInfo
    {
        public string PathToDestinationFile { get; set; }
        public string PathToExampleCodeFile { get; set; }
        public IEnumerable<GenerateMethodInfo> MethodsInfo { get; set; }

        public GenerateMethodsInfo(string pathToDestinationFile, string pathToExampleCodeFile, IEnumerable<GenerateMethodInfo> methodsInfo)
        {
            PathToDestinationFile = pathToDestinationFile;
            PathToExampleCodeFile = pathToExampleCodeFile;
            MethodsInfo = methodsInfo;
        }
    }
}