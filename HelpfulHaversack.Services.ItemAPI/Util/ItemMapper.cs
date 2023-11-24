using HelpfulHaversack.Services.ItemAPI.Models;
using HelpfulHaversack.Services.ItemAPI.Models.Dto;

namespace HelpfulHaversack.Services.ItemAPI.Util
{
    public static class ItemMapper
    {
        public static ItemDto Map(Item item)
        {
            return new ItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                OwnerId = item.OwnerId,
                Weight = item.Weight,
                Value = item.Value,
            };
        }

        public static Item Map(ItemDto item)
        {
            return new Item(item.ItemId)
            {
                Name = item.Name,
                Description = item.Description,
                OwnerId = item.OwnerId,
                Weight = item.Weight,
                Value = item.Value,
            };
        }

        public static List<Item> Map(IEnumerable<ItemDto> itemDto_list)
        {
            List<Item> returnList = new();

            foreach (ItemDto itemDto in itemDto_list)
            {
                returnList.Add(Map(itemDto));
            }

            return returnList;
        }

        public static List<ItemDto> Map(IEnumerable<Item> item_list)
        {
            List<ItemDto> returnList = new();

            foreach (Item item in item_list)
            {
                returnList.Add(Map(item));
            }

            return returnList;
        }
    }
}
