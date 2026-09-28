using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EmployeeApp.Models
{
    public class Employee
    {
        public int id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string? first_name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string? last_name { get; set; }

        public int? gender_id { get; set; }

        public int? location_id { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive")]
        [DataType(DataType.Currency)]
        public decimal? salary { get; set; }

        [Required(ErrorMessage = "Date hired is required")]
        [DataType(DataType.Date)]
        public DateTime? date_hired { get; set; }

        public int? status_id { get; set; }

        // Navigation properties (joined tables)
        [ValidateNever]
        public Gender? Gender { get; set; }

        [ValidateNever]
        public Location? Location { get; set; }

        [ValidateNever]
        public Status? Status { get; set; }

        [ValidateNever]
        public ICollection<EmpSub> EmpSubs { get; set; } = new List<EmpSub>();
    }
}
