using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EmployeeApp.Models
{
    // Join table: one employee can have many subjects
    public class EmpSub
    {
        public int id { get; set; }
        public int sub_id { get; set; }
        public int emp_id { get; set; }

        [ValidateNever]
        public Subject? Subject { get; set; }

        [ValidateNever]
        public Employee? Employee { get; set; }
    }
}
