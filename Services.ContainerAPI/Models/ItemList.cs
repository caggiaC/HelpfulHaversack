using Newtonsoft.Json;
using Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class ItemList
    {
        [JsonProperty]
        private readonly List<Item> _list = new();

        public ItemList() { }

        [JsonConstructor]
        public ItemList(List<Item> Items)
        {
            _list = Items;
        }

        public List<Item> Items { get { return _list; } }

        public void Add(Item item)
        {
            if(item != null)
                _list.Add(item);
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

        public bool Contains(Item item)
        {
            if(item != null)
                return _list.Contains(item);
            return false;
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

        public void UpdateItem(Item item)
        {
            if (item == null)
                throw new ArgumentException("Received Item was null.");

            if (!_list.Contains(item))
                throw new ArgumentException($"Item \"{item.DisplayName}\" with id \"{item.ItemId}\" does not exist within set.");

            Item validatedItem = (Item)item;
            _list[_list.IndexOf(validatedItem)] = validatedItem;
        }
    
    }
}
