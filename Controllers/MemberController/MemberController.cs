using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Generics.Controller;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Services.CoffeeServices;
using BisleriumCafeBackend.Services.MemberServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisleriumCafeBackend.Controllers.MemberController
{
    [Route("api/[controller]")]
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

        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.GET, moduleName), _memberService.getSingleMember(id));
        }

        // POST api/<MemberController>
        [HttpPost]
        public Object Post(Member member)
        {
            _memberService.saveMember(member);
            return SuccessResponse(MessageConstantsMerge.requetMessage(MessageConstants.POST, moduleName), true);
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
