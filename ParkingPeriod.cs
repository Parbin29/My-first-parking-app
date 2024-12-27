namespace ParkingApp.Models
{
    public class ParkingPeriod
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Cost { get; set; }
    }
}
