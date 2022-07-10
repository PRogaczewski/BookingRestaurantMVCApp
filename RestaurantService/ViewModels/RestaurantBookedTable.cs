using RestaurantLibrary.Enums;
using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.ViewModels
{
    public class RestaurantBookedTable
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(30)]
        public string Lastname { get; set; }
        public Restaurant restaurant { get; set; }
        public Category Category { get; set; }
        public string ContactNumber { get; set; }
        public Address Address { get; set; }
        [Required]
        [Display(Name = "Available tables")]
        public List<Tables> Tables { get; set; } = new List<Tables>();
        public int NumberOfSeats { get; set; }
        [Required(ErrorMessage = "Invalid date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Invalid time")]
        public DateTime Time { get; set; }
        public DateTime EndDate { get; set; }
    }
}