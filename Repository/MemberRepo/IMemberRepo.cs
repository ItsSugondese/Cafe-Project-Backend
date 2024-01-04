using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;

namespace BisleriumCafeBackend.Repository.MemberRepo
{
    public interface IMemberRepo
    {
        List<Member> getAll();
        Coffee findById(int id);

        void saveMember(Member member);
        void updateMember(Member member);

        void deleteMember(int id);
    }
}
