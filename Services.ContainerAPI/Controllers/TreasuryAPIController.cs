﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Data;
using Services.ContainerAPI.Util;
using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models;

namespace Services.ContainerAPI.Controllers
{
    [Route("api/TreasuryAPI")]
    [ApiController]
    public class TreasuryAPIController
    {
        private ResponseDto _response;
        private ItemTemplateMasterSet _templates;
        private TreasuryStore _treasuryStore;

        //Dependency Injection

        public TreasuryAPIController()
        {
            _response = new ResponseDto();
            _templates = ItemTemplateMasterSet.Instance;
            _treasuryStore = TreasuryStore.Instance;
        }
        //End Dependency Injection

        [HttpGet]        
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
        [Route("{treasuryId:guid}")]
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
        [Route("{treasuryId:guid}/inventory")]
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
        [Route("{treasuryId:guid}/inventory/{itemId:guid}")]
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
        [Route("{treasuryId:guid}/inventory/itemName")]
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

    }
}
