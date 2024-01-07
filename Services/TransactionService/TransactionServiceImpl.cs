using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.Repository.TransactionRepo;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public class TransactionServiceImpl : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IOrderRepo _orderRepo;
        public TransactionServiceImpl(ITransactionRepo transactionRepo, IOrderRepo orderRepo)
        {
            _transactionRepo = transactionRepo;
            _orderRepo = orderRepo;
        }
        public void saveTransaction(Transaction transaction)
        {
            Order order = _orderRepo.findById(transaction.OrderId);
            order.HasPaid = true;
            _orderRepo.updateOrder(order);
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
