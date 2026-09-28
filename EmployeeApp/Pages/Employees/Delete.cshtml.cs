using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appContext;

        public DeleteModel(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        [BindProperty]
        public Employee Employee { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var employee = await _appContext.Employees
                .AsNoTracking()
                .Include(e => e.Gender)
                .Include(e => e.Location)
                .Include(e => e.Status)
                .FirstOrDefaultAsync(e => e.id == id);

            if (employee == null)
            {
                return NotFound();
            }

            Employee = employee;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var employee = await _appContext.Employees
                .Include(e => e.EmpSubs)
                .FirstOrDefaultAsync(e => e.id == Employee.id);

            if (employee == null)
            {
                return NotFound();
            }

            // remove child emp_sub rows first
            _appContext.EmpSubs.RemoveRange(employee.EmpSubs);
            _appContext.Employees.Remove(employee);

            await _appContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
