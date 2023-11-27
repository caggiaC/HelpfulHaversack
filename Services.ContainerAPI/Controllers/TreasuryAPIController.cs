using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Data;
using Services.ContainerAPI.Util;
using HelpfulHaversack.Services.ContainerAPI.Data;

namespace Services.ContainerAPI.Controllers
{
    [Route("api/TreasuryAPI")]
    [ApiController]
    public class TreasuryAPIController
    {
        private ResponseDto _response;
        private ItemModelStore _itemStore;

        //Dependency Injection

        public TreasuryAPIController()
        {
            _response = new ResponseDto();
            _itemStore = new ItemModelStore();
        }
        //End Dependency Injection

        [HttpGet]        
        public ResponseDto GetAllTreasuries()
        {
            try
            {
                _response.Result = TreasuryStore.Treasuries;
                _response.Message = "Retrieved all treasuries.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{treasuryId:guid}")]
        public ResponseDto GetTreasury(Guid treasuryId)
        {
            try
            {
                _response.Result = TreasuryStore.Treasuries.First(x => x.Id == treasuryId);
                _response.Message = "Retrieved treasury";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
        
        [HttpGet]
        [Route("{treasuryId:guid}/inventory")]
        public ResponseDto GetAllItemsFrom(Guid treasuryId)
        {
            try
            {
                var sourceTreasury = TreasuryStore.Treasuries.First(x => x.Id == treasuryId);
                IEnumerable<Item> returnObjList = sourceTreasury.Inventory;

                _response.Result = Mapper.ItemToDto(returnObjList);
                _response.Message = $"Retrieved {returnObjList.Count()} items from {sourceTreasury.Name} [id:{sourceTreasury.Id}].";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{treasuryId:guid}/{itemId:guid}")]
        public ResponseDto GetItemFrom(Guid treasuryId, Guid itemId)
        {
            try
            {
                var sourceTreasury = TreasuryStore.Treasuries.First(x => x.Id == treasuryId);
                Item returnObj = sourceTreasury.Inventory.First(u => u.ItemId == itemId);
                _response.Result = Mapper.ItemToDto(returnObj);
                _response.Message = $"Successfully retrieved {returnObj.Name} [id:{returnObj.ItemId}] " +
                    $"from {sourceTreasury.Name} [id:{sourceTreasury.Id}].";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{treasuryId:guid}/itemName")]
        public ResponseDto GetItemsByName(Guid treasuryId, string itemName)
        {
            try
            {
                var sourceTreasury = TreasuryStore.Treasuries.First(x => x.Id == treasuryId);
                IEnumerable<Item> returnObjList =
                    sourceTreasury.Inventory.FindAll(u => u.Name.ToLower().Contains(itemName.ToLower()));
                _response.Result = Mapper.ItemToDto(returnObjList);
                _response.Message = $"Successfully retrieved {returnObjList.Count()} items " +
                    $"from {sourceTreasury} [id:{sourceTreasury.Id}].";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Route("createTreasury")]
        public ResponseDto CreateTreasury([FromBody] TreasuryDto dto)
        {
            try
            {
                TreasuryStore.Treasuries.Add(Mapper.DtoToTreasury(dto));
                _response.Message = $"Created {dto.Name} [id:{dto.Id}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Route("createItem")]
        public ResponseDto CreateItem([FromBody] ItemDto dto)
        {
            try
            {
                throw new NotImplementedException();
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
