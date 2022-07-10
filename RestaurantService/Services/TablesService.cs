using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.Connection;
using RestaurantLibrary.Exceptions;
using RestaurantLibrary.Models;
using RestaurantLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantLibrary.Services
{
    public interface ITablesService
    {
        Tables GetTable(int amount);
        IEnumerable<RestaurantTable> GetTableById(int id, int n);
        bool AvailableTable(Restaurant restaurant, DateTime Start, int people);
        Task BookTable(RestaurantConfirmation restaurant);
    }
    public class TablesService : ITablesService
    {
        private readonly RestaurantContext _context;
        public TablesService(RestaurantContext context = null)
        {
            _context = context;
        }

        public bool AvailableTable(Restaurant restaurant, DateTime Start, int people)
        {
            var bookedTable = _context.bookedTables
                .Where(t => t.RestaurantId == restaurant.Id)
                .Where(t => t.Table.NumberOfSeats == people)
                .ToList();

            var AllRestaurantTables = GetTableById(restaurant.Id, people).ToList();

            var AllBookedTables = bookedTable.FindAll(b => b.StartDate == Start);

            if (AllBookedTables == null || AllBookedTables.Count() < AllRestaurantTables.Count())
            {
                return false;
            }
            return true;
        }

        public async Task BookTable(RestaurantConfirmation restaurant)
        {
            var table = await _context.tables.FirstOrDefaultAsync(t => t.NumberOfSeats == restaurant.NumberOfSeats);
            var getRestaurant = await _context.restaurants.SingleAsync(r => r.Id == restaurant.restaurant.Id);
            var bookTable = new BookedTable()
            {
                Firstname=restaurant.Firstname,
                Lastname=restaurant.Lastname,
                StartDate = restaurant.StartDate,
                EndDate = restaurant.EndDate,
                Restaurant = getRestaurant,
                Table = table,
            };

            await _context.bookedTables.AddAsync(bookTable);
            await _context.SaveChangesAsync();
        }

        public Tables GetTable(int amount)
        {
            var table = _context.tables.FirstOrDefault(t => t.NumberOfSeats == amount);

            return table;
        }

        public IEnumerable<RestaurantTable> GetTableById(int id, int n)
        {
            var table = _context.restaurantTable
                .Where(t => t.RestaurantId == id)
                .Include(t => t.Table)
                .Where(t => t.Table.NumberOfSeats == n)
                .ToList();

            return table;
        }
    }
}
