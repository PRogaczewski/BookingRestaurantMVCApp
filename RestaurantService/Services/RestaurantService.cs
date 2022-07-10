using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.Connection;
using RestaurantLibrary.Models;
using RestaurantLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetAllRestaurants();
        IEnumerable<Restaurant> GetAllRestaurants(string name);
        Restaurant GetRestaurant(int id);
        Task AddNewRestaurant(Restaurant restaurant, Address address, Tables table);
        Task BookTable(RestaurantConfirmation confirmation);
        Task EditRestaurant(Restaurant restaurant, Address address, Tables table);
    }
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantContext _context;
        public RestaurantService(RestaurantContext Context)
        {
            _context = Context;
        }

        public async Task AddNewRestaurant(Restaurant restaurant, Address address, Tables table)
        {
            var CreatedRestaurant = new Restaurant()
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                ContactNumber = restaurant.ContactNumber,
                Address = new Address()
                {
                    City = address.City,
                    Street = address.Street
                },
            };

            CreatedRestaurant.Tables.Add(table);

            await _context.restaurants.AddAsync(CreatedRestaurant);
            await _context.SaveChangesAsync();
        }

        public async Task EditRestaurant(Restaurant restaurant, Address address, Tables table)
        {
            var updatedRestaurant = GetRestaurant(restaurant.Id);

            updatedRestaurant.Name = restaurant.Name;
            updatedRestaurant.Description = restaurant.Description;
            updatedRestaurant.Category = restaurant.Category;
            updatedRestaurant.ContactNumber = restaurant.ContactNumber;
            updatedRestaurant.Address = address;
            updatedRestaurant.Tables.Add(table);

            await _context.SaveChangesAsync();
        }
        public async Task BookTable(RestaurantConfirmation confirmation)
        {
            var booked = new BookedTable()
            {
                StartDate = confirmation.StartDate,
                EndDate = confirmation.EndDate,
                Restaurant  = confirmation.restaurant,
                RestaurantId = confirmation.restaurant.Id,
                Table = confirmation.restaurant.Tables.FirstOrDefault(t=>t.NumberOfSeats==confirmation.NumberOfSeats),
            };
            await _context.bookedTables.AddAsync(booked);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            var restaurants = _context.restaurants
                .Include(r => r.Address)
                .Include(r=>r.Tables)
                .ToList();

            return restaurants;
        }

        public Restaurant GetRestaurant(int id)
        {
            var restaurant = _context.restaurants
                .Include(r=>r.Address)
                .Include(r=>r.Tables)
                .FirstOrDefault(r => r.Id == id);

            return restaurant;
        }

        public IEnumerable<Restaurant> GetAllRestaurants(string name)
        {
            var restaurants = _context.restaurants
                .Where(r=>r.Name.Contains(name))
                .Include(r => r.Address)
                .Include(r => r.Tables)
                .ToList();

            return restaurants;
        }
    }
}
