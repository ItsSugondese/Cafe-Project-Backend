

using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface ITransactionRepo
    {
        void saveTransaction(Transaction transaction);
        List<Transaction> getAll();
    }
}
