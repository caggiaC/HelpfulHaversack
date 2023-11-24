using AutoMapper;
using HelpfulHaversack.Services.ItemAPI.Data;
using HelpfulHaversack.Services.ItemAPI.Models;
using HelpfulHaversack.Services.ItemAPI.Models.Dto;
using HelpfulHaversack.Services.ItemAPI.Util;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace HelpfulHaversack.Services.ItemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ItemAPIController
    {
        private ResponseDto _response;

        //Dependency Injection

        public ItemAPIController()
        {
            _response = new ResponseDto();
        }
        //End Dependency Injection

        [HttpGet]
        public ResponseDto GetAllItems()
        {
            try
            {
                IEnumerable<Item> returnObjList = ItemStore.Items.ToList();
                _response.Result = ItemMapper.Map(returnObjList);
                _response.Message = $"Successfully retrieved {returnObjList.Count()} (all) items.";
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
        public ResponseDto getitem(Guid id)
        {
            try
            {
                Item returnObj = ItemStore.Items.First(u => u.ItemId == id);
                _response.Result = ItemMapper.Map(returnObj);
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
                _response.Result = ItemMapper.Map(returnObjList);
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
        public ResponseDto Post([FromBody] ItemDto item)
        {
            try
            {
                ItemStore.Items.Add(ItemMapper.Map(item));
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
        public ResponseDto Put([FromBody] ItemDto itemDto)
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

        [HttpDelete]
        public ResponseDto Delete(Guid itemId)
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
