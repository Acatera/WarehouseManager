using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseManager.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Handled { get; set; }
    }
}