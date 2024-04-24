using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class PaymentRecordModel
    {
        public int? Id { get; set; }
        public int? SubscriberId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? DebtOrOverpayment { get; set; }
        public string? SubscriberName { get; set; } 
    }

}
