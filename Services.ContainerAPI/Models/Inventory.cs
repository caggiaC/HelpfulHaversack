using Services.ContainerAPI.Models;

namespace Services.TreasuryAPI.Models
{
    public class Inventory
    {
        private readonly List<Item> Items = new();

        public void Give(Item item)
        {
            Items.Add(item);
        }

        public Item Take(Guid itemId)
        {
            var item = Items.First(x => x.ItemId == itemId);
            Items.Remove(item);
            return item;
        }

        public Boolean Replace(Item item)
        {
            try
            {
                Items.Remove(
                    Items.First(u => u.ItemId == item.ItemId));

                Items.Add(item);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
