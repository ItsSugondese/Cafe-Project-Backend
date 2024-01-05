using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.TransactionRepo;

namespace BisleriumCafeBackend.Services.Transaction
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderServiceImpl(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public void deleteOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> getAllOrders()
        {
            return _orderRepo.getAll();
        }

        public Order? getSingleMember(int id)
        {
            throw new NotImplementedException();
        }

        public void saveOrder(Order order)
        {
            List<Order> orderList = _orderRepo.getAll();
            if (order.Id == null)
            {
                if (orderList.Count() > 0)
                {
                    Order lastOrder = orderList.Last();
                    order.Id = lastOrder.Id + 1;
                }
                else
                {
                    order.Id = 1;
                }
                _orderRepo.saveOrder(order);
            }
        }
    }
}
