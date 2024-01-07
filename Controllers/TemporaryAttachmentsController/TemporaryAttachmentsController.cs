using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.pojo.TemporaryAttachments;
using BisleriumCafeBackend.Services.CoffeeServices;
using BisleriumCafeBackend.Services.TemporaryAttachmentsServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.TemporaryAttachmentsController
{
    [Route("api/temporary-attachments")]
    [ApiController]
    public class TemporaryAttachmentsController : GenericController
    {
        private readonly ITemporaryAttachmentsService temporaryAttachmentsService;
        private string moduleName;
        public TemporaryAttachmentsController(ITemporaryAttachmentsService temporaryAttachmentsService)
        {
            this.temporaryAttachmentsService = temporaryAttachmentsService;
            this.moduleName = ModuleNameConstants.TEMPORARY_ATTACHMENTS;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TemporaryAttachmentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TemporaryAttachmentsController>
        [HttpPost]
        public Object SaveTemporaryAttachments([FromForm] TemporaryAttachmentsDetailRequestPojo requestPojo)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), 
                temporaryAttachmentsService.SaveTemporaryAttachment(requestPojo));
        }

        // PUT api/<TemporaryAttachmentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TemporaryAttachmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
