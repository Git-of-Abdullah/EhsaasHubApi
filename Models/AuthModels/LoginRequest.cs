namespace EhsaasHub.Models.AuthModels
{
    public class LoginRequest
    {
        public string Phone { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }   

        public string CNIC { get; set; }
    }
}
