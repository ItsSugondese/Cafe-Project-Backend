using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.Repository.MemberRepo;
using BisleriumCafeBackend.Repository.TransactionRepo;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public class TransactionServiceImpl : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly ICoffeeRedeemCoupounRepo _coffeeRedeemCoupounRepo;
        public TransactionServiceImpl(ITransactionRepo transactionRepo, IOrderRepo orderRepo, IMemberRepo memberRepo, ICoffeeRedeemCoupounRepo coffeeRedeemCoupounRepo)
        {
            _transactionRepo = transactionRepo;
            _orderRepo = orderRepo;
            _memberRepo = memberRepo;
            _coffeeRedeemCoupounRepo = coffeeRedeemCoupounRepo;
        }
        public void saveTransaction(Transaction transaction)
        {
            Order order = _orderRepo.findById(transaction.OrderId);
            order.HasPaid = true;
            _orderRepo.updateOrder(order);

            Member member = _memberRepo.findById(transaction.MemberId);
            if (member.IsMember)
            {
                member.CoffeeCount = member.CoffeeCount + 1;
                _memberRepo.updateMember(member);
            }

            List<Transaction> transactionList = _transactionRepo.getAll();
            if (transaction.Id == null)
            {
                if (transactionList.Count() > 0)
                {
                    Transaction lastTransaction = transactionList.Last();
                    transaction.Id = lastTransaction.Id + 1;
                }
                else
                {
                    transaction.Id = 1;
                }
                _transactionRepo.saveTransaction(transaction);
            }
        }
    }
}
