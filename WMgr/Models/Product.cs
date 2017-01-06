using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseManager.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}