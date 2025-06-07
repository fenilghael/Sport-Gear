namespace SportsGearCMS.Models
{
    public class GearItemManufacturer
    {
        public int GearItemId { get; set; }
        public GearItem GearItem { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
