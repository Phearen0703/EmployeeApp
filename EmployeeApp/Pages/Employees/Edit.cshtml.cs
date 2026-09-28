using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Pages.Employees
{
    public class EditModel : EmployeeFormPageModel
    {
        public EditModel(AppDbContext db) : base(db)
        {
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var employee = await Db.Employees
                .AsNoTracking()
                .Include(e => e.EmpSubs)
                .FirstOrDefaultAsync(e => e.id == id);

            if (employee == null)
            {
                return NotFound();
            }

            Employee = employee;
            SelectedSubjectIds = employee.EmpSubs.Select(x => x.sub_id).ToList();

            await LoadLookupsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync();
                return Page();
            }

            var employee = await Db.Employees
                .Include(e => e.EmpSubs)
                .FirstOrDefaultAsync(e => e.id == Employee.id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.first_name = Employee.first_name;
            employee.last_name = Employee.last_name;
            employee.gender_id = Employee.gender_id;
            employee.location_id = Employee.location_id;
            employee.salary = Employee.salary;
            employee.date_hired = Employee.date_hired;
            employee.status_id = Employee.status_id;

            // Sync emp_sub rows with the ticked subjects
            var selected = SelectedSubjectIds.Distinct().ToList();

            var toRemove = employee.EmpSubs
                .Where(x => !selected.Contains(x.sub_id))
                .ToList();

            Db.EmpSubs.RemoveRange(toRemove);

            foreach (var subId in selected.Where(s => !employee.EmpSubs.Any(x => x.sub_id == s)))
            {
                employee.EmpSubs.Add(new EmpSub { sub_id = subId });
            }

            await Db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
