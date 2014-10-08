using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CampusNext.DataAccess
{
    public class CampusNextContext : DbContext
    {
        public CampusNextContext():base("CampusNextContext")
        {
            
        }

        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<Profile> Profiles { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string FacebookName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }

    public class Textbook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string Isbn { get; set; }
        public string Course { get; set; }
        public double Price { get; set; }
        public string PriceCode { get; set; }
        public string Campus { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int LikeCount { get; set; }
        public string ContactInfo { get; set; }
    }
}