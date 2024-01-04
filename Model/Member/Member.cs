namespace BisleriumCafeBackend.Model.Member
{
    public class Member
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMember {  get; set; }
        public int CoffeeCount { get; set; }
    }
}
