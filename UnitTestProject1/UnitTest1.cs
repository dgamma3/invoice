using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using XeroTechnicalTest;

namespace UnitTestProject1
{
    [TestClass]
    public class InvoiceUnitTest
    {
        #region AddInvoiceLine
        [TestMethod]
        public void AddValidInvoiceLines()
        {
            //arrange
            var invoice = new Invoice();
            var invoiceItem1 = new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.21,
                Quantity = 4,
                Description = "Banana"
            };

            var invoiceItem2 = new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5.21,
                Quantity = 1,
                Description = "Orange"
            };

            //act
            invoice.AddInvoiceLine(invoiceItem1);
            invoice.AddInvoiceLine(invoiceItem2);           

            //Assert
            Assert.IsTrue(invoice.LineItems.Count == 2, "LineItems not added successfully");
            Assert.AreSame(invoice.LineItems[0], invoiceItem1, "AddInvoiceLine does not return same result as inserted");
        


        }

        [TestMethod]
        public void AddNullInvoiceLine()
        {
            //arrange
            var invoice = new Invoice();
           
            //act
            var result = invoice.AddInvoiceLine(null);

            //Assert
            Assert.IsFalse(result.Success, "should not be able to add null invoice item");

        }

        [TestMethod]
        public void AddInvalidCostInvoiceLine()
        {
            //arrange
            var invoice = new Invoice();

            //act
            var result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 0,
                Quantity = 1,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must have a non-zero cost");

            //arrange
             invoice = new Invoice();

            //act
            result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = -1,
                Quantity = 1,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must have a non-negative quantity");
        }
     
        [TestMethod]
        public void AddInvalidQuantityInvoiceLine()
        {
            //arrange
            var invoice = new Invoice();

            //act
            var result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5,
                Quantity = 0,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must have a non-zero quantity");

            //arrange
            invoice = new Invoice();

            //act
            result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5,
                Quantity = -1,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must have a non-negative quantity");
        }
                
        [TestMethod]
        public void AddInvalidInvoiceLineIdInvoiceLine()
        {
            //arrange
            var invoice = new Invoice();

            //act
            var result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5,
                Quantity = 0,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must have a non-zero quantity");

            //arrange
            invoice = new Invoice();

            //act
            result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = -2,
                Cost = 5,
                Quantity = 5,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must have a non-negative quantity");

            //arrange
            invoice = new Invoice();

            //act
           invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 5,
                Quantity = 5,
                Description = "Orange"
            });

            result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = 5,
                Quantity = 5,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Item must incrument sequencially");

        }

        [TestMethod]
        public void AddInvalidDescriptionInvoiceLine()
        {
            //arrange
            var invoice = new Invoice();

            //act
            var result = invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5,
                Quantity = 5,
                Description = ""
            });

            //Assert
            Assert.IsFalse(result.Success, "InvoiceLine must have a description");

        }

        #endregion
        #region RemoveInvoiceLine 

        [TestMethod]
        public void RemoveValidInvoiceLines()
        {
            //arrange
            var invoice = new Invoice();
            var invoiceItem1 = new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.21,
                Quantity = 4,
                Description = "Banana"
            };

            var invoiceItem2 = new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5.21,
                Quantity = 1,
                Description = "Orange"
            };

            var invoiceItem3 = new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = 5.21,
                Quantity = 1,
                Description = "Orange3"
            };


            //act
            invoice.AddInvoiceLine(invoiceItem1);
            invoice.AddInvoiceLine(invoiceItem2);
            invoice.AddInvoiceLine(invoiceItem3);
            invoice.RemoveInvoiceLine(2);
            invoice.AddInvoiceLine(invoiceItem3);

            //Assert
            Assert.IsTrue(invoice.LineItems.Count == 1, "LineItems was not removed successfully ");
            Assert.AreSame(invoice.LineItems[0], invoiceItem1, "Wrong LineItem removed");




        }

        #endregion


    }
}
