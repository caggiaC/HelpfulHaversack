using Newtonsoft.Json;
using Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class ItemTemplate
    {
        [JsonProperty]
        private readonly string _name;

        //Properties
        public string Name { get { return _name; } }

        public string Description { get; set; } = string.Empty;

        public double Weight { get; set; } = 0;

        public double Value { get; set; } = 0;

        public ItemRarity Rarity { get; set; } = ItemRarity.COMMON;

        public ItemType Type { get; set; } = ItemType.OTHER;

        //Constructors
        [JsonConstructor]
        public ItemTemplate(string Name)
        {
            _name = Name;
        }


        //Methods
        public Item CreateItemFrom()
        {
            return new Item()
            {
                Name = this.Name,
                DisplayName = this.Name,
                Description = this.Description,
                Weight = this.Weight,
                Value = this.Value,
                Rarity = this.Rarity,
                Type = this.Type
            };
        }

        public static ItemTemplate CreateTemplateFromItem(Item item)
        {
            return new ItemTemplate(item.Name)
            {
                Description = item.Description,
                Weight = item.Weight,
                Value = item.Value,
                Rarity = item.Rarity,
                Type = item.Type,
            };
        }

        //Other
        public enum ItemRarity
        {
            COMMON,
            UNCOMMON,
            RARE,
            VERYRARE,
            LEGENDARY,
            ARTIFACT,
            UNIQUE
        }

        public enum ItemType
        {
            ADVENTURINGGEAR,
            ARMOR,
            OTHER,
            POTION,
            RING,
            ROD,
            SCROLL,
            STAFF,
            WAND,
            WEAPON,
            WONDEROUSITEM
        }
    }
}
