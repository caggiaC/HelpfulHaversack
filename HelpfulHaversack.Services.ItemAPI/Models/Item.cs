using HelpfulHaversack.Services.ItemAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace HelpfulHaversack.Services.ItemAPI.Models
{
    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        VERYRARE,
        LEGENDARY,
        ARTIFACT,
        UNIQUE
    }

    public enum Type
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
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int? OwnerId { get; set; }

        public double Weight { get; set; }

        public int? Value { get; set; }

    }
}
    