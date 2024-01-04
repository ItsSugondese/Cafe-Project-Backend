using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Services.AddInServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.AddInController
{
    [Route("api/addin")]
    [ApiController]
    public class AddInController : GenericController
    {

        private readonly IAddInService _addInService;
        private string moduleName;

        public AddInController(IAddInService addInService)
        {
           _addInService = addInService;
            moduleName = ModuleNameConstants.ADDIN;
        }

        // GET: api/<AddInController>
        [HttpGet]
        public Object Get()
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _addInService.getAllAddin());
           
        }

        // GET api/<AddInController>/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _addInService.getSingleAddin(id));
             
        }

        // POST api/<AddInController>
        [HttpPost]
        public Object Post(AddIn addIn)
        {
            _addInService.saveAddIn(addIn);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), true);
        }


       

        // DELETE api/<AddInController>/5
        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            _addInService.deleteAddInById(id);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.DELETE, moduleName), true);
        }
    }
}
