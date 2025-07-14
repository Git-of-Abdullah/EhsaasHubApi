using EhsaasHub.Models.AuthModels;
using EhsaasHub.Models.ERP;
using EhsaasHub.Models.ERP.HR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EhsaasHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<OrganizationProfile> OrganizationProfiles { get; set; }

        // Add more DbSets for other entities as needed


        // ERP Modules - Donation
        public DbSet<DonationReceived> DonationsReceived { get; set; }
        public DbSet<DonationGiven> DonationsGiven { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<SalaryRecord> SalaryRecords { get; set; }


    }
}

