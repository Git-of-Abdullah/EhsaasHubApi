using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EhsaasHub.Models.ERP.HR
{
    // Represents a monthly salary record for a staff member.
    public class SalaryRecord
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to the staff member.
        public int StaffMemberId { get; set; }

        // Month and year of the salary (e.g., May 2025).
        [Required]
        public DateTime SalaryMonth { get; set; }

        // Salary amount for this month.
        [Required]
        public decimal Amount { get; set; }

        // Whether this salary has been paid.
        public bool IsPaid { get; set; } = false;

        // Date the salary was paid (nullable).
        public DateTime? PaidOn { get; set; }

        // Optional remarks (e.g., advance, deduction).
        public string? Notes { get; set; }

        // Navigation property (optional, useful for joins)
        [ForeignKey("StaffMemberId")]
        public StaffMember? StaffMember { get; set; }
    }
}
