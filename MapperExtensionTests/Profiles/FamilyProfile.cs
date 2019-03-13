using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using AutoMapper;
using MapperExtensionTests.Models;
using MethodGenerator;

namespace MapperExtensions.Models
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FatherDto>()
                .From(x=>x.Father.IdentityCard.Passport).To((x=>x.Number,x=>x.Surname))
                .From(x => x.Father.IdentityCard).To()
                .From(x => x.Father.AddressCard).To()
                .From(x => x.Mother.IdentityCard.Passport)
                .To((x => x.WifeName, x => x.Name), (x => x.WifeSurname, x => x.Surname))
                ;
            //

//            CreateMap<Family, FatherDto>()
//                .ForMember(x => x.Name, s => s.MapFrom(x => x.Father.IdentityCard.Passport.Name))
//                .ForMember(x => x.Surname, s => s.MapFrom(x => x.Father.IdentityCard.Passport.Surname))
//                .ForMember(x => x.Age, s => s.MapFrom(x => x.Father.IdentityCard.Passport.Age))
//                .ForMember(x => x.TinNumber, s => s.MapFrom(x => x.Father.IdentityCard.Tin.Number))
//                .ForMember(x => x.City, s => s.MapFrom(x => x.Father.AddressCard.City))
//                .ForMember(x => x.Country, s => s.MapFrom(x => x.Father.AddressCard.Country))
//                .ForMember(x => x.WifeName,
//                    s => s.MapFrom(x => x.Mother.IdentityCard.Passport.Name))
//                .ForMember(x => x.WifeSurname,
//                    s => s.MapFrom(x => x.Mother.IdentityCard.Passport.Surname));
        }
    }
}