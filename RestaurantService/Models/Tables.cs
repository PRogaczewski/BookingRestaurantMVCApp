using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Tables
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
