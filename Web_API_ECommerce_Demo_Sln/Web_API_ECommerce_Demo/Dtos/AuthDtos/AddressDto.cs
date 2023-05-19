﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_API_ECommerce_Demo.Dtos.AuthDtos
{
    public class AddressDto
    { 
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
