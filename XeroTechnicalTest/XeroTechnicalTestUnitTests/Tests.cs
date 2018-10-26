using System;
using NUnit.Framework;
using XeroTechnicalTest;

namespace XeroTechnicalTestUnitTests
{
    [TestFixture]
    public class Tests
    {
        #region AddInvoiceLine
        [Test]
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

        [Test]
        public void AddNullInvoiceLine()
        {
            //arrange
            var invoice = new Invoice();
           
            //act
            var result = invoice.AddInvoiceLine(null);

            //Assert
            Assert.IsFalse(result.Success, "should not be able to add null invoice item");

        }

        [Test]
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
     
        [Test]
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
                
        [Test]
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
                InvoiceLineId = 1,
                Cost = 5,
                Quantity = 5,
                Description = "Orange"
            });

            //Assert
            Assert.IsFalse(result.Success, "Invoice Line Id must be unique.");

        }

        [Test]
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

        [Test]
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


            //act
            invoice.AddInvoiceLine(invoiceItem1);
            var result = invoice.RemoveInvoiceLine(1);
   
            //assert
            Assert.IsTrue(invoice.LineItems.Count == 0, "LineItems was not removed successfully ");
            Assert.IsTrue(result.Success, "LineItems was not removed successfully ");
        }
        
        [Test]
        public void RemoveInValidInvoiceLines()
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


            //act
            invoice.AddInvoiceLine(invoiceItem1);
            var result = invoice.RemoveInvoiceLine(2);
   
            //assert
            Assert.IsTrue(invoice.LineItems.Count == 1, "No LineItems should have been removed");
            Assert.IsTrue(invoice.LineItems[0] == invoiceItem1, "No LineItems should have been removed");
            Assert.IsFalse(result.Success, "RemoveInvoiceLine should have failed");
        }
        #endregion

        #region Clone
        [Test]
        public void TestClone()
        {
            //arrange
            var originalInvoice = new Invoice();
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
            originalInvoice.AddInvoiceLine(invoiceItem1);
            originalInvoice.AddInvoiceLine(invoiceItem2);        
            var clonedInvoice = originalInvoice.Clone();    

            //Assert          
            Assert.IsTrue(originalInvoice.GetHashCode() == clonedInvoice.GetHashCode(), "cloned object is different to source object");
            Assert.IsFalse(ReferenceEquals(clonedInvoice.LineItems[0],originalInvoice.LineItems[0]), "cloned object must not have any references to original object");
            Assert.IsFalse(ReferenceEquals(clonedInvoice.LineItems[1],originalInvoice.LineItems[1]), "cloned object must not have any references to original object");
        }
        

        #endregion

        #region MergeInvoices
        [Test]
        public void TestMergeInvoicesWorks()
        {
            //arrange
            var originalInvoice = new Invoice();
            var invoiceLine = new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.21,
                Quantity = 4,
                Description = "Banana"
            };
            originalInvoice.LineItems.Add(invoiceLine);

            var invoiceToMerge = new Invoice();
            var invoiceLine2 = new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5.21,
                Quantity = 1,
                Description = "Orange"
            };
            invoiceToMerge.LineItems.Add(invoiceLine2);
         
            //act
            originalInvoice.MergeInvoices(invoiceToMerge);

            //Assert
            Assert.IsTrue(originalInvoice.LineItems.Count == 2, "Did not merge correctly, should have two items");
            Assert.IsTrue(originalInvoice.LineItems[0].GetHashCode() ==  invoiceLine.GetHashCode() , "cloned object values is different to source object");
            Assert.IsTrue(originalInvoice.LineItems[1].GetHashCode() == invoiceLine2.GetHashCode() , "cloned object values is different to source object");
            Assert.IsTrue(ReferenceEquals(originalInvoice.LineItems[0],invoiceLine), "objects should be same");
            Assert.IsFalse(ReferenceEquals(originalInvoice.LineItems[1],invoiceLine2), "objects should be different");
        }
        #endregion
    
    }
}
