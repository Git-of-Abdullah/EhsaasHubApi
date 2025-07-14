using System.ComponentModel.DataAnnotations;

namespace EhsaasHub.Models.ERP
{
    // Stores information about donations given by the organization to beneficiaries or causes.
    public class DonationGiven
    {
        [Key]
        public int Id { get; set; }
        
        // Name of the recipient (individual or entity).
        [Required]
        public string RecipientName { get; set; }

        // Donation type: Item or Money.
        [Required]
        public string DonationType { get; set; }

        // Amount or quantity donated.
        [Required]
        public decimal Value { get; set; }

        // Purpose (e.g., Medical Aid, Emergency, Monthly Support).
        [Required]
        public string Purpose { get; set; }
        
        // Date of donation.
        public DateTime DateGiven { get; set; } = DateTime.Now;

        // Optional document (receipt, photo, or file proof).
        public string? DocumentUrl { get; set; }
    }
}
