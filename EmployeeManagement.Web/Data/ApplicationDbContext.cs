using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(f => f.ApplicationStatus)
                .WithMany()
                .HasForeignKey(f => f.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(f => f.Duration)
                .WithMany()
                .HasForeignKey(f => f.DurationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeSalary>()
                .Property(e => e.BasicSalary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EmployeeSalary>()
                .Property(e => e.Bonus)
                .HasColumnType("decimal(18,2)");
        }


        public DbSet <Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentRole> DepartmentRoles { get; set; }
        public DbSet<SystemCode> SystemCodes { get; set; }
        public DbSet<SystemCodeDetail> SystemCodeDetails { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
       public DbSet<LeaveApplication> LeaveApplications { get; set; }
       public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
