using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.pojo.member;

namespace BisleriumCafeBackend.Services.MemberServices
{
    public interface IMemberService
    {
        void saveMember(MemberRequest member);

        Member? getSingleMember(int id);
        Member? getMemberByContactNumber(string contactNumber);

        List<Member> getAllMember();

        void deleteMemberById(int id);
    }
}
