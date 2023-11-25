using HelpfulHaversack.Services.ItemAPI.Models;
using HelpfulHaversack.Services.ItemAPI.Models.Dto;

namespace HelpfulHaversack.Services.ItemAPI.Util
{
    public static class ItemMapper
    {
        public static ItemDto ItemToDto(Item item)
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

        public static Item DtoToItem(ItemDto item)
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

        public static List<Item> DtoToItem(IEnumerable<ItemDto> itemDto_list)
        {
            List<Item> returnList = new();

            foreach (ItemDto itemDto in itemDto_list)
            {
                returnList.Add(DtoToItem(itemDto));
            }

            return returnList;
        }

        public static List<ItemDto> ItemToDto(IEnumerable<Item> item_list)
        {
            List<ItemDto> returnList = new();

            foreach (Item item in item_list)
            {
                returnList.Add(ItemToDto(item));
            }

            return returnList;
        }
    }
}
