using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface ICoffeeRedeemCoupounRepo
    {
        List<CoffeeRedeemCoupoun> getAll();
        Order findById(int id);

        void saveCoffeeRedeem(CoffeeRedeemCoupoun crc);
        void updateCoffeeRedeem(int id);

        void deleteCoffeeRedeem(int id);
    }
}
