//using MapperExtensions.Models;
//using NUnit.Framework;
//
//namespace Tests.FunctionTests
//{
//    public class Model
//    {
//        public SubModel Submodel { get; set; }
//    }
//
//    public class SubModel
//    {
//        public SubSubModel Subsubmodel { get; set; }
//        public string SubModelName { get; set; }
//    }
//
//    public class SubSubModel
//    {
//        public string Name { get; set; }
//    }
//
//    [TestFixture]
//    public class AutoMapperTests
//    {
//        [Test]
//        public void PropertyMapperTestPositive()
//        {
//            var mock = new Model
//            {
//                Submodel = new SubModel
//                {
//                    Subsubmodel = new SubSubModel
//                    {
//                        Name = "SubSubModelName"
//                    },
//                    SubModelName = "SubModelName"
//                }
//            };
//          
//            Assert.AreEqual(mock.Submodel.Subsubmodel.Name, propertyType.Compile().Invoke(mock));
//        }
//
//        [Test]
//        public void PropertyMapperTestNegative()
//        {
//            var mock = new Model
//            {
//                Submodel = new SubModel
//                {
//                    Subsubmodel = new SubSubModel
//                    {
//                        Name = "SubSubModelName"
//                    },
//                    SubModelName = "SubModelName"
//                }
//            };
//            var propertyType = AuthMapperExtensionHelpers.PropertyMapper<Model>("SubmodelubsubmodelName");
//            Assert.IsNull(propertyType);
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class Test
{
    [Test]
    public void Test1()
    {
        int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] arr2 = { 6, 7, 8,11, 9 };

        IEnumerable<int> nums = arr1.Union<int>(arr2);

        foreach (int i in nums)
            Console.WriteLine(i);
    }
}