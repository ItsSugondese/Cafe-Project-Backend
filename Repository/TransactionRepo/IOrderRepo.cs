using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface IOrderRepo
    {
        List<Order> getAll();
        Order findById(int id);

        void saveOrder(Order order);
        void updateOrder(Order order);

        void deleteOrder(int id);
    }
}
