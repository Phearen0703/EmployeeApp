using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Gender> Genders { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<EmpSub> EmpSubs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Subject>().HasIndex(s => s.id).IsUnique();

            modelBuilder.Entity<Gender>().ToTable("gender");
            modelBuilder.Entity<Location>().ToTable("location");
            modelBuilder.Entity<Status>().ToTable("status");

            modelBuilder.Entity<Employee>(e =>
            {
                e.ToTable("employee");

                e.HasOne(x => x.Gender).WithMany()
                    .HasForeignKey(x => x.gender_id);

                e.HasOne(x => x.Location).WithMany()
                    .HasForeignKey(x => x.location_id);

                e.HasOne(x => x.Status).WithMany()
                    .HasForeignKey(x => x.status_id);
            });

            modelBuilder.Entity<EmpSub>(e =>
            {
                e.ToTable("emp_sub");

                e.HasOne(x => x.Employee).WithMany(x => x.EmpSubs)
                    .HasForeignKey(x => x.emp_id);

                e.HasOne(x => x.Subject).WithMany()
                    .HasForeignKey(x => x.sub_id);
            });
        }
    }
}
