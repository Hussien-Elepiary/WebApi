using ECommerce_Demo_Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Specifications.OrderSpec
{
    public class OrderByPaymentIntentId:BaseSpecification<Order>
    {
        public OrderByPaymentIntentId(string paymentIntentId) : base(O=>O.PaymentIntentId == paymentIntentId) { }
    }
}
