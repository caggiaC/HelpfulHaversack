
using HelpfulHaversack.Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public sealed class ItemTemplateList
    {
        private static Dictionary<string,ItemTemplate> templates = new();
        private static readonly Lazy<ItemTemplateList> _instance = new(() => new ItemTemplateList());

        private ItemTemplateList()
        {
            //Seed list; temporary for development
            Add(new ItemTemplate("Longsword")
            {
                Description = "A versatile melee weapon.",
                Weight = 3,
                Value = 15,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });

            Add(new ItemTemplate("Dagger")
            {
                Description = "A simple melee weapon."
                Weight = 1,
                Value = 2,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity= ItemTemplate.ItemRarity.COMMON
            });

            Add(new ItemTemplate("Shortbow")
            {
                Description = "A simple ranged weapon.",
                Weight = 2,
                Value = 25,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });

            Add(new ItemTemplate("Necklace")
            {
                Description = "An ornate necklace.",
                Weight = 0.1,
                Value = 50,
                Type = ItemTemplate.ItemType.OTHER,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });

            //Load template list from file

        }//End Constructor

        public static ItemTemplateList Instance { get { return _instance.Value; } }

        public void Add(ItemTemplate itemTemplate)
        {
            templates.Add(itemTemplate.Name, itemTemplate);
        }

    }
}
