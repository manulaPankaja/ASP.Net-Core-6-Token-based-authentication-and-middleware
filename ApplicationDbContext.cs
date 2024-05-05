using Microsoft.EntityFrameworkCore;
using Token_based_authentication_and_middleware.Helpers.Utils.GlobalAttributes;
using Token_based_authentication_and_middleware.Models;

namespace Token_based_authentication_and_middleware
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public ApplicationDbContext() 
        {
            //default constructor
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<StoryModel> Stories { get; set; }
        public DbSet<LoginDetailModel> loginDetails { get; set; }


        //Mysql configuration to use with default ApplicationDbContext constructor if not configured
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GlobalAttributes.mySQLConfiguration.ConnectionString, ServerVersion.AutoDetect(GlobalAttributes.mySQLConfiguration.ConnectionString));
            }
        }
    }
}
