namespace SportsGearCMS.Models
{
    public class GearItemCategory
    {
        public int GearItemId { get; set; }
        public GearItem GearItem { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
