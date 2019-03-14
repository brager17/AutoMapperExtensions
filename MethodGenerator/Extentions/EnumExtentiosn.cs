using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.Emit;

namespace MethodGenerator.Extentions
{
    public static class EnumExtentiosn
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Enum @enum)
        {
            var attributes = @enum.GetType()
                .GetMembers()
                .Single(x => x.Name == @enum.ToString())
                .GetCustomAttributes(true)
                .Cast<TAttribute>();
            return attributes;
        }
    }
}