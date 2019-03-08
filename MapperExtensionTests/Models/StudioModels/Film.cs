using System.Collections.Generic;

namespace Tests.Models.StudioModels
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Actor> Actors { get; set; }
    }
}