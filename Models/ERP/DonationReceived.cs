using System.ComponentModel.DataAnnotations;

namespace EhsaasHub.Models.ERP
{
    // Stores information about donations received by the organization.
    public class DonationReceived
    {
        [Key]
        public int Id { get; set; }

        // Name of the donor. This can be null if donor wants to stay anonymous.
        public string? DonorName { get; set; }

        // Amount of donation received.
        [Required]
        public decimal Amount { get; set; }

        // Type of donation (e.g., Cash, Cheque, Online Transfer).
        [Required]
        public string DonationType { get; set; }

        // Purpose of the donation (e.g., Medical, Food, Education).
        [Required]
        public string Purpose { get; set; }

        // Date when donation was received.
        public DateTime DateReceived { get; set; } = DateTime.Now;

        // Optional document proof or receipt (URL or path to file).
        public string? DocumentUrl { get; set; }
    }
}
