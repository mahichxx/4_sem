using System.Collections.Generic;

namespace FitnessClub
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}