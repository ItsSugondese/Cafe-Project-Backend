using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;

namespace BisleriumCafeBackend.Repository.MemberRepo
{
    public class MemberRepoImpl : IMemberRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public MemberRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.MEMBER));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }
        public void deleteMember(int id)
        {
            throw new NotImplementedException();
        }

        public Coffee findById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Member> getAll()
        {
            throw new NotImplementedException();
        }

        public void saveMember(Member member)
        {
            throw new NotImplementedException();
        }

        public void updateMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
