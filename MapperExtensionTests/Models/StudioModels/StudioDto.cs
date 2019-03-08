using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Tests.Models.StudioModels
{
    public class ActorDto
    {
        public string StudioName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportNumber { get; set; }
        public string SchoolName { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<string> Films { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}