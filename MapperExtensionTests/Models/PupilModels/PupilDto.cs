using System;
using AutoMapper.Attributes;
using AutoMapper.Configuration.Conventions;

namespace MapperExtensions.Models
{
    public class PupilDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public string Group { get; set; }
        public int Age { get; set; }
        public int CountStudentsInGroup{ get; set; }
        public string TitleAge { get; set; }
    }
}