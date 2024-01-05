namespace BisleriumCafeBackend.pojo.order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int MemberName { get; set; }
        public DateOnly Date { get; set; } 
        public double Price { get; set; }
        public string CoffeeName { get; set; }
        public List<string> AddInName { get; set; }
        
    }
}
