using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.pojo.member;
using BisleriumCafeBackend.Services.CoffeeServices;
using BisleriumCafeBackend.Services.MemberServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.MemberController
{
    [Route("api/member")]
    [ApiController]
    public class MemberController : GenericController
    {
        private readonly IMemberService _memberService;
        private string moduleName;


        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
            moduleName = ModuleNameConstants.MEMBER;
        }

        
        // GET: api/<MemberController>
        [HttpGet]
        public Object Get()
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _memberService.getAllMember());
        }

        [HttpGet("toggle/{id}")]
        public Object ToggleMembership(int id)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _memberService.toggleMembership(id));
        }

        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public Object GetById(int id)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _memberService.getSingleMember(id));
        }

        [HttpGet("contact/{contactNumber}")]
        public Object GetByContactNumber(string contactNumber)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _memberService.getMemberByContactNumber(contactNumber));
        }

        // POST api/<MemberController>
        [HttpPost]
        public Object Post(MemberRequest member)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), _memberService.saveMember(member));
        }

        // PUT api/<MemberController>/5
      

        // DELETE api/<MemberController>/5
        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            _memberService.deleteMemberById(id);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.DELETE, moduleName), true);
        }
    }
}
