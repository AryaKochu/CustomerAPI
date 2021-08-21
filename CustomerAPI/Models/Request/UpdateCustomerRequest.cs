using CustomerAPI.DB;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Models
{
    public class UpdateCustomerRequest
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
