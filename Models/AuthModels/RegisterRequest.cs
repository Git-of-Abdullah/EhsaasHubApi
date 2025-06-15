namespace EhsaasHub.Models.AuthModels
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CNIC { get; set; }
        public string Location { get; set; }
        public string Role { get; set; }  // "donor" or "beneficiary"
        public string LanguagePreference { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Password { get; set; }
        public string Otp { get; set; }


        public OrganizationDto Organization { get; set; }

    }
}
