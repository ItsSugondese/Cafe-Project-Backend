using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;

namespace BisleriumCafeBackend.Repository.MemberRepo
{
    public interface IMemberRepo
    {
        List<Member> getAll();
        Member? findById(int id);
        Member? findByPhoneNumber(string contact);

        void saveMember(Member member);
        void updateMember(Member member);

        void deleteMember(int id);
    }
}
