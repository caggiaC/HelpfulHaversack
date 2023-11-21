using System.ComponentModel.DataAnnotations;

namespace HelpfulHaversack.Services.ItemAPI.Models.Dto
{
    public class ItemDto
    {
        public int ItemId { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OwnerId { get; set; }
        public float? Weight { get; set; }
        public int? Value { get; set; }
    }
}
