using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class OrderRepoImpl : IOrderRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public OrderRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.ORDER));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }
        public void deleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Order findById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> getAll()
        {
            throw new NotImplementedException();
        }

        public void saveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void updateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
