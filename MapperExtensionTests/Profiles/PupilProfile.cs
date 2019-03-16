using System.Xml.Linq;
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
                .From(x => x.Identity)
                .ToIf(x => x.Number, x => x.Id != "", IdentityCard.ShortFormat, IdentityCard.FullFormat)
                .From(x => x.Identity.Passport).To()
                .From(x => x.EducationCard).To()
                ;

//            CreateMap<Pupil, PupilDto>()
//                .From(x => x.Address).To()
//                .From(x => x.Identity).To()
//                .From(x => x.Identity.Passport).To()
//                ;
//
            CreateMap<Pupil, PupilDto>()
                .ForMember(x => x.Name, s => s.MapFrom(x => true ?x.Identity.Passport.Name:""))
                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Identity.Passport.Surname))
                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.House))
                .ForMember(x => x.Street, s => s.MapFrom(x => x.Address.Street))
                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.Country))
                .ForMember(x => x.Number, s => s.MapFrom(x => x.Identity.Passport.Number))
                ;
        }
    }
}