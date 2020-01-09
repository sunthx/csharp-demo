using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ef_demo.DbContexts
{
    public class CompanyDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Department> Departments { set; get; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=localhost;user id=root;password=SUNfei19901988;database=efdemo");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(p => p.DisplayName)
                .HasComputedColumnSql("[LastName] + ',' + [FirstName]");
        }
    }

    public class Basic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset UpdateTime { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { set; get; }
    }

    public class Company : Basic
    {
        public string CompanyName { get; set; }

        public IList<Department> Departments { get; set; }
    }

    public class Department : Basic
    {
        public string DepartmentName { set; get; }
        public Company Company { set; get; }
        public IList<Employee> Employees { set; get; }
    }

    public class Employee : Basic
    {
        public string EmployeeId { set; get; }

        public string DisplayName{ set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public Department Department { set; get; }
    }
}