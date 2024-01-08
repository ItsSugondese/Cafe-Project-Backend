namespace BisleriumCafeBackend.Model.Coffee
{
    public class Coffee
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string FilePath { get; set; }

        public Coffee() { }
    }
}
