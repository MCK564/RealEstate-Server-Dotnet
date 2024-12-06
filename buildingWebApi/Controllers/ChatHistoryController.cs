using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Controllers
{
    [ApiController]
    [Route("api/chats")]
    public class ChatHistoryController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatHistoryController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("")]
        public IActionResult SendMessage([FromBody] ChatMessageDTO dto)
        {
            try
            {
                var result = _chatService.SaveMessage(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserConnections([FromRoute] long userId)
        {
            try
            {
                var connections = _chatService.GetAllConnectedByUserId(userId);
                return Ok(connections);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}