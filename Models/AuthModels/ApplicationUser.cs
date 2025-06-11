using Microsoft.AspNetCore.Identity;

namespace EhsaasHub.Models.AuthModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } 
        public string CNIC { get; set; }
        public string Location { get; set; }
        public string Role { get; set; }  // "donor", "beneficiary", or "organization"
        public string LanguagePreference { get; set; } // "en" or "ur"
        public string ProfileImageUrl { get; set; }


        public OrganizationProfile OrganizationProfile { get; set; }

    }
}
;