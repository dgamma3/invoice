using System;
using System.Collections.Generic;
using System.Linq;

namespace XeroTechnicalTest
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<InvoiceLine> LineItems { get; set; } = new List<InvoiceLine>();
        public string Message { get; private set; }

        public Result AddInvoiceLine(InvoiceLine proposedInvoiceLine)
        {
            var result = ValidInput(proposedInvoiceLine);

            if (result.Success)
            {
                LineItems.Add(proposedInvoiceLine);
            }

            return result;
            

        }

        public Result RemoveInvoiceLine(int invoiceLineId)
        {
            var result = ValidInput2(invoiceLineId);
                      
            if (result.Success)
            {
               var lineItemToRemove = LineItems.Single(i => i.InvoiceLineId == invoiceLineId);
                LineItems.Remove(lineItemToRemove);

            }

            return result;

        }
       
        /// <summary>
        /// GetTotal should return the sum of (Cost * Quantity) for each line item
        /// </summary>
        public decimal GetTotal()
        {
            return (decimal)LineItems.Sum(x => x.Cost * x.Quantity);
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="invoiceToMerge">Invoice to merge from</param>
        public void MergeInvoices(Invoice invoiceToMerge)
        {
            var copyOfInvoiceToMerge= invoiceToMerge.Clone();
            LineItems.AddRange(copyOfInvoiceToMerge.LineItems);
        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice Clone()
        {
            new List<InvoiceLine>(this.LineItems.Count);

            var copyOfLineItems = LineItems.ConvertAll(i =>
            {
                return new InvoiceLine()
                {
                    Cost = i.Cost,
                    Description = i.Description,
                    InvoiceLineId = i.InvoiceLineId,
                    Quantity = i.Quantity
                };
            });

            return new Invoice()
            {
                InvoiceNumber = this.InvoiceNumber,
                InvoiceDate = this.InvoiceDate,
                LineItems = copyOfLineItems
            };
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [DD/MM/YYYY], LineItemCount: [Number of items in LineItems] 
        /// </summary>
        /// 
        public override string ToString()
        {
            return $"Invoice Number: {InvoiceNumber}, InvoiceDate: {InvoiceDate.ToString("dd/MM/yyy")}, LineItemCount: {LineItems.Count}";
        }

        private Result ValidInput2(int invoiceLineId)
        {
            if (this.LineItems.All(x => x.InvoiceLineId != invoiceLineId))
            {
                return new Result
                {
                    Success = false,
                    Message = $"InvoiceLineId: {invoiceLineId}, doesn't exist,"
                };
            }

            return new Result
            {
                Success = true
            };
        }
        private Result ValidInput(InvoiceLine proposedInvoiceLine)
        {
            if (proposedInvoiceLine == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "cannot add a null invoice item"
                };
            }
            else if (proposedInvoiceLine.Cost <= 0)
            {
                return new Result
                {
                    Success = false,
                    Message = "cannot add an invoice item with an invalid cost, , must be > $0 "
                };
            }
            else if (proposedInvoiceLine.Quantity <= 0)
            {
                return new Result
                {
                    Success = false,
                    Message = "cannot add an invoice item with an invalid quantity, must be > 0"
                };
            }
            else if (string.IsNullOrEmpty(proposedInvoiceLine.Description))
            {
                return new Result
                {
                    Success = false,
                    Message = "cannot add an invoice item with no description"
                };
            }
            else if (proposedInvoiceLine.InvoiceLineId <= 0)
            {
                return new Result
                {
                    Success = false,
                    Message = "cannot add an invoice item with an InvoiceLineId, must be > 0"
                };
            }

            else if (this.LineItems.Any(i => i.InvoiceLineId== proposedInvoiceLine.InvoiceLineId))
            {
                return new Result
                {
                    Success = false,
                    Message = $"InvoiceLineId must be unique."
                };
            }

            return new Result
            {
                Success = true
            };
        }
        
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + this.InvoiceDate.GetHashCode();
                hash = hash * 23 + this.InvoiceNumber.GetHashCode();
                this.LineItems.ForEach(x =>   hash = hash * 23 + x.GetHashCode() );
                return hash;
            }
        }

    }
}
