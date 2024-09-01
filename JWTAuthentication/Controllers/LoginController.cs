using JWTAuthentication.Model;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly TokenProvider _tokenProvider;

        public LoginController(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Get(string UserName, string Password) 
        {
            if (UserName == "parth@gmail.com" && Password == "p123")
            {
                string token = _tokenProvider.Create(1, UserName);    
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
