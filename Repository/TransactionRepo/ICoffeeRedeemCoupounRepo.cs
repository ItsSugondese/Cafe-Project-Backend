using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public interface ICoffeeRedeemCoupounRepo
    {
        List<CoffeeRedeemCoupoun> getAll();
        CoffeeRedeemCoupoun findById(Guid id);
        CoffeeRedeemCoupoun findByMemberId(int id);

        void saveCoffeeRedeem(CoffeeRedeemCoupoun crc);
        void updateCoffeeRedeem(Guid id);

        void deleteCoffeeRedeem(int id);
    }
}
