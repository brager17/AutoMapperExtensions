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
//            CreateMap<Pupil, PupilDto>()
//                .From(x => x.Address).To()
//                .From(x => x.Identity.Passport)
//                .ToIf(x => x.Name, x => true, Passport.FullFormat, Passport.ShortFormat)
//                .To()
//                .From(x => x.Identity)
//                .To((x => x.Name, x => x.Passport.Name), (x => x.Surname, x => "213"))
//                .From(x => x.EducationCard)
//                .ToIf(x => x.Number,
//                    x => x.Exams.Count < 9,
//                    EducationCard.FullText,
//                    EducationCard.ShortText)
//                .To()
//                ;

//            CreateMap<Pupil, PupilDto>()
//                .From(x => x.Address).To()
//                .From(x => x.Identity).To()
//                .From(x => x.Identity.Passport).To()
//                ;

            CreateMap<Pupil, PupilDto>();
//                .ForMember(x => x.Name, s => s.MapFrom(x => x.Identity.Passport.Name))
//                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Identity.Passport.Surname))
//                .ForMember(x => x.Age, s => s.MapFrom(x => x.Identity.Passport.Age))
//                .ForMember(x => x.Number, s => s.MapFrom(x => x.Identity.Passport.Number))
//                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.House))
//                .ForMember(x => x.Street, s => s.MapFrom(x => x.Address.Street))
//                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.Country))
//                .ForMember(x => x.DateOfReceiving, s => s.MapFrom(x => x.Identity.Tin.DateOfReceiving))
//                ;
        }
    }
}