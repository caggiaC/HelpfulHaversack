namespace Services.TreasuryAPI.Models.Dto
{
    public class TreasuryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int PP { get; set; } = 0;
        public int GP { get; set; } = 0;
        public int EP { get; set; } = 0;
        public int SP { get; set; } = 0;
        public int CP { get; set; } = 0;

        public List<Item> Inventory { get; set; } = new();
    }
}
