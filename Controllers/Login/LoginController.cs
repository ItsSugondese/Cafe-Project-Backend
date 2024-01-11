using BisleriumCafeBackend.Model.User;
using BisleriumCafeBackend.Services.Login;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.Login
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        // GET: api/<Login>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Login>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Login>
        [HttpPost]
        public Object Post( User user)
        {
            return Ok(_loginService.Login(user));
        }

        [HttpPost("change-password")]
        public Object ChangePassword(User user)
        {
            _loginService.updatePassword(user);
            return Ok(true);
        }

        // PUT api/<Login>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Login>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
