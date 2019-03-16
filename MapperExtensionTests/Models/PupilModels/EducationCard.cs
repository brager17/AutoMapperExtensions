using System.Collections.Generic;

namespace MapperExtensions.Models
{
    public class EducationCard
    {
        public int Id { get; set; }
        public List<Exam> Exams { get; set; }
        public StudyGroup StudyGroup { get; set; }
    }
}