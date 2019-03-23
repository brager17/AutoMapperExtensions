using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MapperExtensions.Models
{
    public class EducationCard
    {
        public static Expression<Func<EducationCard, string>> ShortText { get; set; } = x => x.StudyGroup.Number;

        public static Expression<Func<EducationCard, string>> FullText { get; set; } = x => x.StudyGroup.Number + " " +
                                                                                            x.StudyGroup.Number;

        public int Id { get; set; }
        public List<Exam> Exams { get; set; }
        public StudyGroup StudyGroup { get; set; }
    }
}