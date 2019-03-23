using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MapperExtensions;
using MapperExtensions.Models;
using NUnit.Framework;

namespace Tests
{
    public class Tests : BaseProfileTests<Tests.PupilProfile, Pupil>
    {
        public class PupilProfile : Profile
        {
            public PupilProfile()
            {
                CreateMap<Pupil, PupilDto>()
                    .From(x => x.Address).To()
                    .From(x => x.Identity.Passport)
                    .ToIf(x => x.TitleAge, x => x.Age < 18, x => $"{x.Age}", x => "Adult").To()
                    .From(x => x.EducationCard.StudyGroup)
                    .To((x => x.Group, x => $"The Best : {x.Number}"),
                        (x => x.CountStudentsInGroup, x => x.CountStudents))
                    ;
            }
        }

        protected override Pupil MockData()
        {
           
            var mock = new Pupil
            {
                Address = new AddressCard
                    {House = 123, Street = "Unknown", Country = "Russia", City = "Kazan"},
                Identity = new IdentityCard
                {
                    Passport = new Passport {Name = "Evgeny", Number = "123123123", Surname = "Terekhin", Age = 18},
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
                            Mark = MapperExtensions.Models.Mark.Bad
                        },
                        new Exam
                        {
                            Name = "Physics",
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
            context.Add(MockData());
            context.SaveChanges();
            var projectTo = context.Pupils
                .ProjectTo<PupilDto>().First();

            Assert.AreEqual(18, projectTo.Age);
            Assert.AreEqual("Adult", projectTo.TitleAge);
            Assert.AreEqual("The Best : 11-608", projectTo.Group);
            Assert.AreEqual("11-608", projectTo.Number);
            Assert.AreEqual("Evgeny", projectTo.Name);
            Assert.AreEqual("Terekhin", projectTo.Surname);
            Assert.AreEqual(123, projectTo.House);
            Assert.AreEqual("Unknown", projectTo.Street);
            Assert.AreEqual("Russia", projectTo.Country);
            Assert.AreEqual(9, projectTo.CountStudentsInGroup);
        }

        [Test]
        public void To__AgeGreater18__Adult()
        {
            context.Add(MockData());
            context.SaveChanges();
            var pupilDto = context.Pupils.ProjectTo<PupilDto>().First();
            Assert.AreEqual("Adult", pupilDto.TitleAge);
        }

        [Test]
        public void To__AgeLess18__Adult()
        {
            var data = MockData();
            data.Identity.Passport.Age = 12;
            context.Add(data);
            context.SaveChanges();
            var pupilDto = context.Pupils.ProjectTo<PupilDto>().First();
            Assert.AreEqual("12", pupilDto.TitleAge);
        }
    }
}