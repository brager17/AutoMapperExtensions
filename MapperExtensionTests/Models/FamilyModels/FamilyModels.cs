using System.Collections.Generic;
using MapperExtensions.Models;

namespace MapperExtensionTests.Models
{
   public class Family
   {
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
      public string Name { get; set; }
      public string Surname { get; set; }
      public int Age { get; set; }
      public string TinNumber { get; set; }
      public string WifeName { get; set; }
      public string WifeSurname { get; set; }
      public string PassportNumber { get; set; }
      public string BornCity { get; set; }
      public string BornCountry { get; set; }
   }
}