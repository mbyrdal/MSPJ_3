using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Models
{
    public class Sale
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Date Sold is required.")]
        [DataType(DataType.Date)]
        public DateTime DateSold { get; set; }

        // Navigation Property for Customer
        // Uncomment if needed for Entity Framework relationship
        // [ForeignKey("CustomerID")]
        // public Customer? Customer { get; set; }

        public Sale() { }

        public Sale(int id, int customerID, DateTime dateSold)
        {
            ID = id;
            CustomerID = customerID;
            DateSold = dateSold;
        }
    }
}
