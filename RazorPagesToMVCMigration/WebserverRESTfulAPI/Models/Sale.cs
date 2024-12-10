using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class Sale
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public DateTime DateSold { get; set; }

        // Navigation Property for Customer
        // [ForeignKey("CustomerID")]
        // public Customer Customer { get; set; }

        public Sale() { }

        public Sale(int ID, int customerID, DateTime dateSold)
        {
            this.ID = ID;
            CustomerID = customerID;
            DateSold = dateSold;
        }
    }
}
