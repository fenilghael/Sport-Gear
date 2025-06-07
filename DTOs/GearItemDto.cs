using System.Collections.Generic;

namespace SportsGearCMS.DTOs
{
    public class GearItemDto
    {
        public int GearItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }

        public List<string> Categories { get; set; }
        public List<string> Manufacturers { get; set; }
    }
}
