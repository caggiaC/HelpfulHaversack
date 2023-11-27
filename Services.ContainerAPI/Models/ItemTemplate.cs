using Services.ContainerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class ItemTemplate : IItemTemplate
    {
        private readonly string _name;

        [Key]
        public string Name { get { return _name; } } 

        public string Description { get; set; } = string.Empty;

        public double Weight { get; set; } = 0;

        public double Value { get; set; } = 0;

        public ItemRarity Rarity { get; set; } = ItemRarity.COMMON;

        public ItemType Type { get; set; } = ItemType.OTHER;

        public ItemTemplate(string name)
        {
            _name = name;
        }

        public Item CreateItemFrom()
        {
            return new Item(_name)
            {
                Description = this.Description,
                Weight = this.Weight,
                Value = this.Value,
                Rarity = this.Rarity,
                Type = this.Type
            };
        }

        public Boolean IsNull() { return false; }

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
