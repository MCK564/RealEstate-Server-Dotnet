using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using buildingWebApi.DTOs;
using buildingWebApi.Services;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/likes")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet("building/{id}")]
        public async Task<IActionResult> GetAllLikesByBuildingId(long id)
        {
            try
            {
                var result = await _likeService.GetAllByBuildingIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetAllLikesByUserId(long id)
        {
            try
            {
                var result = await _likeService.GetAllByUserIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Like([FromBody] LikeDTO dto)
        {
            try
            {
                var result = await _likeService.LikeAsync(dto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Dislike(long id)
        {
            try
            {
                var result = await _likeService.DislikeAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
