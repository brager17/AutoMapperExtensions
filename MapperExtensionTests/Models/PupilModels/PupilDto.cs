using System;

namespace MapperExtensions.Models
{
    public class PupilDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public string EducationCardStudyGroupNumber { get; set; }
        public string Number { get; set; }
        public DateTime DateOfReceiving { get; set; }
    }
}