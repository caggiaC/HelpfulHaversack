using static HelpfulHaversack.Services.ContainerAPI.Models.ItemTemplate;

namespace Services.ContainerAPI.Models.Dto
{
    public class ItemDto
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string DisplayName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public double Weight { get; set; }
        public double Value { get; set; }
        public ItemRarity? Rarity { get; set; }
        public ItemType? Type { get; set; }
    }
}
