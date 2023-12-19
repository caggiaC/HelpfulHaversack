using System.Security.Cryptography;

namespace HelpfulHaversack.Services.ContainerAPI.Models.Dto
{
    public class CharacterDto
    {
        public Guid CharacterId { get; set; } = Guid.Empty;
        public Guid InventoryId { get; set; } = Guid.Empty;
        public int Xp { get; set; }
        public string Class { get; set; } = String.Empty;
        public int MaxHP { get; set; } = 0;
        public int CurrentHp { get; set; } = 0;
        public int ArmorClass { get; set; } = 10;
        public int Str { get; set; } = 8;
        public int Dex { get; set; } = 8;
        public int Con { get; set; } = 8;
        public int Wis { get; set; } = 8;
        public int Int { get; set; } = 8;
        public int Cha { get; set; } = 8;


        public CharacterDto() { }

    }
}
