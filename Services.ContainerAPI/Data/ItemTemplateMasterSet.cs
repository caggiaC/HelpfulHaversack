
using HelpfulHaversack.Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public sealed class ItemTemplateMasterSet
    {
        private static Dictionary<string,IItemTemplate> templates = new();
        private static readonly Lazy<ItemTemplateMasterSet> _instance = new(() => new ItemTemplateMasterSet());

        private ItemTemplateMasterSet()
        {
            //Seed list; temporary for development
            SeedList();     

            //Load template list from file

        }//End Constructor

        public static ItemTemplateMasterSet Instance { get { return _instance.Value; } }

        public void Add(IItemTemplate template)
        {
            if(!template.IsNull())
                templates.Add(template.Name, template);
        }

        public IItemTemplate GetTemplate(string name)
        {
            try
            {
                return templates[name];
            }
            catch(KeyNotFoundException)
            {
                return NullItemTemplate.Instance;
            }
            
        }

        private void SeedList()
        {
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
                Description = "A simple melee weapon.",
                Weight = 1,
                Value = 2,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity = ItemTemplate.ItemRarity.COMMON
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
        }

    }
}
