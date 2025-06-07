using System.Collections.Generic;

namespace SportsGearCMS.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<GearItemCategory> GearItemCategories { get; set; }
    }
}
