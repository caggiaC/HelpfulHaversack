using HelpfulHaversack.Services.ContainerAPI.Models;
using HelpfulHaversack.Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;

namespace Services.ContainerAPI.Util
{
    public static class Mapper
    {
        //Item => Dto
        public static ItemDto ItemToDto(Item item)
        {
            return new ItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                Weight = item.Weight,
                Value = item.Value,
            };
        }
        public static List<ItemDto> ItemToDto(IEnumerable<Item> item_list)
        {
            List<ItemDto> returnList = new();

            foreach (Item item in item_list)
                returnList.Add(ItemToDto(item));

            return returnList;
        }

        //Dto => Item
        public static Item DtoToItem(ItemDto dto)
        {
            return new Item(dto.Name, dto.ItemId)
            {
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Weight = dto.Weight,
                Value = dto.Value,
            };
        }
        public static List<Item> DtoToItem(IEnumerable<ItemDto> dto_list)
        {
            List<Item> returnList = new();

            foreach (ItemDto itemDto in dto_list)
                returnList.Add(DtoToItem(itemDto));

            return returnList;
        }

        //Treasury => Dto
        public static TreasuryDto TreasuryToDto(Treasury treasury)
        {
            return new TreasuryDto
            {
                Id = treasury.Id,
                Name = treasury.Name,
                PP = treasury.PP,
                GP = treasury.GP,
                EP = treasury.EP,
                SP = treasury.SP,
                CP = treasury.CP,
                Inventory = treasury.GetAllItems()
            };
        }
        public static List<TreasuryDto> TreasuryToDto(IEnumerable<Treasury> treasury_list)
        {
            List<TreasuryDto> returnList = new();

            foreach (Treasury treasury in treasury_list)
                returnList.Add(TreasuryToDto(treasury));

            return returnList;
        }

        //Dto => Treasury
        public static Treasury DtoToTreasury(TreasuryDto dto)
        {
            var temp = new Treasury(dto.Id)
            {
                Name = dto.Name,
                PP = dto.PP,
                GP = dto.GP,
                EP = dto.EP,
                SP = dto.SP,
                CP = dto.CP,
            };
            temp.AddItems(dto.Inventory);
            return temp;
        }
        public static List<Treasury> DtoToTreasury(IEnumerable<TreasuryDto> dto_list)
        {
            List<Treasury> returnList = new();

            foreach (TreasuryDto dto in dto_list)
                returnList.Add(DtoToTreasury(dto));

            return returnList;
        }

        //ItemTemplate => Dto
        public static ItemTemplateDto ItemTemplateToDto(ItemTemplate itemTemplate)
        {
            return new ItemTemplateDto
            {
                Name = itemTemplate.Name,
                Description = itemTemplate.Description,
                Weight = itemTemplate.Weight,
                Value = itemTemplate.Value,
                Rarity = itemTemplate.Rarity,
                Type = itemTemplate.Type
            };
        }
        public static List<ItemTemplateDto> ItemTemplateToDto(IEnumerable<ItemTemplate> itemTemplate_list)
        {
            List<ItemTemplateDto> returnList = new();

            foreach (ItemTemplate itemTemplate in itemTemplate_list)
                returnList.Add(ItemTemplateToDto(itemTemplate));

            return returnList;
        }

        //Dto => ItemTemplate
        public static ItemTemplate DtoToItemTemplate(ItemTemplateDto dto)
        {
            return new ItemTemplate(dto.Name)
            {
                Description = dto.Description,
                Weight = dto.Weight,
                Value = dto.Value,
                Rarity = dto.Rarity,
                Type = dto.Type
            };
        }
        public static List<ItemTemplate> DtoToItemTemplate(IEnumerable<ItemTemplateDto> dto_list)
        {
            List<ItemTemplate> returnList = new();

            foreach (ItemTemplateDto dto in dto_list)
                returnList.Add(DtoToItemTemplate(dto));

            return returnList;
        }


    }
}
