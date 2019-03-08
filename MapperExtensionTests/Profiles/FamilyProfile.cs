using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using AutoMapper;
using MapperExtensionTests.Models;

namespace MapperExtensions.Models
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FatherDto>()
                .From(x => x.Father.IdentityCard.Passport).To()
                .From(x => x.Father.IdentityCard)
                .To((x => x.PassportNumber, x => x.Passport.Number), (x => x.TinNumber, x => x.TIN.Number))
                ;
            //

//            CreateMap<Family, FatherDto>()
//                .ForMember(x => x.Name, s => s.MapFrom(x => x.Father.IdentityCard.Passport.Name))
//                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Father.IdentityCard.Passport.Surname))
//                .ForMember(x => x.Age, s => s.MapFrom(x => x.Father.IdentityCard.Passport.Age))
//                .ForMember(x => x.TinNumber, s => s.MapFrom(x => x.Father.IdentityCard.TIN.Number))
//                .ForMember(x => x.BornCity, s => s.MapFrom(x => x.Father.AddressCard.City))
//                .ForMember(x => x.BornCountry, s => s.MapFrom(x => x.Father.AddressCard.Country))
//                .ForMember(x => x.WifeName,
//                    s => s.MapFrom(x => x.Mother.IdentityCard.Passport.Name))
//                .ForMember(x => x.WifeSurname,
//                    s => s.MapFrom(x => x.Mother.IdentityCard.Passport.Surname));
        }
    }
}