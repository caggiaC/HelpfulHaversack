using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.TreasuryAPI.Models;
using Services.TreasuryAPI.Models.Dto;
using Services.TreasuryAPI.Data;
using Services.TreasuryAPI.Util;

namespace Services.TreasuryAPI.Controllers
{
    [Route("api/TreasuryAPI")]
    [ApiController]
    public class TreasuryAPIController
    {
        private ResponseDto _response;

        //Dependency Injection

        public TreasuryAPIController()
        {
            _response = new ResponseDto();
        }
        //End Dependency Injection

        [HttpGet]
        [Route("{treasuryId:guid}")]
        public ResponseDto GetAllItemsFrom(Guid id)
        {
            try
            {
                var sourceTreasury = TreasuryStore.Treasuries.First(x => x.Id == id);
                IEnumerable<Item> returnObjList = sourceTreasury.Inventory;

                _response.Result = Mapper.ItemToDto(returnObjList);
                _response.Result = $"Retrieved {returnObjList.Count()} items from {sourceTreasury.Name} [id:{sourceTreasury.Id}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public ResponseDto GetItem(Guid id)
        {
            try
            {
                var sourceTreasury = TreasuryStore.Treasuries.First(x => x.Id == id);
                Item returnObj = ItemStore.Items.First(u => u.ItemId == id);
                _response.Result = ItemMapper.ItemToDto(returnObj);
                _response.Message = $"Successfully retrieved {returnObj.Name} [ID:{returnObj.ItemId}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{name}")]
        public ResponseDto GetItem(string name)
        {
            try
            {
                IEnumerable<Item> returnObjList =
                    ItemStore.Items.FindAll(u => u.Name.ToLower().Contains(name.ToLower()));
                _response.Result = ItemMapper.ItemToDto(returnObjList);
                _response.Message = $"Successfully retrieved {returnObjList.Count()} items.";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto AddItem([FromBody] ItemDto item)
        {
            try
            {
                ItemStore.Items.Add(ItemMapper.DtoToItem(item));
                _response.Message = $"Successfully added {item.Name} [ID:{item.ItemId}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto UpdateItem([FromBody] ItemDto itemDto)
        {
            try
            {
                var item = ItemStore.Items.First(u => u.ItemId == itemDto.ItemId);
                item.Name = itemDto.Name;
                item.Description = itemDto.Description;
                item.OwnerId = itemDto.OwnerId;
                item.Weight = itemDto.Weight;
                item.Value = itemDto.Value;

                _response.Message = $"Succesfully updated {item.Name} [ID:{item.ItemId}]";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPatch("{id:guid}")]
        public ResponseDto UpdateItemPartial(Guid itemId, JsonPatchDocument<ItemDto> patchDto)
        {
            try
            {
                var item = ItemStore.Items.First(u => u.ItemId == itemId);

                ItemDto itemDto = ItemMapper.ItemToDto(item);

                patchDto.ApplyTo(itemDto);

                var patchedItem = ItemMapper.DtoToItem(itemDto);
                ItemStore.Replace(patchedItem);

                _response.Result = patchedItem;
                _response.Message = $"Successfully updated {patchedItem.Name} [{patchedItem.ItemId}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        public ResponseDto DeleteItem(Guid itemId)
        {
            try
            {
                var item = ItemStore.Items.First(u => u.ItemId == itemId);
                ItemStore.Items.Remove(item);
                _response.Message = $"Succesfully deleted {item.Name} [ID:{item.ItemId}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

    }
}
