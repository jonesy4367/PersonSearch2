using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Models;

namespace DataAccess
{
    public class PeopleContext : DbContext
    {
        public PeopleContext() : base("PeopleContext")
        {
        }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<State> States { get; set; }

        public virtual DbSet<Interest> Interests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}