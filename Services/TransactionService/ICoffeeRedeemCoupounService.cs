using BisleriumCafeBackend.Model.Transaction;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public interface ICoffeeRedeemCoupounService
    {
        void saveCoffeeRedeem(CoffeeRedeemCoupoun redeem);
    }
}
