using System.Collections.Generic;

namespace MapperExtensions.Models
{
    public class MapPropertyInfo
    {
        public     MapPropertyInfo(string destinationPropertyName, IEnumerable<string> pathToSourceProperty)
        {
            DestinationPropertyName = destinationPropertyName;
            PathToSourceProperty = pathToSourceProperty;
        }

        public string DestinationPropertyName { get; }
        public IEnumerable<string> PathToSourceProperty { get;  }
    }
}