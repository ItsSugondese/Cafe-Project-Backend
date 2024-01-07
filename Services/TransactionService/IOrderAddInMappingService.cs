using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public interface IOrderAddInMappingService
    {
        void saveOrderAddInMapping(OrderAddInMapping orderAddInMapping);
    }
}
