namespace XeroTechnicalTest
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + InvoiceLineId.GetHashCode();
                hash = hash * 23 + Description.GetHashCode();
                hash = hash * 23 + Quantity.GetHashCode();
                hash = hash * 23 + Cost.GetHashCode();
                return hash;
            }
        } 
    }
}
