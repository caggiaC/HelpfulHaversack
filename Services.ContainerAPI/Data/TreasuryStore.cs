using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models;
using Services.ContainerAPI.Models;

namespace Services.ContainerAPI.Data
{
    public sealed class TreasuryStore
    {
        private static readonly List<Treasury> _treasuries = new();
        private readonly ItemTemplateMasterSet _templates = ItemTemplateMasterSet.Instance;

        private static readonly Lazy<TreasuryStore> _instance = new(() => new TreasuryStore());

        private TreasuryStore()
        {
            //Seed list; temporary for development
            //SeedList();
            //WriteToFile("./Data/");

            //Load treasuries from file
            ReadFromFile("./Data/");
        }

        public static TreasuryStore Instance { get { return _instance.Value; } }

        public Treasury? GetTreasury(Guid id)
        {
            return _treasuries.FirstOrDefault(t => t.TreasuryId == id);
        }

        public List<Treasury> GetAllTreasuries() { return _treasuries; }

        public List<Treasury> GetTreasuriesByName(string treasuryName)
        {
            return _treasuries.FindAll(t => t.Name.ToLower().Contains(treasuryName.ToLower()));
        }

        public void AddTreasury(Treasury treasury)
        {
            if(_treasuries.Contains(treasury)) { throw new ArgumentException("Treasury with this ID already exists."); }
            _treasuries.Add(treasury);
        }

        public void RemoveTreasury(Guid treasuryId)
        {
            _treasuries.Remove(_treasuries.First(x => x.TreasuryId == treasuryId));
        }

        public void RemoveTreasury(Treasury treasury)
        {
            _treasuries.Remove(treasury);
        }

        public void RemoveItemFromTreasury(Guid treasuryId, Guid itemId)
        {
            _treasuries.First(t => t.TreasuryId == treasuryId).RemoveItem(itemId);
        }

        public void UpdateTreasury(Treasury treasury)
        {
            if(!_treasuries.Contains(treasury))
                throw new ArgumentException($"Treasury with id {treasury.TreasuryId} does not exist.");

            _treasuries[_treasuries.IndexOf(treasury)] = treasury;
        }

        public bool Contains(Treasury treasury)
        {
            foreach(Treasury t in _treasuries)
                if(t.TreasuryId == treasury.TreasuryId) return true;

            return false;
        }

        public bool Contains(Guid treasuryId)
        {
            foreach(Treasury t in _treasuries)
                if(t.TreasuryId == treasuryId) return true;

            return false;
        }

        private void SeedList()
        {
            Treasury temp = new()
            {
                Name = "Mysterious Chest",
                GP = 100,
                SP = 200,
                CP = 500
            };

            ItemTemplate? TEMPlate = _templates.GetTemplate("Dagger");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            TEMPlate = _templates.GetTemplate("Dagger");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            TEMPlate = _templates.GetTemplate("Necklace");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            _treasuries.Add(temp);


            temp = new()
            {
                Name = "Bag of Holding",
            };

            TEMPlate = _templates.GetTemplate("Longsword");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            _treasuries.Add(temp);
        }

        private static void WriteToFile(string path)
        {
            JsonFileHandler.WriteCollectionToFile(Path.Combine(path, "Treasuries.txt"), _treasuries);
        }

        private static void ReadFromFile(string path)
        {
            foreach(Treasury treasury in 
                JsonFileHandler.GetFileContents<Treasury>(Path.Combine(path, "Treasuries.txt")))
            {
                _treasuries.Add(treasury);
            }
        }
    }
}
