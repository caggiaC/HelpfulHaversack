namespace HelpfulHaversack.Web.Models.Dto
{
    public class TreasuryDto
    {
        public Guid TreasuryId { get; set; }
        public string Name { get; set; } = String.Empty;
        public int PP { get; set; } = 0;
        public int GP { get; set; } = 0;
        public int EP { get; set; } = 0;
        public int SP { get; set; } = 0;
        public int CP { get; set; } = 0;

        public List<ItemDto> Inventory { get; set; } = new();
    }
}
