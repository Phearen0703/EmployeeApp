using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Pages.Employees
{
    public class CreateModel : EmployeeFormPageModel
    {
        public CreateModel(AppDbContext db) : base(db)
        {
        }

        public async Task OnGetAsync()
        {
            Employee.date_hired = DateTime.Today;
            await LoadLookupsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync();
                return Page();
            }

            // emp_sub rows are saved together with the employee in one SaveChanges
            Employee.EmpSubs = SelectedSubjectIds
                .Distinct()
                .Select(subId => new EmpSub { sub_id = subId })
                .ToList();

            Db.Employees.Add(Employee);
            await Db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
