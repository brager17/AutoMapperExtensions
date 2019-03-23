using System;

namespace MapperExtensions.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Mark Mark { get; set; }
    }
}