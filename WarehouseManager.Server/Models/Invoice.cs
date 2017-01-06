using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WarehouseManager.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public int Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        public bool Validated { get; set; }

        public virtual ICollection<InvoiceDetail> Details { get; set; }
        public object Total {
            get
            {
                var total = 0m;

                foreach (var detail in Details)
                {
                    total += detail.SellPrice * detail.Quantity;
                }

                return total;
            }
        }
    }
}