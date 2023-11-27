using HelpfulHaversack.Services.ContainerAPI.Data;
using Services.ContainerAPI.Models;

namespace Services.ContainerAPI.Data
{
    public sealed class TreasuryStore
    {
        private static List<Treasury> _treasuries;
        private readonly ItemTemplateMasterSet _templates = ItemTemplateMasterSet.Instance;

        private static readonly Lazy<TreasuryStore> _instance = new(() => new TreasuryStore());

        private TreasuryStore()
        {
            //Seed list; temporary for development
            _treasuries = new()
            {
                new Treasury
                {
                    Name = "Treasure Chest",
                    GP = 100,
                    SP = 200,
                    CP = 500,
                    Inventory = new()
                    {
                        _templates.Get("Dagger").CreateItemFrom(),
                        _templates.Get("Dagger").CreateItemFrom(),
                        _templates.Get("Necklace").CreateItemFrom(),
                    }
                }
            };

            //Load treasuries from file
        }

        public static TreasuryStore Instance { get {  return _instance.Value; } }
    }
}
