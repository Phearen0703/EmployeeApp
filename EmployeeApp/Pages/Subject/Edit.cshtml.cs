using EmployeeApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeApp.Pages.Subject
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _appContext;

        public EditModel(AppDbContext appContext)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var subject = await _appContext.Subjects.FindAsync(Subject.id);

            if (subject == null)
            {
                return NotFound();
            }

            subject.sub_name = Subject.sub_name;
            subject.sort = Subject.sort;

            await _appContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}