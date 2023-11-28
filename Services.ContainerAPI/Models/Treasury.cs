using HelpfulHaversack.Services.ContainerAPI.Models;

namespace Services.ContainerAPI.Models
{
    public class Treasury
    {
        private readonly Guid _id;
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

        public Treasury(Guid id)
        {
            _id = id;
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

        public void RemoveItem(IItem item)
        {
            _inventory.Remove(item);
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetItemsByName(string name)
        {
            return _inventory.GetItemsByName(name);
        }

        public List<Item> GetAllItems()
        {
            return _inventory.GetAllItems();
        }

        public override bool Equals(Object? obj)
        {
            try { return ((Treasury)obj).Id == _id; }
            catch { return false; } 
        }

    }
}
