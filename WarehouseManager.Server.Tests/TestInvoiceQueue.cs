using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseManager.Inventory;
using WarehouseManager.Models;
using System.Collections.Generic;

namespace WarehouseManager.Tests
{
    [TestClass]
    public class TestInvoiceQueue
    {
        [TestMethod]
        public void TestInvoiceEnqueueingAndCount()
        {
            InvoiceQueue.Clear();

            Invoice invoice = new Invoice();
            invoice.Details = new List<InvoiceDetail>();
            invoice.Details.Add(new InvoiceDetail() { Quantity = 1, SellPrice = 10.99m, Product = new Product() { ProductName = "TestProduct" } });
            InvoiceQueue.EnqueueInvoice(invoice);

            Assert.AreEqual(1, InvoiceQueue.Count());
        }

        [TestMethod]
        public void TestInvoiceQueueCleaning()
        {
            InvoiceQueue.Clear();
        }

        [TestMethod]
        public void TestInvoiceDequeueing()
        {
            Invoice invoice = new Invoice();
            invoice.Details = new List<InvoiceDetail>();
            invoice.Details.Add(new InvoiceDetail() { Quantity = 1, SellPrice = 10.99m, Product = new Product() { ProductName = "TestProduct" } });
            InvoiceQueue.EnqueueInvoice(invoice);

            var newInvoice = InvoiceQueue.DequeueInvoice();
            Assert.IsNotNull(newInvoice);
        }
    }
}
