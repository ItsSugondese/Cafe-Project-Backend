using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.pojo.order;
using BisleriumCafeBackend.Services.AddInServices;
using BisleriumCafeBackend.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.OrderController
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : GenericController
    {
        private readonly IOrderService _orderService;
        private string moduleName;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            moduleName = ModuleNameConstants.ORDER;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public Object Get()
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), _orderService.getAllOrders());
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public Object Post(OrderRequestPojo order)
        {
            _orderService.saveOrder(order);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), true);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
