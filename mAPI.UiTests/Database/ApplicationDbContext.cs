using mAPI.UiTests.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace mAPI.UiTests.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<DCandidate> DCandidates { get; set; }


        #region Overrides
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new DCandidate.DCandidateEntityConfiguration());
        }
        #endregion
    }
}