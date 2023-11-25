using Services.TreasuryAPI.Models;

namespace Services.TreasuryAPI.Data
{
    public static class TreasuryStore
    {
        public static List<Treasury> Treasuries = new List<Treasury>
        {
            new Treasury()
            {
                Name = "Unassigned Items",
                Inventory = new()
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
                }
            },
            new Treasury()
            {
                Name = "Treasure Chest",
                GP = 500,
                Inventory = new()
                {
                    new Item
                    {
                        Name = "Necklace",
                        Description = "An Ornamental Necklace.",
                        Weight = 0.2,
                        Value = 250
                    }
                }
            }
        };
    }
}
