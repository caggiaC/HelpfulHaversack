using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Data;
using Services.ContainerAPI.Util;
using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models;
using HelpfulHaversack.Services.ContainerAPI.Models.Dto;

namespace Services.ContainerAPI.Controllers
{
    [Route("api/TreasuryAPI")]
    [ApiController]
    public class TreasuryAPIController
    {
        private readonly ResponseDto _response;
        private readonly ItemTemplateMasterSet _templates;
        private readonly TreasuryStore _treasuryStore;

        //Dependency Injection

        public TreasuryAPIController()
        {
            _response = new ResponseDto();
            _templates = ItemTemplateMasterSet.Instance;
            _treasuryStore = TreasuryStore.Instance;
        }
        //End Dependency Injection

        //-----------------------------------Get Endpoints------------------------------------
        [HttpGet]
        [Route("treasuries")]
        public ResponseDto GetAllTreasuries()
        {
            try
            {
                _response.Result = Mapper.TreasuryToDto(_treasuryStore.GetAllTreasuries());
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
        [Route("treasuries/{treasuryId:guid}")]
        public ResponseDto GetTreasury(Guid treasuryId)
        {
            try
            {
                _response.Result = Mapper.TreasuryToDto(_treasuryStore.GetTreasury(treasuryId));
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
        [Route("treasuries/search={treasuryName}")]
        public ResponseDto GetTreasuriesByName(string treasuryName)
        {
            try
            {
                IEnumerable<Treasury> returnList = _treasuryStore.GetTreasuriesByName(treasuryName);
                _response.Result = returnList;
                int listLength = returnList.Count();
                _response.Message = (listLength > 0) ?
                    $"Retrieved {listLength} treasuries." :
                    $"No treasuries found that matched search \"{treasuryName}\"";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("treasuries/{treasuryId:guid}/inventory")]
        public ResponseDto GetAllItemsFrom(Guid treasuryId)
        {
            try
            {
                var sourceTreasury = _treasuryStore.GetTreasury(treasuryId);
                IEnumerable<Item> returnObjList = sourceTreasury.GetAllItems();

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
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        public ResponseDto GetItemFrom(Guid treasuryId, Guid itemId)
        {
            try
            {
                var sourceTreasury = _treasuryStore.GetTreasury(treasuryId);
                Item returnObj = sourceTreasury.GetItem(itemId);
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
        [Route("treasuries/{treasuryId:guid}/inventory/search={itemName}")]
        public ResponseDto GetItemsByName(Guid treasuryId, string itemName)
        {
            try
            {
                var sourceTreasury = _treasuryStore.GetTreasury(treasuryId);
                IEnumerable<Item> returnObjList =
                    sourceTreasury.GetItemsByName(itemName);
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

        [HttpGet]
        [Route("templates")]
        public ResponseDto GetAllItemTemplates()
        {
            try
            {
                _response.Result = _templates.GetAllTemplates();
                _response.Message = "Retrieved all item templates.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("templates/{templateName}")]
        public ResponseDto GetItemTemplate(string templateName)
        {
            try
            {
                _response.Result = _templates.GetTemplate(templateName);
                _response.Message = $"Retrieved template for {templateName}";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        //-----------------------------------Post Endpoints-----------------------------------
        [HttpPost]
        [Route("create/treasury")]
        public ResponseDto CreateTreasury([FromBody] TreasuryDto treasuryDto)
        {
            try
            {
                _treasuryStore.AddTreasury(Mapper.DtoToTreasury(treasuryDto));
                _response.Message = $"Created {treasuryDto.Name} [id:{treasuryDto.Id}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Route("create/template")]
        public ResponseDto CreateItemTemplate([FromBody] ItemDto dto)
        {
            try
            {
                _templates.Add(ItemTemplate.CreateTemplateFromItem(Mapper.DtoToItem(dto)));
                _response.Message = $"Created template for {dto.Name}";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        //-----------------------------------Put Endpoints------------------------------------
        [HttpPut]
        [Route("templates/{templateName}")]
        public ResponseDto UpdateItemTemplate(string templateName, [FromBody] ItemTemplateDto itemTemplateDto)
        {
            try
            {
                if (itemTemplateDto == null || itemTemplateDto.Name != templateName)
                    throw new BadHttpRequestException("Data Transfer Object was invalid or Name did not match route.");

                _templates.RemoveTemplate(templateName);
                _templates.Add(Mapper.DtoToItemTemplate(itemTemplateDto));

                _response.Message = $"Updated {templateName}. Entire resource was affected.";


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        [Route("treasuries/{treasuryId:guid}")]
        public ResponseDto UpdateTreasury(Guid treasuryId, [FromBody] TreasuryDto treasuryDto)
        {
            try
            {
                if (treasuryDto == null || treasuryDto.Id != treasuryId)
                    throw new BadHttpRequestException("Data Transfer Object was invalid or ID did not match route.");

                _treasuryStore.RemoveTreasury(treasuryId);
                _treasuryStore.AddTreasury(Mapper.DtoToTreasury(treasuryDto));

                _response.Message = $"Updated {treasuryDto.Name} [id:{treasuryDto.Id}]. " +
                    $"Entire resource was affected.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        //-----------------------------------Patch Endpoints----------------------------------


        //-----------------------------------Delete Endpoints---------------------------------
        [HttpDelete]
        [Route("treasuries/{treasuryId:guid}")]
        public ResponseDto DeleteTreasury(Guid treasuryId)
        {
            try
            {
                Treasury treasuryToDelete = _treasuryStore.GetTreasury(treasuryId);
                _treasuryStore.RemoveTreasury(treasuryId);
                _response.Message = $"Removed treasury {treasuryToDelete.Name} [id:{treasuryId}]";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("templates/{templateName}")]
        public ResponseDto DeleteItemTemplate(string templateName)
        {
            try
            {
                _templates.RemoveTemplate(templateName);
                _response.Message = $"Deleted template for {templateName}";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        public ResponseDto DeleteItemFromTreasury(Guid treasuryId, Guid itemId)
        {
            try
            {
                Treasury targetTreasury = _treasuryStore.GetTreasury(treasuryId);
                Item deletedItem = targetTreasury.RemoveItem(itemId);

                _response.Message = $"Deleted {deletedItem.Name} [id:{deletedItem.ItemId}] from " +
                    $"{targetTreasury.Name} [id:{targetTreasury.Id}]";
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
