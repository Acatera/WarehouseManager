using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WMgr.Models
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public virtual int InvoiceId { get; set; } //link to invoice header

        public float Quantity { get; set; }
        public virtual Product Product { get; set; }
        public decimal SellPrice { get; set; }
    }
}