using System;
using System.Linq.Expressions;
using AutoMapper;
using MethodGenerator;

namespace MapperExtensions.Models
{
    public class PupilProfile : Profile
    {
        public PupilProfile()
        {
            CreateMap<Pupil, PupilDto>()
                .From(x => x.Address).To()
                .From(x => x.Identity.Passport).To()
                .From(x => x.EducationCard.StudyGroup)
                .To((x => x.Group, x => $"The Best : {x.Number}"), (x => x.CountStudentsInGroup, x => x.CountStudents))
                ;

//            CreateMap<Pupil, PupilDto>()
//                .From(x => x.Address).To()
//                .From(x => x.Identity).To()
//                .From(x => x.Identity.Passport).To()
//                ;

//            CreateMap<Pupil, PupilDto>()
//                .ForMember(x => x.Name, s => s.MapFrom(x => x.Identity.Passport.Name))
//                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Identity.Passport.Surname))
//                .ForMember(x => x.Age, s => s.MapFrom(x => x.Identity.Passport.Age))
//                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.House))
//                .ForMember(x => x.Street, s => s.MapFrom(x => x.Address.Street))
//                .ForMember(x => x.Country, s => s.MapFrom(x => x.Address.Country))
//                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Identity.Passport.Age))
//                .ForMember(x => x.Group, s => s.MapFrom(x => x.EducationCard.StudyGroup.Number));
////                ;
        }
    }
}