using System.ComponentModel.DataAnnotations;

namespace Services.ContainerAPI.Models
{
    public class Item
    {
        private readonly Guid _id;

        [Key]
        public Guid ItemId { get { return _id; } }

        [Required]
        public string Name { get; set; } = "A New Item";

        [Required]
        public string Description { get; set; } = String.Empty;


        public double Weight { get; set; } = 0;

        public double Value { get; set; } = 0;

        public Item()
        {
            _id = Guid.NewGuid();
        }

        public Item(Guid guid)
        {
            _id = guid;
        }

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
    }
}
