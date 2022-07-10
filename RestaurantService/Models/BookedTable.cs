using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class BookedTable
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; } 
        public Tables Table { get; set; }
        public int TableId { get; set; }
    }
}
