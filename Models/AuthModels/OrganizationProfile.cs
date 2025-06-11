using System.ComponentModel.DataAnnotations.Schema;

namespace EhsaasHub.Models.AuthModels
{
    public class OrganizationProfile
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } // Foreign Key to ApplicationUser

        public ApplicationUser User { get; set; }
        public string OrganizationName { get; set; }
        public string ServiceDetails { get; set; }
        public string RegistrationProofUrl { get; set; }
    }
}
