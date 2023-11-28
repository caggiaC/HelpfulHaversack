using Services.ContainerAPI.Models;
using System.ComponentModel.DataAnnotations;
using static HelpfulHaversack.Services.ContainerAPI.Models.ItemTemplate;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public interface IItemTemplate
    {
        [Key]
        public string Name { get; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Value { get; set; }
        public ItemRarity Rarity { get; set; }
        public ItemType Type { get; set; }

        public IItem CreateItemFrom();
        public bool IsNull();
    }
}
