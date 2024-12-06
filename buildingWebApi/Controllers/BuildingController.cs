using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using buildingWebApi.Services;
using buildingWebApi.DTOs;
using buildingWebApi.Models;
using buildingWebApi.Constants;
using buildingWebApi.Utils;

namespace buildingWebApi.Controllers
{
    [ApiController]
    [Route("api/buildings")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        private readonly IBuildingImageService _buildingImageService;
        private readonly IGoogleVisionService _googleVisionService;
        private readonly ILocalizationUtils _localizationUtils;

        public BuildingController(IBuildingService buildingService, 
                                  IBuildingImageService buildingImageService, 
                                  IGoogleVisionService googleVisionService, 
                                  ILocalizationUtils localizationUtils)
        {
            _buildingService = buildingService;
            _buildingImageService = buildingImageService;
            _googleVisionService = googleVisionService;
            _localizationUtils = localizationUtils;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllBuildingsByKeyword([FromQuery] Dictionary<string, object> params, [FromQuery] List<string> type, int page = 0, int limit = 20)
        {
            try
            {
                var result = await _buildingService.FindByConditionAsync(params, page, limit, type);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("relations/{id}")]
        public async Task<IActionResult> GetSimilarBuildings(long id)
        {
            try
            {
                var result = await _buildingService.GetRelativeBuildingsByBuildingIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] BuildingDTO buildingDTO)
        {
            try
            {
                var result = await _buildingService.CreateOrUpdateAsync(buildingDTO);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetAllByOwnerId(long id)
        {
            try
            {
                var result = await _buildingService.FindByOwnerIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetSomePopularBuildings()
        {
            try
            {
                var result = await _buildingService.GetSomePopularBuildingsAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuildingById(long id)
        {
            try
            {
                var result = await _buildingService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("uploads/{id}")]
        public async Task<IActionResult> UploadImages(long id, [FromForm] List<IFormFile> images)
        {
            try
            {
                if (images.Count > BuildingImage.MaxImagesPerBuilding)
                {
                    return BadRequest(_localizationUtils.GetLocalizedMessage(MessageKeys.UploadImagesMax5));
                }

                foreach (var file in images)
                {
                    if (file.Length == 0) continue;

                    if (file.Length > 10 * 1024 * 1024) // 10MB
                    {
                        return StatusCode(StatusCodes.Status413PayloadTooLarge, _localizationUtils.GetLocalizedMessage(MessageKeys.UploadImagesFileLarge));
                    }

                    if (!file.ContentType.StartsWith("image/"))
                    {
                        return StatusCode(StatusCodes.Status415UnsupportedMediaType, _localizationUtils.GetLocalizedMessage(MessageKeys.UploadImagesFileMustBeImage));
                    }
                }

                if (!await _googleVisionService.CheckSensitiveContentAsync(images))
                {
                    return BadRequest(MessageKeys.UploadUnsuccessfully);
                }

                var result = await _buildingImageService.UploadImagesAsync(images, id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("avatar/{id}")]
        public async Task<IActionResult> UploadAvatar(long id, [FromForm] IFormFile avatar)
        {
            try
            {
                if (avatar.Length > 10 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status413PayloadTooLarge, _localizationUtils.GetLocalizedMessage(MessageKeys.UploadImagesFileLarge));
                }

                if (!avatar.ContentType.StartsWith("image/"))
                {
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType, _localizationUtils.GetLocalizedMessage(MessageKeys.UploadImagesFileMustBeImage));
                }

                if (!await _googleVisionService.CheckSensitiveContentAsync(avatar))
                {
                    return BadRequest(MessageKeys.UploadUnsuccessfully + " because the image contains sensitive content");
                }

                var result = await _buildingImageService.UploadAvatarAsync(avatar, id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildingById(long id)
        {
            try
            {
                await _buildingService.DeleteByIdAsync(id);
                return Ok(MessageKeys.DeleteSuccessfully);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("building-edit/{id}")]
        public async Task<IActionResult> GetBuildingEditById(long id)
        {
            try
            {
                var result = await _buildingService.GetBuildingEditByIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin/search")]
        public async Task<IActionResult> GetBuildingEditList([FromQuery] Dictionary<string, object> params, [FromQuery] List<string> type, int page = 0, int limit = 20)
        {
            try
            {
                var result = await _buildingService.GetBuildingEditsAsync(params, page, limit, type);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
