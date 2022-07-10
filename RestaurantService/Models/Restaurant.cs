using RestaurantLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage = "Select category")]
        public Category Category { get; set; }
        [Display(Name = "Contact number")]
        [Required(ErrorMessage = "Enter your phone number.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Invalid Phone Number.")]
        public string ContactNumber { get; set; }
        [Required]
        public Address Address { get; set; }
        public int AddressId { get; set; }

        public List<Tables> Tables { get; set; } = new List<Tables>();
    }
}
