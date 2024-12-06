using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buildingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMailService _mailService;
        private readonly string _returnClientUrl;

        public PaymentController(IPaymentService paymentService, IMailService mailService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _mailService = mailService;
            _returnClientUrl = configuration["vnpay:return_client_url"];
        }

        [HttpGet("search")]
        public IActionResult SearchPaymentByConditions(
            [FromQuery] int year = 2024,
            [FromQuery] int month = 1,
            [FromQuery] int page = 0,
            [FromQuery] int limit = 10)
        {
            try
            {
                var result = _paymentService.AdminGetPayments(year, month, page, limit);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{user_id}")]
        public IActionResult GetPaymentHistoryByUserId([FromRoute] long userId)
        {
            try
            {
                var result = _paymentService.GetAllByUserId(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult BuyPost([FromBody] PaymentDTO paymentDTO)
        {
            try
            {
                var payUrl = _paymentService.CreatePaymentUrl(paymentDTO);
                return Ok(payUrl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("vnpay_return")]
        public IActionResult HandleVNPayReturn([FromQuery] Dictionary<string, string> queryParams)
        {
            try
            {
                var redirectUrl = _paymentService.HandleVNPayReturnURL(queryParams);
                return Redirect(redirectUrl);
            }
            catch (Exception)
            {
                return Redirect($"{_returnClientUrl}failed");
            }
        }

        [HttpGet("revenue_excel")]
        public IActionResult ExportAndDownloadExcelFile([FromQuery] int year, [FromQuery] int month)
        {
            
            return Ok("url");
        }

        [HttpGet("total_revenue")]
        public IActionResult GetTotalRevenue()
        {
            // Dummy response
            return Ok("haha");
        }
    }

   

    
    public interface IPaymentService
    {
        object AdminGetPayments(int year, int month, int page, int limit);
        object GetAllByUserId(long userId);
        string CreatePaymentUrl(PaymentDTO paymentDTO);
        string HandleVNPayReturnURL(Dictionary<string, string> queryParams);
    }

   
}
