using Newtonsoft.Json;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class Character
    {
        [JsonProperty]
        private readonly Guid _id;

        public Guid CharacterId { get { return _id; } }
        public Guid InventoryId { get; set; }
        public int MaxHP { get; set; } = 0;
        public int CurrentHp { get; set; } = 8;
        public int ArmorClass { get; set; } = 10;
        public int Str { get; set; } = 8;
        public int Dex { get; set; } = 8;
        public int Con { get; set; } = 8;
        public int Wis { get; set; } = 8;
        public int Int { get; set; } = 8;
        public int Cha { get; set; } = 8;


        public Character()
        {
            _id = Guid.NewGuid();
        }

        [JsonConstructor]
        public Character(Guid CharacterId)
        {
            _id = CharacterId;
        }
    }
}
