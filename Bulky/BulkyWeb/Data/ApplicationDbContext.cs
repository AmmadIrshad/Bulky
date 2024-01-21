using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    //DbContext is a root class of entityFrameworkCore 
    public class ApplicationDbContext : DbContext
    {
        //when we register DbContext we will be adding some configuration
        //whatever options we configure will be passed to the base class
        //register ApplicationDbContext in program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //with the help of DbContext(entityF) category table will be added
        //add-migration AddCategoryTableToDb
                      //update-database
        public DbSet<Category> Categories { get; set; }

        //for seeding data in database by default implemented in entity framework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1 ,Name = "Action" , DisplayOrder=1},
                new Category { Id = 2, Name = "scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
