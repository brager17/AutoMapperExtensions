using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MapperExtensions.Models;
using NUnit.Framework;

namespace Tests
{
    public class Tests : BaseProfileTests<PupilProfile,Pupil>
    {

        protected override Pupil MockData()
        {
            var mock = new Pupil
            {
                Address = new AddressCard {Id = "1", House = 1, Street = "street", Country = "country", City = "city"},
                Identity = new IdentityCard
                {
                    Passport = new Passport {Name = "name", Number = "123", Surname = "surname"},
                    TIN = new TIN() {Number = "123", DateOfReceiving = new DateTime(1, 2, 3)}
                }
            };
            return mock;
        }


        [Test]
        public void PupilProfile__SimpleMapRules__GetPupilCorrectDto()
        {
            var projectTo = context.Pupils.ProjectTo<PupilDto>();
            Assert.DoesNotThrow(() => projectTo.ToList());
        }
    }
}