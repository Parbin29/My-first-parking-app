namespace ParkingApp.Models
{
    public class Car
    {
       public string LicensePlate { get; set; }
        public ParkingPeriod? CurrentPeriod { get; set; }
    }
}
