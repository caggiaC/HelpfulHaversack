using HelpfulHaversack.Services.ItemAPI.Models;

namespace HelpfulHaversack.Services.ItemAPI.Data
{
    //temporary class for development purposes
    public class ItemStore
    {
        public static List<Item> Items = new List<Item>
        {
            new Item
            {
                Name = "Longword",
                Description = "A versatile melee weapon.",
                Weight = 3.0,
                Value = 15
            }
        };
    }
}
