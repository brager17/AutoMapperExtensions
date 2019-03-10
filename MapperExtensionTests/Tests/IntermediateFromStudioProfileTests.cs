using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using MapperExtensions.Models;
using MapperExtensionTests.Models;
using NUnit.Framework;
using Tests.Models.StudioModels;

namespace Tests
{
    public class IntermediateFromStudioProfileTests : BaseProfileTests<FamilyProfile, Family>
    {
        protected override Family MockData()
        {
            return new Family
            {
                Father = new Human
                {
                    AddressCard = new AddressCard
                    {
                        Id = "Id", City = "fatherCity", House = 1, Street = "fatherStreet", Country = "fatherCountry"
                    },
                    IdentityCard = new IdentityCard
                    {
                        Id = "id",
                        Passport = new Passport {Age = 9, Name = "fatherName", Number = "1", Surname = "fatherSurname"},
                        Tin = new TIN {Number = "fatherTIN", DateOfReceiving = DateTime.Now}
                    }
                },
                Mother = new Human
                {
                    AddressCard = new AddressCard
                    {
                        Id = "Id", City = "city", House = 1, Street = "street", Country = "country"
                    },
                    IdentityCard = new IdentityCard
                    {
                        Id = "id",
                        Passport = new Passport {Age = 9, Name = "momName", Number = "1", Surname = "momSurname"},
                        Tin = new TIN {Number = "MotherTIN", DateOfReceiving = DateTime.Now}
                    }
                },
                Children = new List<Human>() { }
            };
        }

        [Test]
        public void Test()
        {
            var family = context.Families.First();
            var projectTo = context.Families.ProjectTo<FatherDto>();
            var sssss = projectTo.First();
            Assert.DoesNotThrow(() => projectTo.ToList());
        }

    }
}