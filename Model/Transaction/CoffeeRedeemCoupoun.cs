namespace BisleriumCafeBackend.Model.Transaction
{
    public class CoffeeRedeemCoupoun
    {
        public Guid Id { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
        public bool IsRedeem {  get; set; }
    }
}
