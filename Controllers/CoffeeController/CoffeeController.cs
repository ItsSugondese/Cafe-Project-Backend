using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.pojo.coffee;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Services.AddInServices;
using BisleriumCafeBackend.Services.CoffeeServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.CoffeeController
{
    [Route("api/coffee")]
    [ApiController]
    public class CoffeeController : GenericController
    {
        private readonly ICoffeeService _coffeeService;
        private string moduleName;
        private readonly ICoffeeRepo _coffeeRepo;


        public CoffeeController(ICoffeeService coffeeService, ICoffeeRepo coffeeRepo)
        {
            _coffeeRepo = coffeeRepo;
            _coffeeService = coffeeService;
            moduleName = ModuleNameConstants.COFFEE;
        }
        
        
        // GET: api/<CoffeeController>
        [HttpGet]
        public Object Get()
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _coffeeService.getAllCoffee());
        }

        // GET api/<CoffeeController>/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _coffeeService.getSingleCoffee(id));
        }

        [HttpGet("doc/{id}")]
        public Object GetDocs(int id)
        {
            string? photoPath = _coffeeRepo.findById(id)?.FilePath;

            if (photoPath != null && !string.IsNullOrEmpty(photoPath))
            {
                //genericFileUtils.GetFileFromFilePath(photoPath, response);
                Byte[] b = System.IO.File.ReadAllBytes(photoPath);   // You can use your own method over here.         
                return File(b, "image/jpeg");
            }
   
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), true);
        }

        // POST api/<CoffeeController>
        [HttpPost]
        public Object Post(CoffeeRequest coffee)
        {
            _coffeeService.saveCoffee(coffee);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), true);
        }

        

        // DELETE api/<CoffeeController>/5
        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            _coffeeService.deleteCoffeeById(id);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.DELETE, moduleName), true);
        }
    }
}
