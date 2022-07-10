using RestaurantLibrary.Enums;
using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.ViewModels
{
    public class RestaurantConfirmation
    {
        [DisplayName("Name")]
        public string Firstname { get; set; }
        [DisplayName("Lastname")]
        public string Lastname { get; set; }
        [DisplayName("Restaurant name")]
        public Restaurant restaurant { get; set; }
        [DisplayName("Table for")]
        public int NumberOfSeats { get; set; }
        [DisplayName("Reservation date")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
