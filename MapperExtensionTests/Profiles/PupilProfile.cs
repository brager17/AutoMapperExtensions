using AutoMapper;

namespace MapperExtensions.Models
{
    public class PupilProfile : Profile
    {
        public PupilProfile()
        {
//            CreateMap<Pupil, PupilDto>()
//                .From(x => x.Address).To(x => x.House, x => x.Country, x => x.Street)
//                .From(x => x.Identity).To(x => x.Id)
//                .From(x => x.Identity.Passport).To(x => x.Name, x => x.Surname, s => s.Number)
//                ;

            CreateMap<Pupil, PupilDto>()
                .From(x => x.Address).To()
                .From(x => x.Identity).To()
                .From(x => x.Identity.Passport).To()
                ;

//            CreateMap<Pupil, PupilDto>()
//                .ForMember(x => x.Name, s => s.MapFrom(x => x.Identity.Passport.Name))
//                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Identity.Passport.Surname))
//                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.House))
//                .ForMember(x => x.Street, s => s.MapFrom(x => x.Address.Street))
//                .ForMember(x => x.House, s => s.MapFrom(x => x.Address.Country))
//                .ForMember(x => x.Number, s => s.MapFrom(x => x.Identity.Passport.Number))
//                .ForMember(x => x.DateOfReceiving, s => s.MapFrom(x => x.Identity.TIN.DateOfReceiving))
//                ;
        }
    }
}