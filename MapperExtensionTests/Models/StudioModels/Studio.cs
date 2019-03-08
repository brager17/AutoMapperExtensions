using System.Collections.Generic;

namespace Tests.Models.StudioModels
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Film> Films { get; set; }
        public List<Actor> Employees { get; set; }
    }
}