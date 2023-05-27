﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Entities.Cart
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
