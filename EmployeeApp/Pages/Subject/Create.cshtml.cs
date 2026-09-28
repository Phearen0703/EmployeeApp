using EmployeeApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeApp.Pages.Subject
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _appContext;

        public CreateModel(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        [BindProperty]
        public EmployeeApp.Models.Subject Subject { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Subject.sort = (_appContext.Subjects.Max(s => (int?)s.sort) ?? 0) + 1;

                _appContext.Subjects.Add(Subject);

                await _appContext.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    ex.InnerException?.Message ?? ex.Message);

                return Page();
            }
        }
    }
}