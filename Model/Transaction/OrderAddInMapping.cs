namespace BisleriumCafeBackend.Model.Transaction
{
    public class OrderAddInMapping
    {
        public int? Id { get; set; }
        
        public int OrderId { get; set; }
        public int AddInId { get; set; }
    }
}
