using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Pages.Employees
{
    // Loads the dropdown / checkbox data used by Index, Create and Edit
    public abstract class EmployeeLookupPageModel : PageModel
    {
        protected readonly AppDbContext Db;

        protected EmployeeLookupPageModel(AppDbContext db)
        {
            Db = db;
        }

        public SelectList Genders { get; set; } = default!;
        public SelectList Locations { get; set; } = default!;
        public SelectList Statuses { get; set; } = default!;
        public SelectList SubjectItems { get; set; } = default!;

        // Fully qualified: the namespace EmployeeApp.Pages.Subject would otherwise hide the Subject class
        public List<EmployeeApp.Models.Subject> AllSubjects { get; set; } = new();

        protected async Task LoadLookupsAsync()
        {
            var genders = await Db.Genders.AsNoTracking().OrderBy(x => x.id).ToListAsync();
            var locations = await Db.Locations.AsNoTracking().OrderBy(x => x.id).ToListAsync();
            var statuses = await Db.Statuses.AsNoTracking().OrderBy(x => x.id).ToListAsync();
            AllSubjects = await Db.Subjects.AsNoTracking().OrderBy(x => x.sort).ToListAsync();

            Genders = new SelectList(genders, "id", "gender");
            Locations = new SelectList(locations, "id", "loc_name");
            Statuses = new SelectList(statuses, "id", "status_name");
            SubjectItems = new SelectList(AllSubjects, "id", "sub_name");
        }
    }

    // Shared by Create and Edit
    public abstract class EmployeeFormPageModel : EmployeeLookupPageModel
    {
        protected EmployeeFormPageModel(AppDbContext db) : base(db)
        {
        }

        [BindProperty]
        public Employee Employee { get; set; } = new();

        [BindProperty]
        public List<int> SelectedSubjectIds { get; set; } = new();
    }
}
