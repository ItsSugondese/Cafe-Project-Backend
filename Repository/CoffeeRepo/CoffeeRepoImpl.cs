using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Coffee;

namespace BisleriumCafeBackend.Repository.CoffeeRepo
{
    public class CoffeeRepoImpl : ICoffeeRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public CoffeeRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.COFFEE));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public void deleteCoffee(int id)
        {
            throw new NotImplementedException();
        }

        public Coffee findById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Coffee> getAll()
        {
            throw new NotImplementedException();
        }

        public void saveCoffee(Coffee coffee)
        {
            throw new NotImplementedException();
        }

        public void updateCoffee(Coffee coffee)
        {
            throw new NotImplementedException();
        }
    }
}
