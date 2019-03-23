using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MapperExtensions.Models;
using NUnit.Framework;

namespace Tests
{
    public class Tests : BaseProfileTests<PupilProfile, Pupil>
    {
        protected override Pupil MockData()
        {
            var mock = new Pupil
            {
                Address = new AddressCard {Id = "1", House = 10, Street = "Grivki", Country = "Russia", City = "Kazan"},
                Identity = new IdentityCard
                {
                    Passport = new Passport {Name = "Evgeny", Number = "First one", Surname = "Terekhin", Age = 12},
                    Tin = new TIN() {Number = "123", DateOfReceiving = new DateTime(1998, 05, 05)}
                },
                EducationCard = new EducationCard
                {
                    Exams = new List<Exam>
                    {
                        new Exam
                        {
                            Name = "Math",
                            Date = DateTime.Now,
                            Mark = MapperExtensions.Models.Mark.Excellent
                        },
                        new Exam
                        {
                            Name = "Fithicks",
                            Date = DateTime.Now,
                            Mark = MapperExtensions.Models.Mark.Bad
                        },
                        new Exam
                        {
                            Name = "English",
                            Date = DateTime.Now,
                            Mark = MapperExtensions.Models.Mark.Bad
                        },
                    },
                    StudyGroup = new StudyGroup
                    {
                        Number = "11-608",
                        CountStudents = 9
                    }
                }
            };
            return mock;
        }


        [Test]
        public void PupilProfile__SimpleMapRules__GetPupilCorrectDto()
        {
            var projectTo = context.Pupils
                    .ProjectTo<PupilDto>()
                    .GroupBy(x => x.House)
                ;
           
            var s = projectTo.First();
            Assert.DoesNotThrow(() => projectTo.ToList());
        }
    }
}