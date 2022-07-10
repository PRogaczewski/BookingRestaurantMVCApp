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
    public class GetRestaurantInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        public Address Address { get; set; }
        public int AddressId { get; set; }
        [DisplayName("Availables tables")]
        public List<Tables> Tables { get; set; } = new List<Tables>();
    }
}
