using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface IOrderAddInMappingRepo
    {
        List<OrderAddInMapping> getAll();
        OrderAddInMapping? findById(int id);
        List<OrderAddInMapping> findByOrderId(int id);


        void saveOrderAddIn(OrderAddInMapping orderAddIn);
        void updateOrderAddIn(OrderAddInMapping orderAddIn);

        void deleteOrderAddIn(int id);
    }
}
