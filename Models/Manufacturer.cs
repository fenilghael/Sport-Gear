using System.Collections.Generic;

namespace SportsGearCMS.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }

        // Navigation Property
        public ICollection<GearItemManufacturer> GearItemManufacturers { get; set; }
    }
}
