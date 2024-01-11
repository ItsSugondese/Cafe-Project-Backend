using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.order;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface IOrderRepo
    {
        List<Order> getAll();

        List<OrderResponse> getAllOrdersDetails();
        OrderResponse getSingleOrderDetails(int id);
        Order? findById(int id);

        void saveOrder(Order order);
        void updateOrder(Order order);

        void deleteOrder(int id);
    }
}
