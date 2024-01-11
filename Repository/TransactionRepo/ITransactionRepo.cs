

using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface ITransactionRepo
    {
        void saveTransaction(Transaction transaction);
        List<Transaction> getAll();

        List<TransactionResponse> getAllTransactionDetails();
    }
}
