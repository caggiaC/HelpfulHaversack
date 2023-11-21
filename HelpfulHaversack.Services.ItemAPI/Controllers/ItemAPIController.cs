using AutoMapper;
using HelpfulHaversack.Services.ItemAPI.Data;
using HelpfulHaversack.Services.ItemAPI.Models;
using HelpfulHaversack.Services.ItemAPI.Models.Dto;
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
        private IMapper _mapper;

        public ItemAPIController(IMapper mapper)
        {
            _mapper = mapper;
            _response = new ResponseDto();
        }
        //End Dependency Injection

        [HttpGet]
        public ResponseDto GetAllItems()
        {
            try
            {
                IEnumerable<Item> returnObjList = ItemStore.Items.ToList();
                _response.Result = _mapper.Map<IEnumerable<ItemDto>>(returnObjList);
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
