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

        public Item Remove(Item item)
        {
            _list.Remove(item);
            return item;
        }

        public Item Remove(Guid itemId)
        {
            Item targetItem = _list.First(i => i.ItemId == itemId);
            _list.Remove(targetItem);
            return targetItem;
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

        public void UpdateItem(IItem item)
        {
            if (item.IsNull())
                throw new ArgumentException("Bad item received.");

            if (!_list.Contains(item))
                throw new ArgumentException($"Item \"{item.DisplayName}\" with id \"{item.ItemId}\" does not exist within set.");

            Item validatedItem = (Item)item;
            _list[_list.IndexOf(validatedItem)] = validatedItem;
        }
    
    }
}
