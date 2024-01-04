using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;

namespace BisleriumCafeBackend.Services.MemberServices
{
    public interface IMemberService
    {
        void saveMember(Member coffee);

        AddIn getSingleMember(int id);

        List<AddIn> getAllMember();

        void deleteMemberById(int id);
    }
}
