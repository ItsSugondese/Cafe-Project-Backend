using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public interface ITransactionService
    {
        void saveTransaction(Transaction transaction);
    }
}
