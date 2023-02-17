using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruits.API.Models;
using Recruits.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruits.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Recruits : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;

        //So here we will be making use of contructor dependency injection
        public Recruits(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
      
        //GET: api/<UsersController>
        [HttpGet]
        public List<string> Get()
        {
            var recruits = new List<string>
            {
                "John Doe",
                "Jane Doe",
                "Jane Doe"
            };
            return recruits;
        }
         [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userdata)
        {
             var token = _tokenRepository.Authenticate(userdata);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
