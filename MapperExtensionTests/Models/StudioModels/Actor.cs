using MapperExtensions.Models;

namespace Tests.Models.StudioModels
{
    public class Actor
    {
        public int Id { get; set; }
        public IdentityCard IdentityCard { get; set; }
        public AddressCard AddressCard { get; set; }
        public ActingCard ActorCard { get; set; }
    }
}