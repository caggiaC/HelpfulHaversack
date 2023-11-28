using Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class ItemList
    {
        private readonly List<Item> _list = new();

        public void Add(IItem item)
        {
            if(!item.IsNull())
                _list.Add((Item)item);
        }

        public bool Remove(IItem item)
        {
            if(!item.IsNull())
                return _list.Remove((Item)item);
            return false;
        }

        public bool Contains(IItem item)
        {
            return !item.IsNull() && _list.Contains(item);
        }

        public Item GetItem(Guid id)
        {
            return _list.First(x => x.ItemId == id);
        }

        public List<Item> GetAllItems() {  return _list; }

        public List<Item> GetItemsByName(string itemName)
        {
            return _list.FindAll(u => u.Name.ToLower().Contains(itemName.ToLower()));
        }
    
    }
}
