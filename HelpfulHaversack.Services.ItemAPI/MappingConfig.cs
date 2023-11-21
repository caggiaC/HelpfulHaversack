using AutoMapper;
using HelpfulHaversack.Services.ItemAPI.Models;
using HelpfulHaversack.Services.ItemAPI.Models.Dto;

namespace HelpfulHaversack.Services.ItemAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ItemDto, Item>();
                config.CreateMap<Item, ItemDto>();
            });
        }
    }
}
