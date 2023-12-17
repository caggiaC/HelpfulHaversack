
using Newtonsoft.Json;

namespace Services.ContainerAPI.Models
{
    public class Treasury
    {
        [JsonProperty]
        private readonly Guid _id;
        [JsonProperty]
        private readonly List<Item> _inventory = new();

        public Guid TreasuryId { get { return _id; } }

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

        public void AddItem(Item? item)
        {
            if(item != null)
            _inventory.Add(item);
        }

        public void AddItems(IEnumerable<Item?> items)
        {
            foreach (var item in items)
            {
                if (item != null)
                    _inventory.Add(item);
            }        
        }

        public Item? RemoveItem(Item? item)
        {
            if (item != null)
            {
                var removedItem = _inventory.FirstOrDefault(i => i.ItemId == item.ItemId);
                if(removedItem != null)
                {
                    _inventory.Remove(removedItem);
                    return removedItem;
                }
            }                          
            return null;
        }

        public Item? RemoveItem(Guid itemId)
        {
            var removedItem = _inventory.First(i => i.ItemId == itemId);
            if(removedItem != null)
            {
                _inventory.Remove(removedItem);
                return removedItem;
            }
            return null;
        }

        public Item? GetItem(Guid itemId)
        {
            return _inventory.FirstOrDefault(i => i.ItemId == itemId);
        }

        public List<Item> GetItemsByName(string name)
        {
            return _inventory.FindAll(i => i.Name.ToLower().Contains(name.ToLower()));
        }

        public List<Item> GetAllItems()
        {
            return _inventory;
        }

        public void UpdateItem(Item? item)
        {
            if(item == null) return;

            _inventory[_inventory.IndexOf(item)] = item;
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null) return false;
            try{ return ((Treasury)obj).TreasuryId == _id; }
            catch { return false; } 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
