using System.Collections.Generic;

namespace SportsGearCMS.Models
{
    public class GearItem
    {
        public int GearItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }

        // Navigation Properties
        public ICollection<GearItemCategory> GearItemCategories { get; set; }
        public ICollection<GearItemManufacturer> GearItemManufacturers { get; set; }
    }
}
