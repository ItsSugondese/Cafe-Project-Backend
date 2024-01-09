using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.pojo.member;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.MemberRepo;

namespace BisleriumCafeBackend.Services.MemberServices
{
    public class MemberServiceImpl : IMemberService
    {
        private readonly IMemberRepo _memberRepo;
        public MemberServiceImpl(IMemberRepo memberRepo)
        {
            _memberRepo = memberRepo;
        }
        public void deleteMemberById(int id)
        {
            errorWhenMemberNotExist(id);
            _memberRepo.deleteMember(id);
        }

        public List<Member> getAllMember()
        {
            return _memberRepo.getAll();
        }

        public Member? getSingleMember(int id)
        {
            return _memberRepo.findById(id);
        }

        //public void saveMember(MemberRequest memberRequest)
        //{
        //    List<Member> memberList =  _memberRepo.getAll();

        //    errorWhenPhoneNumberAlreadyExist(memberRequest);
        //    if (memberRequest.Id == null)
        //    {
        //        if (memberList.Count() > 0)
        //        {
        //            Member lastMember = memberList.Last();
        //            memberRequest.Id = lastMember.Id + 1;
        //        }
        //        else
        //        {
        //            memberRequest.Id = 1;
        //        }
        //        _memberRepo.saveMember(memberRequest);
        //    }
        //    else
        //    {
        //        errorWhenMemberNotExist(memberRequest.Id ?? 0);
        //        _memberRepo.updateMember(memberRequest);
        //    }
        //}

        public Member saveMember(MemberRequest memberRequest)
        {
            List<Member> memberList = _memberRepo.getAll();
                if (memberList.Count() > 0)
                {
                    Member lastMember = memberList.Last();
                    memberRequest.Id = lastMember.Id + 1;
                }
                else
                {
                    memberRequest.Id = 1;
                }
                Member member = new Member
                {
                    CoffeeCount = 0,
                    Id = memberRequest.Id,
                    IsMember = false,
                    Name = memberRequest.Name,
                    PhoneNumber = memberRequest.PhoneNumber
                };
            _memberRepo.saveMember(member);
            return member;
         }
        

        Member? IMemberService.getMemberByContactNumber(string contactNumber)
        {
            return _memberRepo.findByPhoneNumber(contactNumber);
        }
        private void errorWhenMemberNotExist(int id)
        {
            if (_memberRepo.findById(id) == null)
            {
                throw new Exception(MessageConstantsMerge.notExist("id", ModuleNameConstants.MEMBER));
            }
        }
        private void errorWhenPhoneNumberAlreadyExist(MemberRequest member)
        {
            Member? checkMember = _memberRepo.findByPhoneNumber(member.PhoneNumber);
            if (checkMember != null && checkMember.Id != checkMember.Id)
            {
                throw new Exception(MessageConstantsMerge.alreadyExist("contact", ModuleNameConstants.MEMBER));
            }
        }

        public bool toggleMembership(int id)
        {
            Member member = getSingleMember(id);
            if (member.IsMember == true)
            {
                member.IsMember = false;
            }else
            {
                member.IsMember = true;
            }

            _memberRepo.updateMember(member);
            return member.IsMember;
        }
    }
}
