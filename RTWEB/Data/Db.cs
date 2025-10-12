using Microsoft.EntityFrameworkCore;
using RTWEB.Models;

namespace RTWEB.Data
{
    public class Db:DbContext
    {
        //public static string ConnectionString = "Server=localhost;Database=RTSYSTEM;User Id=sa;Password=Test_123;Encrypt=False";

        public static string ConnectionString = "Server=103.125.252.243;Database=demo;User Id=oct_demo;Password=hbswiplv4czmyjfqdexn;Encrypt=False";
        //public static string ConnectionString = "Server=103.125.252.243;Database=support;User Id=oct_support;Password=kfmrzeoxacldiwth6gjv;Encrypt=False";

        public Db()
        {

        }

        public Db(DbContextOptions<Db> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Domain> Domains { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<UpdateDetail> UpdateDetails {  get; set; }
        public DbSet<CustomerIssue> CustomerIssues { get; set; }
        public DbSet<OurCustomer> OurCustomers { get; set; }
        public DbSet<SolvedIssue> SolvedIssues { get; set; }
        public DbSet<SolvedDetail> SolvedDetails { get; set; }
        public DbSet<ParentProject> ParentProjects {  get; set; }
    }
}
