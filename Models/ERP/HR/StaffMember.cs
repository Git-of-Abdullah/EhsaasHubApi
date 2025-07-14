using System.ComponentModel.DataAnnotations;

namespace EhsaasHub.Models.ERP.HR
{
    // Represents a staff member of the organization.
    public class StaffMember
    {
        [Key]
        public int Id { get; set; }

        // Full name of the team member.
        [Required]
        public string FullName { get; set; }
        
        // Email address (used for login/notifications).
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Contact number of the team member.
        [Required]
        public string PhoneNumber { get; set; }

        // Role: Admin, Monitor, Volunteer, FinanceOfficer, etc.
        [Required]
        public string Role { get; set; }

        // Status to determine if active or deactivated.
        public bool IsActive { get; set; } = true;

        // Date when the member joined.
        public DateTime JoinedOn { get; set; } = DateTime.Now;

        // Optional date of deactivation.
        public DateTime? DeactivatedOn { get; set; }
    }
}
