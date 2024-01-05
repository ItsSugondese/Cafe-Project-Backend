using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Services.Transaction
{
    public interface IOrderService
    {
        void saveOrder(Order order);

        Order? getSingleMember(int id);

        List<Order> getAllOrders();

        void deleteOrderById(int id);
    }
}
