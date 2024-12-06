using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourNamespace.Services;
using buildingWebApi.DTOs;
using buildingWebApi.Constants;

namespace buildingWebApi.Controllers
{
    [ApiController]
    [Route("api/communications")]
    public class CommunicationController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public CommunicationController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllByKeyWord(string keyword, int page, int limit)
        {
            try
            {
                var result = await _communicationService.GetAllByKeyWordAsync(keyword, page, limit);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CommunicationDTO dto)
        {
            try
            {
                var result = await _communicationService.CreateOrUpdateAsync(dto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByUserId(long id, [FromQuery] string buildingName, int page, int limit)
        {
            try
            {
                var result = await _communicationService.GetAllBySalerIdOrUserIdAsync(id, buildingName, page, limit);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            try
            {
                await _communicationService.DeleteCommunicationAsync(id);
                return Ok(MessageKeys.DeleteSuccessfully);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("building/{id}")]
        public async Task<IActionResult> GetAllByBuildingId(long id)
        {
            try
            {
                var result = await _communicationService.GetAllByBuildingIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
