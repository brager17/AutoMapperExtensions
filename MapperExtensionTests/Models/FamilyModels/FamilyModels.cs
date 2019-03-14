using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MapperExtensions.Models;

namespace MapperExtensionTests.Models
{
    public class Family
    {
        public Expression<Func<Family, string>> fullfamilyName =
            x => $"{x.Father.IdentityCard.Passport.Name} {x.Mother.IdentityCard.Passport.Name} {x.Id}";

        public Expression<Func<Family, string>> shortFamilyName = x => x.Mother.IdentityCard.Passport.Name;
        public int Id { get; set; }
        public Human Father { get; set; }
        public Human Mother { get; set; }
        public List<Human> Children { get; set; }
    }

    public class Human
    {
        public int Id { get; set; }
        public IdentityCard IdentityCard { get; set; }
        public AddressCard AddressCard { get; set; }
    }

    public class FatherDto
    {
        public string PassportName { get; set; }
        public string PassportSurname { get; set; }
        public int PassportAge { get; set; }
        public string TinNumber { get; set; }
        public string WifeName { get; set; }
        public string WifeSurname { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}