using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.pojo.member;

namespace BisleriumCafeBackend.Services.MemberServices
{
    public interface IMemberService
    {
        Member saveMember(MemberRequest member);

        Member? getSingleMember(int id);
        Member? getMemberByContactNumber(string contactNumber);

        List<Member> getAllMember();
        bool toggleMembership(int id);

        void deleteMemberById(int id);
    }
}
