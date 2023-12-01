using static HelpfulHaversack.Services.ContainerAPI.Models.ItemTemplate;

namespace HelpfulHaversack.Services.ContainerAPI.Models.Dto
{
    public class ItemTemplateDto
    {
        public string Name { get; set; } = "A Aew Item";
        public string Description { get; set; } = String.Empty;
        public double Weight { get; set; } = 0.0;
        public double Value { get; set; } = 0.0;

        public ItemRarity Rarity { get; set; } = ItemRarity.COMMON;
        public ItemType Type { get; set; } = ItemType.OTHER;

    }
}
