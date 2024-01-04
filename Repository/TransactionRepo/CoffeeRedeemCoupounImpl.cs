using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class CoffeeRedeemCoupounImpl : ICoffeeRedeemCoupounRepo
    {

        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public CoffeeRedeemCoupounImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.COFFEE_REDEEM));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public void deleteCoffeeRedeem(int id)
        {
            throw new NotImplementedException();
        }

        public Order findById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CoffeeRedeemCoupoun> getAll()
        {
            throw new NotImplementedException();
        }

        public void saveCoffeeRedeem(CoffeeRedeemCoupoun crc)
        {
            throw new NotImplementedException();
        }

        public void updateCoffeeRedeem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
