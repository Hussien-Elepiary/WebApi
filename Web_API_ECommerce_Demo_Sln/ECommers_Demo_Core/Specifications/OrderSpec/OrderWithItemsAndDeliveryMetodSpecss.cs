using ECommerce_Demo_Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Specifications.OrderSpec
{
    public class OrderWithItemsAndDeliveryMetodSpecss:BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMetodSpecss(string Email):base(
                O=>O.BuyerEmail == Email
            )
        {
            Includes.Add(O=>O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(O=>O.OrderDate);
        }

        public OrderWithItemsAndDeliveryMetodSpecss(string Email,int orderId) : base(
                O => O.BuyerEmail == Email && O.Id == orderId
            )
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
