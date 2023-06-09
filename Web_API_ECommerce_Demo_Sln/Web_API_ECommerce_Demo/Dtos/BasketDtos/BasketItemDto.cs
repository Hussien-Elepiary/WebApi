﻿using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;

namespace Web_API_ECommerce_Demo.Dtos.BasketDtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue ,ErrorMessage = "Price must be greater than zero!!")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Quantity must be at least one item ")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}