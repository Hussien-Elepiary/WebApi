using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pinding")]
        Pinding,
        [EnumMember(Value = "PaymentReceivd")]
        PaymentReceivd,
        [EnumMember(Value = "PaymentFaild")]
        PaymentFaild
    }
}
