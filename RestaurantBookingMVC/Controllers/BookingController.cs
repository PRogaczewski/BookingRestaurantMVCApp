using Microsoft.AspNetCore.Mvc;
using RestaurantLibrary.Services;
using RestaurantLibrary.ViewModels;
using System;
using System.Threading.Tasks;

namespace RestaurantBookingMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITablesService _tablesService;
        private readonly ITimeService _timeService;
        public BookingController(IRestaurantService restaurantService, ITablesService tablesService, ITimeService timeService)
        {
            _restaurantService = restaurantService;
            _tablesService = tablesService;
            _timeService = timeService;
        }

        [HttpPost]
        public async Task<IActionResult> GetConfirmation(RestaurantBookedTable bookedTable)
        {
            var startDate = _timeService.FullDate(bookedTable.StartDate, bookedTable.Time);
            var time = new TimeSpan(2, 0, 0);

            var restaurantConfirmation = new RestaurantConfirmation()
            {
                Firstname = bookedTable.Firstname,
                Lastname = bookedTable.Lastname,
                restaurant = bookedTable.restaurant,
                NumberOfSeats = bookedTable.NumberOfSeats,
                StartDate = startDate,
                EndDate = startDate.Add(time),
            };

            await _tablesService.BookTable(restaurantConfirmation);

            return View(restaurantConfirmation);
        }

        [HttpPost]
        public ActionResult Result(int inputId, DateTime inputDate, DateTime inputVal, int inputPeople)
        {
            var restaurant = _restaurantService.GetRestaurant(inputId);

            var fullTime = _timeService.FullDate(inputDate, inputVal);
            var isBooked = _tablesService.AvailableTable(restaurant, fullTime, inputPeople);

            if (!isBooked)
                return Json(new { success = true });
            else
                return Json(new { success = false });
        }
    }
}
