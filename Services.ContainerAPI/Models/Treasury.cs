using HelpfulHaversack.Services.ContainerAPI.Models;
using Newtonsoft.Json;

namespace Services.ContainerAPI.Models
{
    public class Treasury
    {
        [JsonProperty]
        private readonly Guid _id;
        [JsonProperty]
        private readonly ItemList _inventory = new();

        public Guid Id { get { return _id; } }

        public string Name { get; set; } = String.Empty;

        public int PP { get; set; } = 0;
        public int GP { get; set; } = 0;
        public int EP { get; set; } = 0;
        public int SP { get; set; } = 0;
        public int CP { get; set; } = 0;

        public Treasury()
        {
            _id = Guid.NewGuid();
        }

        [JsonConstructor]
        public Treasury(Guid Id)
        {
            _id = Id;
        }

        public void AddItem(IItem item)
        {
            _inventory.Add(item);
        }

        public void AddItems(IEnumerable<IItem> items)
        {
            foreach (var item in items)
                _inventory.Add(item);
        }

        public Item RemoveItem(Item item)
        {
            return _inventory.Remove(item);
        }

        public Item RemoveItem(Guid itemId)
        {
            return _inventory.Remove(itemId);
        }

        public Item GetItem(Guid itemId)
        {
            return _inventory.GetItem(itemId);
        }

        public List<Item> GetItemsByName(string name)
        {
            return _inventory.GetItemsByName(name);
        }

        public List<Item> GetAllItems()
        {
            return _inventory.GetAllItems();
        }

        public void UpdateItem(IItem item)
        {
            _inventory.UpdateItem(item);
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null) return false;
            try{ return ((Treasury)obj).Id == _id; }
            catch { return false; } 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
