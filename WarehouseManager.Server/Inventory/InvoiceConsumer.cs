using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseManager.Models;

namespace WarehouseManager.Inventory
{
    public class InvoiceConsumer
    {
        Invoice _currentInvoice;
        public void ConsumeNextInvoice()
        {
            _currentInvoice = InvoiceQueue.DequeueInvoice();
        }
    }
}