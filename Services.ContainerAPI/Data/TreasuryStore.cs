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
            SeedList();

            //Load treasuries from file
        }

        public static TreasuryStore Instance { get { return _instance.Value; } }

        public Treasury GetTreasury(Guid id)
        {
            return _treasuries.First(t => t.Id == id);
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
            _treasuries.Remove(_treasuries.First(x => x.Id == treasuryId));
        }

        public void RemoveTreasury(Treasury treasury)
        {
            _treasuries.Remove(treasury);
        }

        public void RemoveItemFromTreasury(Guid treasuryId, Guid itemId)
        {
            _treasuries.First(t => t.Id == treasuryId).RemoveItem(itemId);
        }

        public void UpdateTreasury(Treasury treasury)
        {
            if(!_treasuries.Contains(treasury))
                throw new ArgumentException($"Treasury with id {treasury.Id} does not exist.");

            _treasuries[_treasuries.IndexOf(treasury)] = treasury;
        }

        public bool Contains(Treasury treasury)
        {
            foreach(Treasury t in _treasuries)
                if(t.Id == treasury.Id) return true;

            return false;
        }

        public bool Contains(Guid treasuryId)
        {
            foreach(Treasury t in _treasuries)
                if(t.Id == treasuryId) return true;

            return false;
        }

        private void SeedList()
        {
            Treasury temp = new()
            {
                Name = "Treasure Chest",
                GP = 100,
                SP = 200,
                CP = 500
            };
            temp.AddItems(new List<IItem>
            {
                _templates.GetTemplate("Dagger").CreateItemFrom(),
                _templates.GetTemplate("Dagger").CreateItemFrom(),
                _templates.GetTemplate("Necklace").CreateItemFrom(),
                _templates.GetTemplate("Fake Item").CreateItemFrom(),
            });
            _treasuries.Add(temp);

            temp = new()
            {
                Name = "Bag of Holding",
            };
            temp.AddItems(new List<IItem>
            {
                _templates.GetTemplate("Longsword").CreateItemFrom()
            });
            _treasuries.Add(temp);
        }
    }
}
