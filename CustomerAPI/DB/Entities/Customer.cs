using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.DB
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
