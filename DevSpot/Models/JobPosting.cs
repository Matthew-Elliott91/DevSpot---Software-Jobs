using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevSpot.Models
{
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }

        public DateTime DatePosted { get; set; }
        public bool IsApproved { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        public string SalaryString { get; set; }
        public bool Remote { get; set; }
        public bool Hybrid { get; set; }
        public string JobUrl { get; set; }
        public string CompanyDomain { get; set; }
        public string PostalCode { get; set; }
        public string Url { get; set; }
        public string ShortDescription
        {
            get
            {
                if (Description.Length <= 100)
                    return Description;
                return Description.Substring(0, 100) + "...";
            }
        }
        public Company CompanyObject { get; set; }
        
    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Logo { get; set; }
        public bool? IsRecruitingAgency { get; set; }
    }
}
