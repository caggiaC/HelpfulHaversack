using HelpfulHaversack.Services.ContainerAPI.Models;
using HelpfulHaversack.Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;

namespace Services.ContainerAPI.Util
{
    public static class Mapper
    {
        //---------------------------------| Item => Dto |---------------------------------

        /// <summary>
        /// Takes an Item and returns a DTO for that item.
        /// </summary>
        /// <param name="item">The Item to be converted into a DTO.</param>
        /// <returns>A DTO of the passed Item.</returns>
        public static ItemDto ItemToDto(Item item)
        {
            return new ItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                Weight = item.Weight,
                Value = item.Value,
                Type = item.Type
            };
        }

        /// <summary>
        /// Takes a List of type Item and returns a List of DTOs for those Items.
        /// </summary>
        /// <param name="item">The List of Items to be converted into DTOs.</param>
        /// <returns>A List of DTOs of the passed items.</returns>
        public static List<ItemDto> ItemToDto(IEnumerable<Item> item_list)
        {
            List<ItemDto> returnList = new();

            foreach (Item item in item_list)
                returnList.Add(ItemToDto(item));

            return returnList;
        }

        //---------------------------------| Dto => Item |---------------------------------

        /// <summary>
        /// Takes an ItemDto and returns an Item constructed from that DTO.
        /// </summary>
        /// <param name="dto">The DTO to convert to an Item.</param>
        /// <returns>An Item constructed from the DTO.</returns>
        public static Item DtoToItem(ItemDto dto)
        {
            return new Item(dto.ItemId)
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Weight = dto.Weight,
                Value = dto.Value,
                Type = dto.Type
            };
        }

        /// <summary>
        /// Takes a List of type ItemDto and returns a List of Items constructed from those DTOs.
        /// </summary>
        /// <param name="item">The List of type ItemDto to be converted into Items.</param>
        /// <returns>A List of DTOs of the passed items.</returns>
        public static List<Item> DtoToItem(IEnumerable<ItemDto> dto_list)
        {
            List<Item> returnList = new();

            foreach (ItemDto itemDto in dto_list)
                returnList.Add(DtoToItem(itemDto));

            return returnList;
        }

        //---------------------------------| Treasury => Dto |-----------------------------

        /// <summary>
        /// Takes a Treasury and returns a DTO for that Treasury.
        /// </summary>
        /// <param name="treasury">The Treasury to convert into a DTO.</param>
        /// <returns>A DTO of the passed treasury.</returns>
        public static TreasuryDto TreasuryToDto(Treasury treasury)
        {
            return new TreasuryDto
            {
                TreasuryId = treasury.TreasuryId,
                Name = treasury.Name,
                PP = treasury.PP,
                GP = treasury.GP,
                EP = treasury.EP,
                SP = treasury.SP,
                CP = treasury.CP,
                Inventory = ItemToDto(treasury.GetAllItems())
            };
        }

        /// <summary>
        /// Takes a List of type Treasury and returns a List of DTOs for those treasuries.
        /// </summary>
        /// <param name="item">The List of type Treasury to be converted into DTOs.</param>
        /// <returns>A List of type TreasuryDto for those treasuries.</returns>
        public static List<TreasuryDto> TreasuryToDto(IEnumerable<Treasury> treasury_list)
        {
            List<TreasuryDto> returnList = new();

            foreach (Treasury treasury in treasury_list)
                returnList.Add(TreasuryToDto(treasury));

            return returnList;
        }

        //---------------------------------| Dto => Treasury |-----------------------------

        /// <summary>
        /// Takes a TreasuryDto and returns a Treasury constructed from that DTO.
        /// </summary>
        /// <param name="dto">The DTO to convert to a Treasury.</param>
        /// <returns>A Treasury constructed from the DTO.</returns>
        public static Treasury DtoToTreasury(TreasuryDto dto)
        {
            var temp = new Treasury(dto.TreasuryId)
            {
                Name = dto.Name,
                PP = dto.PP,
                GP = dto.GP,
                EP = dto.EP,
                SP = dto.SP,
                CP = dto.CP,
            };
            temp.AddItems(DtoToItem(dto.Inventory));
            return temp;
        }

        /// <summary>
        /// Takes a List of type TreasuryDto and returns a List of treasuries constructed from those DTOs.
        /// </summary>
        /// <param name="item">The List of type TreasuryDto to be converted into Items.</param>
        /// <returns>A List of DTOs of the passed treasuries.</returns>
        public static List<Treasury> DtoToTreasury(IEnumerable<TreasuryDto> dto_list)
        {
            List<Treasury> returnList = new();

            foreach (TreasuryDto dto in dto_list)
                returnList.Add(DtoToTreasury(dto));

            return returnList;
        }

        //---------------------------------| ItemTemplate => Dto |-------------------------

        /// <summary>
        /// Takes an ItemTemplate and returns a DTO for that template.
        /// </summary>
        /// <param name="item">The ItemTemplate to be converted into a DTO.</param>
        /// <returns>A DTO of the passed ItemTemplate.</returns>
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

        /// <summary>
        /// Takes a List of type ItemTemplate and returns a List of DTOs for those templates.
        /// </summary>
        /// <param name="item">The List of ItemTemplates to be converted into DTOs.</param>
        /// <returns>A List of DTOs of the passed ItemTeamplates.</returns>
        public static List<ItemTemplateDto> ItemTemplateToDto(IEnumerable<ItemTemplate> itemTemplate_list)
        {
            List<ItemTemplateDto> returnList = new();

            foreach (ItemTemplate itemTemplate in itemTemplate_list)
                returnList.Add(ItemTemplateToDto(itemTemplate));

            return returnList;
        }

        //---------------------------------| Dto => ItemTemplate |-------------------------

        /// <summary>
        /// Takes an ItemTemplateDto and returns an Item constructed from that DTO.
        /// </summary>
        /// <param name="dto">The DTO to convert to an ItemTemplate.</param>
        /// <returns>An ItemTemplate Constructed from the DTO.</returns>
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

        /// <summary>
        /// Takes a List of type ItemTemplateDto and returns a List of ItemTemplates constructed from those DTOs.
        /// </summary>
        /// <param name="item">The List of type ItemTemplateDto to be converted into ItemTemplates.</param>
        /// <returns>A List of DTOs of the passed templates.</returns>
        public static List<ItemTemplate> DtoToItemTemplate(IEnumerable<ItemTemplateDto> dto_list)
        {
            List<ItemTemplate> returnList = new();

            foreach (ItemTemplateDto dto in dto_list)
                returnList.Add(DtoToItemTemplate(dto));

            return returnList;
        }

        //---------------------------------| Character => Dto |----------------------------

        /// <summary>
        /// Takes an Character and returns a DTO for that character.
        /// </summary>
        /// <param name="item">The Character to be converted into a DTO.</param>
        /// <returns>A DTO of the passed Character.</returns>
        public static CharacterDto CharacterToDto(Character character)
        {
            return new CharacterDto()
            {
                CharacterId = character.CharacterId,
                InventoryId = character.InventoryId,
                Name = character.Name,
                Xp = character.Xp,
                Class = character.Class,
                MaxHP = character.MaxHP,
                CurrentHp = character.CurrentHp,
                ArmorClass = character.ArmorClass,
                Str = character.Str,
                Dex = character.Dex,
                Con = character.Con,
                Int = character.Int,
                Wis = character.Wis,
                Cha = character.Cha
            };
        }

        /// <summary>
        /// Takes a List of type Character and returns a List of DTOs for those characters.
        /// </summary>
        /// <param name="item">The List of Characters to be converted into DTOs.</param>
        /// <returns>A List of DTOs of the passed Characters.</returns>
        public static List<CharacterDto> CharacterToDto(IEnumerable<Character> character_list)
        {
            List<CharacterDto> returnList = new();

            foreach (Character character in character_list)
                returnList.Add(CharacterToDto(character));

            return returnList;
        }

        //---------------------------------| Dto => Character |----------------------------

        /// <summary>
        /// Takes a CharacterDto and returns a Character constructed from that DTO.
        /// </summary>
        /// <param name="dto">The DTO to convert to a Character.</param>
        /// <returns>A Character constructed from the DTO.</returns>
        public static Character DtoToCharacter(CharacterDto dto)
        {
            return new Character(dto.CharacterId)
            {
                InventoryId = dto.InventoryId,
                Xp = dto.Xp,
                Name = dto.Name,
                Class = dto.Class,
                MaxHP = dto.MaxHP,
                CurrentHp = dto.CurrentHp,
                ArmorClass = dto.ArmorClass,
                Str = dto.Str,
                Dex = dto.Dex,
                Con = dto.Con,
                Int = dto.Int,
                Wis = dto.Wis,
                Cha = dto.Cha
            };
        }

        /// <summary>
        /// Takes a List of type CharacterDto and returns a List of Characters constructed from those DTOs.
        /// </summary>
        /// <param name="item">The List of type CharacterDto to be converted into Items.</param>
        /// <returns>A List of DTOs of the passed characters.</returns>
        public static List<Character> DtoToCharacter(IEnumerable<CharacterDto> dto_list)
        {
            List<Character> returnList = new();

            foreach (CharacterDto dto in dto_list)
                returnList.Add(DtoToCharacter(dto));

            return returnList;
        }

        //---------------------------------| TreasuryReference => Dto |----------------------------

        /// <summary>
        ///   
        /// </summary>
        /// <param name="treasuryReference"></param>
        /// <returns></returns>
        public static TreasuryReferenceDto TreasuryReferenceToDto(TreasuryReference treasuryReference)
        {
            return new TreasuryReferenceDto
            {
                TreasuryId = treasuryReference.TreasuryId,
                TreasuryName = treasuryReference.TreasuryName
            };
        }

        public static List<TreasuryReferenceDto> TreasuryReferenceToDto(IEnumerable<TreasuryReference> treasuryReference_list)
        {
            List<TreasuryReferenceDto> returnList = new();

            foreach (TreasuryReference treasuryReference in treasuryReference_list)
                returnList.Add(TreasuryReferenceToDto(treasuryReference));

            return returnList;
        }

        //---------------------------------| Dto => TreasuryReference |----------------------------

        public static TreasuryReference DtoToTreasuryReference(TreasuryReferenceDto dto)
        {
            return new TreasuryReference(dto.TreasuryName, dto.TreasuryId);
        }

        public static List<TreasuryReference> DtoToTreasuryReference(IEnumerable<TreasuryReferenceDto> dto_list)
        {
            List<TreasuryReference> returnList = new();

            foreach (TreasuryReferenceDto dto in dto_list)
                returnList.Add(DtoToTreasuryReference(dto));

            return returnList;
        }
    }
}
