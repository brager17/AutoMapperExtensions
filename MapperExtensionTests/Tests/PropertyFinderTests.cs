using System.Linq;
using System.Text.RegularExpressions;
using MapperExtensions;
using MapperExtensions.Models;
using NUnit.Framework;

namespace Tests
{
    public class NotMark
    {
        
    }
    public class Hard
    {
        public NotMark It { get; set; }
        public Mark ItSmark123 { get; set; }
    }

    public class Mark
    {
        public StudyGroup1 StudyGroup { get; set; }
    }

    public class StudyGroup1
    {
        public string GroupNumber { get; set; }
    }

    public class PropertyFinderTests
    {
        [Test]
        public void FinderTest()
        {
            var test = new Hard();
            var str = ("ItSmark123StudyGroupGroupNumber");
            var matches = Regex.Matches(str, @"[A-Z][\w]+").Select(xx => xx.Value).ToList();
            var actual = HelpersMethod.AdvanceMapProperty(matches, typeof(Hard));
            var expected = new[] {"ItSmark123", "StudyGroup", "GroupNumber"};
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}