namespace MapperExtensions.Models
{
    public class Pupil
    {
        public int Id { get; set; }
        public IdentityCard Identity { get; set; }
        public AddressCard Address { get; set; }
        public EducationCard EducationCard { get; set; }
    }
}