using MapperExtensions.Models;
using NUnit.Framework;

namespace Tests.FunctionTests
{
    public class Model
    {
        public SubModel Submodel { get; set; }
    }

    public class SubModel
    {
        public SubSubModel Subsubmodel { get; set; }
        public string SubModelName { get; set; }
    }

    public class SubSubModel
    {
        public string Name { get; set; }
    }

    [TestFixture]
    public class AutoMapperTests
    {
        [Test]
        public void PropertyMapperTestPositive()
        {
            var mock = new Model
            {
                Submodel = new SubModel
                {
                    Subsubmodel = new SubSubModel
                    {
                        Name = "SubSubModelName"
                    },
                    SubModelName = "SubModelName"
                }
            };
            var propertyType = AuthMapperExtensionHelpers.PropertyMapper<Model>("SubmodelSubsubmodelName");
            Assert.AreEqual(mock.Submodel.Subsubmodel.Name, propertyType.Compile().Invoke(mock));
        }

        [Test]
        public void PropertyMapperTestNegative()
        {
            var mock = new Model
            {
                Submodel = new SubModel
                {
                    Subsubmodel = new SubSubModel
                    {
                        Name = "SubSubModelName"
                    },
                    SubModelName = "SubModelName"
                }
            };
            var propertyType = AuthMapperExtensionHelpers.PropertyMapper<Model>("SubmodelubsubmodelName");
            Assert.IsNull(propertyType);
        }
    }
}