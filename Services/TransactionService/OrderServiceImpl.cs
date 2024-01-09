using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.order;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.MemberRepo;
using BisleriumCafeBackend.Repository.TransactionRepo;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderAddInMappingRepo _orderAddInMappingRepo;
        private readonly IMemberRepo _memberRepo;

        public OrderServiceImpl(IOrderRepo orderRepo, IOrderAddInMappingRepo orderAddInMappingRepo, IMemberRepo memberRepo)
        {
            _orderRepo = orderRepo;
            _orderAddInMappingRepo = orderAddInMappingRepo;
            _memberRepo = memberRepo;
        }
        public void deleteOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderResponse> getAllOrders()
        {
            return _orderRepo.getAllOrdersDetails();
        }

        public Order? getSingleMember(int id)
        {
            throw new NotImplementedException();
        }

        public void saveOrder(OrderRequestPojo requestPojo)
        {
            List<Order> orderList = _orderRepo.getAll();
            if (requestPojo.Id == null)
            {
                if (orderList.Count() > 0)
                {
                    Order lastOrder = orderList.Last();
                    requestPojo.Id = lastOrder.Id + 1;
                }
                else
                {
                    requestPojo.Id = 1;
                }
                _orderRepo.saveOrder(new Order
                {
                    Id = requestPojo.Id,
                    CoffeeId = requestPojo.CoffeeId,
                    HadDiscount = requestPojo.HadDiscount,
                    HadAddIn = requestPojo.HadAddIn,
                    MemberId = requestPojo.MemberId,
                    Price = requestPojo.Price,
                    RedeemId = requestPojo.RedeemId,
                    WasRedeem = requestPojo.WasRedeem,
                    HasPaid = false,
                    Date = DateOnly.FromDateTime(DateTime.Now)
            });

                if (requestPojo.WasRedeem)
                {

                    Member member = _memberRepo.findById(requestPojo.MemberId);
                    member.CoffeeCount = 0;
                    _memberRepo.updateMember(member);
                        
                    }
                


                if (requestPojo.HadAddIn)
                {
                    List<OrderAddInMapping> allOrderAddIn = _orderAddInMappingRepo.getAll();
                    int saveMappingId;
                    if (allOrderAddIn.Count() > 0)
                    {
                        OrderAddInMapping lastOrderAddIn = allOrderAddIn.Last();
                        saveMappingId = (int)((lastOrderAddIn.Id) + 1);
                    }
                    else
                    {
                        saveMappingId = 1;
                    }
                    List<int> addInsId = new List<int>();

                    foreach(int addIn in requestPojo.AddInsId)
                    {
                        _orderAddInMappingRepo.saveOrderAddIn(new OrderAddInMapping
                        {
                            Id = saveMappingId,
                            AddInId = addIn,
                            OrderId = (int) requestPojo.Id
                        });

                        saveMappingId++;
                    }
                }
            }
        }
    }
}
