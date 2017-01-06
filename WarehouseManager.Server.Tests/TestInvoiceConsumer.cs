using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseManager.Models;
using WarehouseManager.Inventory;

namespace WarehouseManager.Tests
{
    [TestClass]
    public class TestInvoiceConsumer
    {
        [TestInitialize()]
        public void TestInitialize()
        {
            Invoice invoice = new Invoice();
            invoice.Details = new List<InvoiceDetail>();
            invoice.Details.Add(new InvoiceDetail() { Quantity = 1, SellPrice = 10.99m, Product = new Product() { ProductName = "TestProduct" } });
            InvoiceQueue.EnqueueInvoice(invoice);
        }

        [TestCleanup()]
        public void TestCleanup()
        {

        }

        [TestMethod]
        public void TestEmptyInvoiceConsumption()
        {
            var consumer = new InvoiceConsumer();
            consumer.ConsumeNextInvoice();
        }
    }
}
