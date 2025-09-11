using Microsoft.EntityFrameworkCore;

namespace RTWEB.Data
{
    public class Db:DbContext
    {
        public static string ConnectionString = "Server=localhost;Database=hmsystem;User Id=sa;Password=Test_123;Encrypt=False";

        //public static string ConnectionString = "Server=103.125.252.243;Database=ebikeuat;User Id=oct_ebikeuat;Password=foabwl7pdigzystxecvq;Encrypt=False";

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


    }
}
