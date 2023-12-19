using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;
using Services.ContainerAPI.Data;
using Services.ContainerAPI.Util;
using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models.Dto;

namespace HelpfulHaversack.Services.ContainerAPI.Controllers
{
    [Route("api/TreasuryAPI")]
    [ApiController]
    public class TreasuryAPIController
    {
        private readonly ResponseDto _response;
        private readonly ItemTemplateSet _templates;
        private readonly TreasuryStore _treasuryStore;
        private readonly TimedStateService _timedStateService;

        //Dependency Injection

        //Constructor
        public TreasuryAPIController()
        {
            _response = new ResponseDto();
            _templates = ItemTemplateSet.Instance;
            _treasuryStore = TreasuryStore.Instance;
            _timedStateService = new();

            _timedStateService.StartAsync(new CancellationToken());  
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
                }   
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
                }
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
                    }
                }
                else //sourceTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                }
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
                if(sourceTreasury != null)
                {
                    IEnumerable<Item> returnObjList =
                   sourceTreasury.GetItemsByName(itemName);
                    _response.Result = Mapper.ItemToDto(returnObjList);
                    _response.Message = $"Successfully retrieved {returnObjList.Count()} items " +
                        $"from {sourceTreasury} [id:{sourceTreasury.TreasuryId}] based on search \"{itemName}\".";
                }
                else //sourceTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                }
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

        [HttpGet]
        [Route("templates/search={templateName}")]
        public ResponseDto SearchItemTemplate(string templateName)
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


        //-----------------------------------Post Endpoints-----------------------------------
        [HttpPost]
        [Route("create/treasury:{treasuryName}")]
        public ResponseDto CreateTreasury(string treasuryName)
        {
            try
            {
                if (treasuryName == null)
                    throw new ArgumentException("treasuryName must not be null");

                Treasury newTreasury = new Treasury() { Name = treasuryName};

                _treasuryStore.AddTreasury(newTreasury);

                _response.Result = newTreasury;
                _response.Message = $"Created new treasury with the name \"{treasuryName}\" [id:{newTreasury.TreasuryId}].";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Route("create/template:{templateName}")]
        public ResponseDto CreateItemTemplate(string templateName, [FromBody] ItemTemplateDto dto)
        {
            if (templateName != dto.Name)
                throw new BadHttpRequestException("Template name did not match route.");

                try
                {   
                    _templates.Add(Mapper.DtoToItemTemplate(dto));

                    _response.Message = $"Created template for {dto.Name}";
                    _timedStateService.SetTemplateSetModified();
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
                _timedStateService.SetTemplateSetModified();
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
                if (treasuryDto == null || treasuryDto.TreasuryId != treasuryId)
                    throw new BadHttpRequestException("Data Transfer Object was invalid or ID did not match route.");

                _treasuryStore.RemoveTreasury(treasuryId);
                _treasuryStore.AddTreasury(Mapper.DtoToTreasury(treasuryDto));

                _response.Message = $"Updated {treasuryDto.Name} [id:{treasuryDto.TreasuryId}]. " +
                    $"Entire resource was affected.";
                _timedStateService.SetTreasuryStoreModified();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        public ResponseDto UpdateItem(Guid treasuryId, Guid itemId, [FromBody] ItemDto itemDto)
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
                        _response.IsSuccess = false;
                        _response.Message = $"Item with the Id {itemId} was not found in " +
                            $"Treasury \"{targetTreasury.Name}\" [id:{treasuryId}]";
                    }
                }
                else //targetTreasury == null
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the id {treasuryId} was not found.";
                }  
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        //-----------------------------------Patch Endpoints----------------------------------
        [HttpPatch]
        [Route("templates/{templateName}")]
        public ResponseDto UpdateItemTemplatePartial(string templateName, JsonPatchDocument<ItemTemplateDto> patchDto)
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
                    _response.IsSuccess = false;
                    _response.Message = $"Template with the name \"{templateName}\" was not found.";
                }
                           
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPatch]
        [Route("treasuries/{treasuryId:guid}")]
        public ResponseDto UpdateTreasuryPartial(Guid treasuryId, JsonPatchDocument<TreasuryDto> patchDto)
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
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                }     
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPatch]
        [Route("treasuries/{treasuryId:guid}/inventory/{itemId:guid}")]
        public ResponseDto UpdateItemPartial(Guid treasuryId, Guid itemId, JsonPatchDocument<ItemDto> patchDto)
        {
            try
            {
                if (patchDto == null)
                    throw new BadHttpRequestException("Data Transfer Object was invalid.");

                Treasury? targetTreasury = _treasuryStore.GetTreasury(treasuryId);
                if(targetTreasury != null)
                {
                    var targetItem = targetTreasury.GetItem(itemId);
                    if(targetItem != null)
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
                        _response.IsSuccess = false;
                        _response.Message = $"";
                    }                    
                }
                else //targetTreasury == null
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                }                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPatch]
        [Route("treasuries/{srcTreasuryId:guid}/inventory/{itemId:guid};sendTo={destTreasuryId:guid}")]
        public ResponseDto MoveItem(Guid srcTreasuryId, Guid itemId, Guid destTreasuryId)
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
                        _response.IsSuccess = false;
                        _response.Message = $"Item with the Id {itemId} was not found in " +
                            $"the treasury \"{srcTreasury.Name}\" [id:{srcTreasuryId}].";
                    }
                }
                else //srcTreasury and/or destTreasury is null
                {
                    _response.IsSuccess = false;
                    _response.Message = "One or more requested treasuries was not found.";
                }

                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        //-----------------------------------Delete Endpoints---------------------------------
        [HttpDelete]
        [Route("treasuries/{treasuryId:guid}")]
        public ResponseDto DeleteTreasury(Guid treasuryId)
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
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found. Congratulations?";
                }                
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
                _timedStateService.SetTemplateSetModified();
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
                        _response.IsSuccess = false;
                        _response.Message = $"Item with the Id {itemId} was not found in the treasury " +
                            $"\"{targetTreasury.Name}\" [id:{treasuryId}]. Congratulations?";
                    }                   
                }
                else //targetTreasury == null
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Treasury with the Id {treasuryId} was not found.";
                }             
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        //Background Services
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
                if (_templateSetModified)
                { 
                    if(!_locked)
                    {
                        _locked = true;
                        _templates.Save();
                        _locked = false;
                        _templateSetModified = false;
                    }
                }

                if(_treasuryStoreModified)
                {
                    if(!_locked)
                    {
                        _locked = true;
                        _treasuries.Save();
                        _locked = false;
                        _treasuryStoreModified = false;
                    }
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
