using ColorBackend.DataAccess;
using ColorBackend.Model;
using ColorBackend.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(string userIP)
        {
            try
            {
                string token = TokenService.GenerateToken(userIP);

                var createdResponse = new ApiResponse<string>
                {
                    IsSuccess = true,
                    ReturnedCode = 201,
                    Message = "Token Ready",
                    Result = token
                };
                return StatusCode(201, createdResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<object>
                {
                    IsSuccess = false,
                    ReturnedCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Result = null
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
