using Microsoft.AspNetCore.Mvc;
using ParkingApp.Models;

namespace ParkingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingController : ControllerBase
    {
        private static List<User> Users = new();

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            user.Id = Users.Count + 1;
            Users.Add(user);
            return Created("", user);
        }

        [HttpPost("begin")]
        public IActionResult BeginPeriod([FromBody] string licensePlate)
        {
            var car = Users.SelectMany(u => u.Cars).FirstOrDefault(c => c.LicensePlate == licensePlate);
            if (car == null) return NotFound("Car not found.");

            car.CurrentPeriod = new ParkingPeriod { StartTime = DateTime.Now };
            return Ok("Parking period started.");
        }

        [HttpPost("end")]
        public IActionResult EndPeriod([FromBody] string licensePlate)
        {
            var car = Users.SelectMany(u => u.Cars).FirstOrDefault(c => c.LicensePlate == licensePlate);
            if (car == null || car.CurrentPeriod == null) return NotFound("Car or parking period not found.");

            car.CurrentPeriod.EndTime = DateTime.Now;
            var hours = (car.CurrentPeriod.EndTime.Value - car.CurrentPeriod.StartTime).TotalHours;
            car.CurrentPeriod.Cost = CalculateCost(hours);
            return Ok(car.CurrentPeriod);
        }

        [HttpGet("cost/{userId}")]
        public IActionResult GetCost(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return NotFound("User not found.");

            return Ok(user.AccountBalance);
        }

        private decimal CalculateCost(double hours)
        {
            var fullHours = Math.Ceiling(hours);
            return fullHours <= 10 ? (decimal)(14 * fullHours) : (decimal)(6 * fullHours);
        }
    }
}
