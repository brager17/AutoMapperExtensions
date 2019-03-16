using System;
using System.Collections.Generic;
using System.Linq;
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
                Address = new AddressCard {Id = "1", House = 1, Street = "street", Country = "country", City = "city"},
                Identity = new IdentityCard
                {
                    Passport = new Passport {Name = "name", Number = "123", Surname = "surname"},
                    Tin = new TIN() {Number = "123", DateOfReceiving = new DateTime(1, 2, 3)}
                },
                EducationCard = new EducationCard
                {
                    Exams = new List<Exam>
                    {
                        new Exam
                        {
                            Name = "exam1",
                            Date = DateTime.Now,
                            Mark = Mark.Excellent
                        },
                        new Exam
                        {
                            Name = "exam2",
                            Date = DateTime.Now,
                            Mark = Mark.Bad
                        },
                        new Exam
                        {
                            Name = "exam3",
                            Date = DateTime.Now,
                            Mark = Mark.Bad
                        },
                    },
                    StudyGroup = new StudyGroup
                    {
                        Number = "11",
                        CountStudents = 20
                    }
                }
            };
            return mock;
        }


        [Test]
        public void PupilProfile__SimpleMapRules__GetPupilCorrectDto()
        {
            var projectTo = context.Pupils.ProjectTo<PupilDto>();
            var pupil = projectTo.First();
            Assert.DoesNotThrow(() => projectTo.ToList());
        }
    }
}