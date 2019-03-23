using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MapperExtensions
{
    public class CachedMethodInfo : ICachedMethodInfo
    {
        Dictionary<string, MethodInfo> _dic = new Dictionary<string, MethodInfo>
        {
            {
                nameof(HelpersMethod.GetRuleByConvention).Split('.').Last(),
                typeof(HelpersMethod).GetMethod(nameof(HelpersMethod.GetRuleByConvention).Split('.').Last())
            },
        };

        public MethodInfo GetMethod(string name)
        {
            return _dic.FirstOrDefault(x => x.Key == name).Value 
                   ?? throw new ArgumentException($"Нет информации о методе {name}");
        }
    }
}