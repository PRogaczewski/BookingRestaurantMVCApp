using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantLibrary.Exceptions;
using RestaurantLibrary.Models;
using RestaurantLibrary.Services;
using RestaurantLibrary.ViewModels;
using System.Threading.Tasks;

namespace RestaurantBookingMVC.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITablesService _tablesService;
        public RestaurantController(IRestaurantService restaurantService, ITablesService tablesService, ITimeService timeService)
        {
            _restaurantService = restaurantService;
            _tablesService = tablesService;
        }

        [HttpGet]
        public ActionResult GetRestaurants()
        {
            return View(_restaurantService.GetAllRestaurants());
        }

        [HttpPost]
        public ActionResult GetRestaurantsByName(string searchName)
        {
            var restaurants = _restaurantService.GetAllRestaurants(searchName);

            return View("GetRestaurants", restaurants);
        }

        [HttpGet]
        public ActionResult RestaurantDetails(int id)
        {
            var restaurant = _restaurantService.GetRestaurant(id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var restaurantBookedTable = new RestaurantBookedTable()
            {
                Id = restaurant.Id,
                restaurant = restaurant,
                Category = restaurant.Category,
                ContactNumber = restaurant.ContactNumber,
                Address = restaurant.Address,
                Tables = restaurant.Tables,
            };

            return View(restaurantBookedTable);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant restaurant, Address address, int amount)
        {
           var table =  _tablesService.GetTable(amount);
           await _restaurantService.AddNewRestaurant(restaurant, address, table);

            return RedirectToAction(nameof(HomeController.Index));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public ActionResult Update(int id)
        {
            var restaurant = _restaurantService.GetRestaurant(id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            return View(restaurant);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Update(Restaurant restaurant, Address address, int amount)
        {
            var table = _tablesService.GetTable(amount);

            await _restaurantService.EditRestaurant(restaurant, address, table);

            return RedirectToAction(nameof(RestaurantController.GetRestaurants));
        }
    }
}
