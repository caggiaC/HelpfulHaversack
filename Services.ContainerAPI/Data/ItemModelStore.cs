using Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public class ItemModelStore
    {
        private static readonly Dictionary<Guid,Item> ItemModels = new();

        public Boolean Add(Item item)
        {
            try
            {
                ItemModels.Add(item.ItemId, item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean Remove(Item item)
        {
            try
            {
                ItemModels.Remove(item.ItemId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Item? GetModel(Guid id)
        {
            try
            {
                return ItemModels[id];
            }
            catch
            {
                return null;
            }
        }
    }
}
