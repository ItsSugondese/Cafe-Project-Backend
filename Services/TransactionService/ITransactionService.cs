using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.Transaction;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public interface ITransactionService
    {
        void saveTransaction(Transaction transaction);

        List<TransactionResponse> getTransactionDetails();

        void transactionDataForPdf();
    }
}
