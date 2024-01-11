using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.pojo.coffee;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.CoffeeRepo;
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
        private readonly IAddInRepo _addInRepo;
        private string moduleName;

        public AddInController(IAddInService addInService, IAddInRepo addInRepo)
        {
           _addInService = addInService;
            moduleName = ModuleNameConstants.ADDIN;
            _addInRepo = addInRepo;
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

        [HttpGet("doc/{id}")]
        public Object GetDocs(int id)
        {
            string? photoPath = _addInRepo.findById(id)?.FilePath;

            if (photoPath != null && !string.IsNullOrEmpty(photoPath))
            {
                //genericFileUtils.GetFileFromFilePath(photoPath, response);
                Byte[] b = System.IO.File.ReadAllBytes(photoPath);   // You can use your own method over here.         
                return File(b, "image/jpeg");
            }

            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), true);
        }

        // POST api/<AddInController>
        [HttpPost]
        public Object Post(CoffeeRequest addIn)
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
