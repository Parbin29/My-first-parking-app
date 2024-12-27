namespace ParkingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Car> Cars { get; set; } = new();
        public decimal AccountBalance { get; set; }
    }
}