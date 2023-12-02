using Services.ContainerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class ItemTemplate : IItemTemplate
    {
        private readonly string _name;

        //Properties
        [Key]
        public string Name { get { return _name; } } 

        public string Description { get; set; } = string.Empty;

        public double Weight { get; set; } = 0;

        public double Value { get; set; } = 0;

        public ItemRarity Rarity { get; set; } = ItemRarity.COMMON;

        public ItemType Type { get; set; } = ItemType.OTHER;

        //Constructors
        public ItemTemplate(string name)
        {
            _name = name;
        }


        //Methods
        public IItem CreateItemFrom()
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

        public bool IsNull() { return false; }

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
