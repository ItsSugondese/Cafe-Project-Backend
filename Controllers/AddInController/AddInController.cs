using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Services.AddInServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.AddInController
{
    [Route("api/addin")]
    [ApiController]
    public class AddInController : ControllerBase
    {

        private readonly IAddInService _addInService;

        public AddInController(IAddInService addInService)
        {
            _addInService = addInService;
        }

        // GET: api/<AddInController>
        [HttpGet]
        public List<AddIn> Get()
        {
            return _addInService.getAllAddin();
        }

        // GET api/<AddInController>/5
        [HttpGet("{id}")]
        public AddIn Get(int id)
        {
            return _addInService.getSingleAddin(id);
        }

        // POST api/<AddInController>
        [HttpPost]
        public void Post(AddIn addIn)
        {
            _addInService.saveAddIn(addIn);
        }

        // PUT api/<AddInController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddInController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
