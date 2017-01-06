using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarehouseManager.Models;

namespace WarehouseManager.Inventory
{
    public static class InvoiceQueue
    {
        private static Queue<Invoice> queue = new Queue<Invoice>();

        public static void EnqueueInvoice(Invoice invoice)
        {
            queue.Enqueue(invoice);
        }

        public static Invoice DequeueInvoice()
        {
            return queue.Dequeue();
        }

        public static int Count()
        {
            return queue.Count();
        }

        public static void Clean()
        {
            throw new NotImplementedException();
        }
    }
}