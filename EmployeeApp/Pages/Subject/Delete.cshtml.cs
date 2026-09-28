using EmployeeApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeApp.Pages.Subject
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appContext;

        public DeleteModel(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        [BindProperty]
        public EmployeeApp.Models.Subject Subject { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var subject = await _appContext.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            Subject = subject;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var subject = await _appContext.Subjects.FindAsync(Subject.id);

            if (subject == null)
            {
                return NotFound();
            }

            _appContext.Subjects.Remove(subject);

            await _appContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}