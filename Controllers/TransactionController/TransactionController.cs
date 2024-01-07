using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.TransactionController
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : GenericController
    {
        private readonly ITransactionService _transactionService;
        private string moduleName;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            moduleName = ModuleNameConstants.TRANSACTION;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransactionController>
        [HttpPost]
        public Object Post(Model.Transaction.Transaction transaction)
        {
            _transactionService.saveTransaction(transaction);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), true);
        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
