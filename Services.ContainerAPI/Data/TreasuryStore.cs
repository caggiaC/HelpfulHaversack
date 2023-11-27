using Services.ContainerAPI.Models;

namespace Services.ContainerAPI.Data
{
    public sealed class TreasuryStore
    {
        private static List<Treasury> treasuries;

        private static readonly Lazy<TreasuryStore> _instance = new(() => new TreasuryStore());

        private TreasuryStore()
        {
            //Seed list; temporary for development
            treasuries = new()
            {
                new Treasury
                {
                    Name = "Treasure Chest",
                    GP = 100,
                    Inventory = new()
                    {

                    }
                }
            };

            //Load treasuries from file
        }

        public static TreasuryStore Instance { get {  return _instance.Value; } }
    }
}
