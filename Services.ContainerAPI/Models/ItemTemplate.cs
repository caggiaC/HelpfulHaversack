using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class ItemTemplate
    {
        [Required]
        public string Name { get; set; } = "A New Item";

        [Required]
        public string Description { get; set; } = String.Empty;


        public double Weight { get; set; } = 0;

        public double Value { get; set; } = 0;

        public ItemRarity? Rarity { get; set; }

        public ItemType? Type { get; set; }

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
            POTION,
            RING,
            ROD,
            SCROLL,
            STAFF,
            WAND,
            WONDEROUSITEM
        }

    }
}
