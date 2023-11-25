using HelpfulHaversack.Services.ItemAPI.Models;
using HelpfulHaversack.Services.ItemAPI.Models.Dto;

namespace HelpfulHaversack.Services.ItemAPI.Data
{
    //temporary class for development purposes
    public class ItemStore
    {
        public static List<Item> Items = new List<Item>
        {
            new Item
            {
                Name = "Longsword",
                Description = "A versatile melee weapon.",
                Weight = 3.0,
                Value = 15
            },
            new Item
            {
                Name = "Dagger",
                Description = "A simple melee weapon.",
                Weight = 1,
                Value = 2
            },
            new Item
            {
                Name = "Bow",
                Description = "A simple ranged weapon.",
                Weight = 2,
                Value = 25
            }
        };

        public static Boolean Replace(Item item)
        {
            try
            {
                Items.Remove(
                    Items.First(u => u.ItemId == item.ItemId));

                Items.Add(item);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
