using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public Tables Table { get; set; }
        public int TableId { get; set; }
    }
}
