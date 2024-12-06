using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace buildingWebApi .Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILocalizationUtils _localizationUtils;
        private readonly ITokenService _tokenService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public UserController(IUserService userService, ILocalizationUtils localizationUtils, 
                              ITokenService tokenService, IRabbitMQProducer rabbitMQProducer)
        {
            _userService = userService;
            _localizationUtils = localizationUtils;
            _tokenService = tokenService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var result = _userService.Login(dto.PhoneNumber, dto.Password, HttpContext);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{MessageKeys.LOGIN_FAILED} {e.Message}");
            }
        }

        [HttpGet("search")]
        public IActionResult GetAllByKeyword([FromQuery] string keyword, [FromQuery] int page, [FromQuery] int limit)
        {
            try
            {
                var result = _userService.GetAllByKeyword(keyword, page, limit);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO.RoleId == 1)
                return Unauthorized("Không thể đăng ký tài khoản hoặc admin");

            try
            {
                var result = _userService.Register(registerDTO);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById([FromRoute] long id)
        {
            try
            {
                var user = _userService.GetById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById([FromRoute] long id)
        {
            try
            {
                var result = _userService.DeleteById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("avatar/{id}")]
        public IActionResult UpdateAvatar([FromRoute] long id, [FromForm] IFormFile avatar)
        {
            try
            {
                if (avatar.Length > 10 * 1024 * 1024) 
                {
                    return StatusCode(StatusCodes.Status413PayloadTooLarge,
                        _localizationUtils.GetLocalizedMessage(MessageKeys.UPLOAD_IMAGES_FILE_LARGE));
                }

                if (!avatar.ContentType.StartsWith("image/"))
                {
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType,
                        _localizationUtils.GetLocalizedMessage(MessageKeys.UPLOAD_IMAGES_FILE_MUST_BE_IMAGE));
                }

                var result = _userService.UpdateAvatar(id, avatar);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult UpdateUser([FromBody] UserDTO userDto)
        {
            try
            {
                return _userService.CreateOrUpdate(userDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

               [HttpPost("{id}/reset-password")]
        public IActionResult ResetPassword([FromBody] PasswordDTO dto, [FromRoute] long id)
        {
            try
            {
                var result = _userService.ResetPassword(dto, id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDTO dto)
        {
            try
            {
                return Ok(""); // Placeholder for refresh token logic
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("reset-post")]
        public IActionResult ResetPost()
        {
            var result = _userService.Reset();
            return Ok(result);
        }

        [HttpGet("contacteds/{id}")]
        public IActionResult GetListContactedUser([FromRoute] long id)
        {
            var result = _userService.GetAllContactedByUserId(id);
            return Ok(result);
        }
    }
}

