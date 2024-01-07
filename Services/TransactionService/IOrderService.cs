using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.order;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public interface IOrderService
    {
        void saveOrder(OrderRequestPojo requestPojo);

        Order? getSingleMember(int id);

        List<OrderResponse> getAllOrders();

        void deleteOrderById(int id);
    }
}
