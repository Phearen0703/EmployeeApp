using EmployeeApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Pages.Subject
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _appContext;

        public IndexModel(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public List<EmployeeApp.Models.Subject> Subjects { get; set; } = new List<EmployeeApp.Models.Subject>();
        public async Task OnGetAsync()
        {
            Subjects = await _appContext.Subjects.ToListAsync();
        }




    }
}
