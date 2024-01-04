namespace BisleriumCafeBackend.Model.Transaction
{
    public class CoffeeRedeemCoupoun
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
    }
}
