using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class OrderAddInMappingRepoImpl : IOrderAddInMappingRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public OrderAddInMappingRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.ORDER_ADD_IN));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }
        public void deleteOrderAddIn(int id)
        {
            throw new NotImplementedException();
        }

        public OrderAddInMapping findById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderAddInMapping> getAll()
        {
            throw new NotImplementedException();
        }

        public void saveOrderAddIn(OrderAddInMapping orderAddIn)
        {
            throw new NotImplementedException();
        }

        public void updateOrderAddIn(OrderAddInMapping orderAddIn)
        {
            throw new NotImplementedException();
        }
    }
}
