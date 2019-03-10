using System;
using AutoMapper.Configuration.Conventions;

namespace MapperExtensions.Models
{
    public class IdentityCard
    {
        public string Id { get; set; }
        public Passport Passport { get; set; }
        public TIN Tin { get; set; }
    }

    public class TIN
    {
        public string Number { get; set; }
        public DateTime DateOfReceiving { get; set; }
    }

    public class Passport
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public int Age { get; set; }

    }
}