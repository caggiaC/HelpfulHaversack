using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Data;
using Services.ContainerAPI.Util;
using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models.Dto;
using HelpfulHaversack.Services.ContainerAPI.Util;
using HelpfulHaversack.Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Controllers
{
    [Route("api/TreasuryAPI")]
    [ApiController]
    public class TreasuryAPIController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly ItemTemplateSet _templates;
        private readonly TreasuryStore _treasuryStore;
        private readonly CharacterStore _characterStore;
        private readonly RsaHelper _rsaHelper;
        private readonly TimedStateService _timedStateService;
        private readonly List<TreasuryReference> _treasuryReferences;

        //Dependency Injection

        //Constructor
        public TreasuryAPIController()
        {
            _response = new ResponseDto();
            _templates = ItemTemplateSet.Instance;
            _treasuryStore = TreasuryStore.Instance;
            _characterStore = CharacterStore.Instance;
            _treasuryReferences = _treasuryStore.GetTreasuryReferences();

            _rsaHelper = RsaHelper.Instance;

            _timedStateService = new();

            _timedStateService.StartAsync(new CancellationToken());  
        }
        //End Dependency Injection

        //-----------------------------------Get Endpoints------------------------------------
        [HttpGet]
        [Route("treasuries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTreasuries()
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
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);

            //return _rsaHelper.Encrypt(JsonFileHandler.Serialize<ResponseDto>(_response, <targetKey>));
        }

        [HttpGet]
        [Route("Characters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCharacters()
        {
            try
            {
                _response.Result = Mapper.CharacterToDto(_characterStore.GetAllCharacters());
                _response.Message = "Retrieved all characters.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("references")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTreasuryReferences()
        {
            try
            {
                _response.Result = Mapper.TreasuryReferenceToDto(_treasuryReferences);
                _response.Message = "Retrieved all treasury references.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("treasuries/{treasuryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTreasury(Guid treasuryId)
        {
            try
            {
                Treasury? retrievedTreasury = _treasuryStore.GetTreasury(treasuryId);
                if(retrievedTreasury != null)
                {
                    _response.Result = Mapper.TreasuryToDto(retrievedTreasury);
                    _response.Message = "Retrieved treasury";
                }
                else //retrievedTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id \"{treasuryId}\" was not found.";
                    return BadRequest(_response);
                    
                }   
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("treasuries/search={treasuryName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTreasuriesByName(string treasuryName)
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
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("treasuries/{treasuryId:guid}/inventory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllItemsFrom(Guid treasuryId)
        {
            try
            {
                var sourceTreasury = _treasuryStore.GetTreasury(treasuryId);
                if(sourceTreasury != null)
                {
                    IEnumerable<Item> returnObjList = sourceTreasury.GetAllItems();

                    _response.Result = Mapper.ItemToDto(returnObjList);
                    _response.Message = $"Retrieved {returnObjList.Count()} " +
                        $"items from {sourceTreasury.Name} [id:{sourceTreasury.TreasuryId}].";
                }
                else //sourceTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with Id {treasuryId} was not found.";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetItemFrom(Guid treasuryId, Guid itemId)
        {
            try
            {
                var sourceTreasury = _treasuryStore.GetTreasury(treasuryId);
                if(sourceTreasury != null)
                {
                    Item? returnObj = sourceTreasury.GetItem(itemId);
                    if (returnObj != null)
                    {
                        _response.Result = Mapper.ItemToDto(returnObj);
                        _response.Message = $"Successfully retrieved {returnObj.Name} [id:{returnObj.ItemId}] " +
                            $"from {sourceTreasury.Name} [id:{sourceTreasury.TreasuryId}].";
                    }
                    else //returnObj == null
                    {
                        _response.Result = null;
                        _response.IsSuccess = false;
                        _response.Message = $"Item with the Id {itemId} was not found in " +
                            $"Treasury \"{sourceTreasury.Name}\" [id:{treasuryId}]";
                        return BadRequest(_response);
                    }
                }
                else //sourceTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("treasuries/{treasuryId:guid}/inventory/search={itemName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetItemsByName(Guid treasuryId, string itemName)
        {
            try
            {
                var sourceTreasury = _treasuryStore.GetTreasury(treasuryId);
                if(sourceTreasury != null)
                {
                    IEnumerable<Item> returnObjList =
                   sourceTreasury.GetItemsByName(itemName);
                    _response.Result = Mapper.ItemToDto(returnObjList);
                    _response.Message = $"Successfully retrieved {returnObjList.Count()} items " +
                        $"from {sourceTreasury} [id:{sourceTreasury.TreasuryId}] based on search for\"{itemName}\".";
                }
                else //sourceTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                    return BadRequest(_response);

                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("templates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllItemTemplates()
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
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("templates/{templateName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetItemTemplate(string templateName)
        {
            try
            {
                _response.Result = _templates.GetTemplate(templateName);
                if(_response.Result != null)
                {
                    _response.Message = $"Retrieved template for {templateName}";
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Template with the name \"{templateName}\" was not found.";                    
                    return BadRequest(_response);
                }
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("templates/search={templateName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SearchItemTemplate(string templateName)
        {
            try
            {
                _response.Result = _templates.GetAllTemplates().FindAll(
                    t => t.Name.ToLower().Contains(templateName.ToLower()));

                _response.Message = $"Retreived all templates with a name containing \"{templateName}\"";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }


        //-----------------------------------Post Endpoints-----------------------------------
        [HttpPost]
        [Route("create/treasury:{treasuryName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTreasury(string treasuryName)
        {
            try
            {
                if (treasuryName == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Argument \"treasuryName\" must not be null.";
                    return BadRequest(_response);
                }

                Treasury newTreasury = new() { Name = treasuryName};

                _treasuryStore.AddTreasury(newTreasury);

                _response.Result = newTreasury;
                _response.Message = $"Created new treasury with the name \"{treasuryName}\" [id:{newTreasury.TreasuryId}].";
            }
            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        [Route("create/template:{templateName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateItemTemplate(string templateName, [FromBody] ItemTemplateDto dto)
        {
            try
            {
                if (templateName != dto.Name)
                    throw new BadHttpRequestException("Template name did not match route.");

                _templates.Add(Mapper.DtoToItemTemplate(dto));

                _response.Message = $"Created template for {dto.Name}";
                _timedStateService.SetTemplateSetModified();
            }
            catch (Exception ex) when (
                ex is BadHttpRequestException
                || ex is ArgumentException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }


        //-----------------------------------Put Endpoints------------------------------------
        [HttpPut]
        [Route("templates/{templateName}")]
        [Route("create/template:{templateName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateItemTemplate(string templateName, [FromBody] ItemTemplateDto itemTemplateDto)
        {
            try
            {
                if (itemTemplateDto == null || itemTemplateDto.Name != templateName)
                    throw new BadHttpRequestException("Data Transfer Object was invalid or Name did not match route.");

                _templates.RemoveTemplate(templateName);
                _templates.Add(Mapper.DtoToItemTemplate(itemTemplateDto));

                _response.Message = $"Updated {templateName}. Entire resource was affected.";
                _timedStateService.SetTemplateSetModified();
            }
            catch (Exception ex) when (
                ex is BadHttpRequestException 
                || ex is ArgumentException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpPut]
        [Route("treasuries/{treasuryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTreasury(Guid treasuryId, [FromBody] TreasuryDto treasuryDto)
        {
            try
            {
                if (treasuryDto == null || treasuryDto.TreasuryId != treasuryId)
                    throw new BadHttpRequestException("Data Transfer Object was invalid or ID did not match route.");

                _treasuryStore.RemoveTreasury(treasuryId);
                _treasuryStore.AddTreasury(Mapper.DtoToTreasury(treasuryDto));

                _response.Message = $"Updated {treasuryDto.Name} [id:{treasuryDto.TreasuryId}]. " +
                    $"Entire resource was affected.";
                _timedStateService.SetTreasuryStoreModified();
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpPut]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateItem(Guid treasuryId, Guid itemId, [FromBody] ItemDto itemDto)
        {
            try
            {
                if (itemDto == null || itemDto.ItemId != itemId)
                    throw new BadHttpRequestException("Data Transfer Object was invalid or item ID did not match route.");

                var targetTreasury = _treasuryStore.GetTreasury(itemId);
                if(targetTreasury != null)
                {
                    var removedItem = targetTreasury.RemoveItem(itemId);
                    if (removedItem != null)
                    {
                        targetTreasury.AddItem(Mapper.DtoToItem(itemDto));

                        _response.Message = $"Updated {itemDto.Name} [id:{itemId} in {targetTreasury.Name} [id:{treasuryId}]. " +
                            "Entire resource was affected.";
                        _timedStateService.SetTreasuryStoreModified();
                    }
                    else //removedItem == null
                    {
                        throw new BadHttpRequestException(
                            $"Item with the Id {itemId} was not found in " +
                            $"Treasury \"{targetTreasury.Name}\" [id:{treasuryId}]");
                    }
                }
                else //targetTreasury == null
                {
                    throw new BadHttpRequestException(
                        $"Treasury with the id {treasuryId} was not found.");
                }  
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }


        //-----------------------------------Patch Endpoints----------------------------------
        [HttpPatch]
        [Route("templates/{templateName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateItemTemplatePartial(string templateName, JsonPatchDocument<ItemTemplateDto> patchDto)
        {
            try
            {
                if(patchDto == null)
                    throw new BadHttpRequestException("Data Transfer Object recieved was invalid");

                var targetTemplate = _templates.GetTemplate(templateName);

                if(targetTemplate != null)
                {
                    ItemTemplateDto targetTemplateDto = Mapper.ItemTemplateToDto(targetTemplate);

                    patchDto.ApplyTo(targetTemplateDto);

                    _templates.UpdateTemplate(Mapper.DtoToItemTemplate(targetTemplateDto));

                    _response.Message = $"Updated template \"{templateName}\".";
                    _timedStateService.SetTemplateSetModified();
                }
                else //targetTemplate == null
                {
                   throw new BadHttpRequestException(
                       $"Template with the name \"{templateName}\" was not found.");
                }            
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpPatch]
        [Route("treasuries/{treasuryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTreasuryPartial(Guid treasuryId, JsonPatchDocument<TreasuryDto> patchDto)
        {
            try
            {
                if (patchDto == null)
                    throw new BadHttpRequestException("Data Transfer Object was invalid.");

                var targetTreasury = _treasuryStore.GetTreasury(treasuryId);

                if(targetTreasury != null)
                {
                    TreasuryDto targetTreasuryDto = Mapper.TreasuryToDto(targetTreasury);

                    patchDto.ApplyTo(targetTreasuryDto);

                    _treasuryStore.UpdateTreasury(Mapper.DtoToTreasury(targetTreasuryDto));

                    _response.Message = $"Updated treasury \"{targetTreasuryDto.Name}\".";
                    _timedStateService.SetTreasuryStoreModified();
                }
                else //targetTreasury == null
                {
                    throw new BadHttpRequestException(
                        $"Treasury with the Id {treasuryId} was not found.");
                }     
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        public IActionResult UpdateItemPartial(Guid treasuryId, Guid itemId, JsonPatchDocument<ItemDto> patchDto)
        {
            try
            {
                if (patchDto == null)
                    throw new BadHttpRequestException("Data Transfer Object was invalid.");

                Treasury? targetTreasury = _treasuryStore.GetTreasury(treasuryId);
                if (targetTreasury != null)
                {
                    var targetItem = targetTreasury.GetItem(itemId);
                    if (targetItem != null)
                    {
                        ItemDto targetItemDto = Mapper.ItemToDto(targetItem);

                        patchDto.ApplyTo(targetItemDto);

                        targetTreasury.UpdateItem(Mapper.DtoToItem(targetItemDto));

                        _response.Message = $"Updated item \"{targetItemDto.DisplayName}\" [id:{targetItemDto.ItemId}] " +
                            $"in treasury \"{targetTreasury.Name}\" [id:{targetTreasury.TreasuryId}]";
                        _timedStateService.SetTreasuryStoreModified();
                    }
                    else //targetItem == null
                    {
                        throw new BadHttpRequestException(
                            $"Requested item was not found within Treasury \"{targetTreasury.Name}\" [id:{targetTreasury.TreasuryId}].");
                    }
                }
                else //targetTreasury == null
                {
                    throw new BadHttpRequestException($"Treasury with the Id {treasuryId} was not found.");
                }
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpPatch]
        [Route("treasuries/{srcTreasuryId:guid}/inventory/{itemId:guid}:sendTo={destTreasuryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MoveItem(Guid srcTreasuryId, Guid itemId, Guid destTreasuryId)
        {
            try
            {
                var srcTreasury = _treasuryStore.GetTreasury(srcTreasuryId);
                var destTreasury = _treasuryStore.GetTreasury(destTreasuryId);

                if (srcTreasury != null && destTreasury != null)
                {
                    var targetItem = srcTreasury.RemoveItem(itemId);
                    if (targetItem != null)
                    {
                        destTreasury.AddItem(targetItem);
                        _response.Message = $"Item was moved from \"{srcTreasury.Name}\" to \"{destTreasury.Name}\"";
                        _timedStateService.SetTreasuryStoreModified() ;
                    }
                    else //targetItem == null
                    {
                        throw new BadHttpRequestException($"Item with the Id {itemId} was not found in " +
                            $"the treasury \"{srcTreasury.Name}\" [id:{srcTreasuryId}].");
                    }
                }
                else //srcTreasury and/or destTreasury is null
                {
                    throw new BadHttpRequestException("One or more requested treasuries was not found.");
                }    
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        //-----------------------------------Delete Endpoints---------------------------------
        [HttpDelete]
        [Route("treasuries/{treasuryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTreasury(Guid treasuryId)
        {
            try
            {
                var treasuryToDelete = _treasuryStore.GetTreasury(treasuryId);
                if(treasuryToDelete != null)
                {
                    _treasuryStore.RemoveTreasury(treasuryId);
                    _response.Message = $"Removed treasury {treasuryToDelete.Name} [id:{treasuryId}]";
                    _timedStateService.SetTreasuryStoreModified() ;
                }
                else //treasuryToDelete == null
                {
                    throw new BadHttpRequestException(
                        $"Treasury with the Id {treasuryId} was not found. Congratulations?");
                }                
            }
            catch (Exception ex) when (
                ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [Route("templates/{templateName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteItemTemplate(string templateName)
        {
            try
            {
                _templates.RemoveTemplate(templateName);
                _response.Message = $"Deleted template for {templateName}";
                _timedStateService.SetTemplateSetModified();
            }
            catch (Exception ex) when (
                ex is ArgumentException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteItemFromTreasury(Guid treasuryId, Guid itemId)
        {
            try
            {
                var targetTreasury = _treasuryStore.GetTreasury(treasuryId);
                if(targetTreasury != null)
                {
                    var deletedItem = targetTreasury.RemoveItem(itemId);
                    if(deletedItem != null)
                    {
                        _response.Message = $"Deleted {deletedItem.Name} [id:{deletedItem.ItemId}] from " +
                                                $"{targetTreasury.Name} [id:{targetTreasury.TreasuryId}]";
                        _timedStateService.SetTreasuryStoreModified();
                    }
                    else //deletedItem == null
                    {
                        throw new BadHttpRequestException(
                            $"Item with the Id {itemId} was not found in the treasury " +
                            $"\"{targetTreasury.Name}\" [id:{treasuryId}]. Congratulations?");
                    }                   
                }
                else //targetTreasury == null
                {
                    throw new BadHttpRequestException(
                        $"Treasury with the Id {treasuryId} was not found.");
                }             
            }
            catch (Exception ex) when (
                ex is ArgumentException
                || ex is BadHttpRequestException)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }


        //-----------------------------------Background Services------------------------------
        private class TimedStateService : IHostedService
        {
            private readonly ItemTemplateSet _templates;
            private readonly TreasuryStore _treasuries;
            private Timer? _timer;
            private bool _treasuryStoreModified;
            private bool _templateSetModified;
            private bool _locked;

            public TimedStateService() 
            {
                _templates = ItemTemplateSet.Instance;
                _treasuries = TreasuryStore.Instance;
                _treasuryStoreModified = false;
                _templateSetModified = false;
                _timer = null;
                _locked = false;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _timer = new Timer(SaveIfModified, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

                return Task.CompletedTask;
            }

            private void SaveIfModified(object? state)
            {
                //Console.WriteLine($"Checked state {DateTime.Now} ({_templateMasterSetModified} | {_treasuryStoreModified})");
                if (_templateSetModified && !_locked)
                { 
                    _locked = true;
                    _templates.Save();
                    _templateSetModified = false;
                    _locked = false;
                }

                if(_treasuryStoreModified && !_locked)
                {
                    _locked = true;
                    _treasuries.Save();
                    _treasuryStoreModified = false;
                    _locked = false;
                }
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _timer?.Change(Timeout.Infinite, 0);

                return Task.CompletedTask;
            }

            public void SetTreasuryStoreModified() { _treasuryStoreModified = true; }

            public void SetTemplateSetModified() { _templateSetModified = true; }
        }
    }
}
