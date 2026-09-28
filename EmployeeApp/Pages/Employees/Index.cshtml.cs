using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Pages.Employees
{
    public class IndexModel : EmployeeLookupPageModel
    {
        public IndexModel(AppDbContext db) : base(db)
        {
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? GenderId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? LocationId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? StatusId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SubjectId { get; set; }

        public IList<Employee> Employees { get; set; } = new List<Employee>();

        public async Task OnGetAsync()
        {
            await LoadLookupsAsync();

            // Joins: employee + gender + location + status + emp_sub + Subject
            IQueryable<Employee> query = Db.Employees
                .AsNoTracking()
                .Include(e => e.Gender)
                .Include(e => e.Location)
                .Include(e => e.Status)
                .Include(e => e.EmpSubs).ThenInclude(es => es.Subject);

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                var s = SearchString.Trim();
                var isNumber = int.TryParse(s, out var number);

                query = query.Where(e =>
                    (e.first_name != null && e.first_name.Contains(s)) ||
                    (e.last_name != null && e.last_name.Contains(s)) ||
                    ((e.first_name + " " + e.last_name).Contains(s)) ||
                    (e.Gender != null && e.Gender.gender != null && e.Gender.gender.Contains(s)) ||
                    (e.Location != null && e.Location.loc_name != null && e.Location.loc_name.Contains(s)) ||
                    (e.Status != null && e.Status.status_name != null && e.Status.status_name.Contains(s)) ||
                    e.EmpSubs.Any(es => es.Subject != null && es.Subject.sub_name.Contains(s)) ||
                    (isNumber && e.id == number));
            }

            if (GenderId.HasValue)
                query = query.Where(e => e.gender_id == GenderId);

            if (LocationId.HasValue)
                query = query.Where(e => e.location_id == LocationId);

            if (StatusId.HasValue)
                query = query.Where(e => e.status_id == StatusId);

            if (SubjectId.HasValue)
                query = query.Where(e => e.EmpSubs.Any(es => es.sub_id == SubjectId));

            Employees = await query.OrderBy(e => e.id).ToListAsync();
        }
    }
}
