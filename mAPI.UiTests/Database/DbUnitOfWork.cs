namespace mAPI.UiTests.Database
{
    public class DbUnitOfWork
    {
        public ApplicationDbContext DonationDB { get; }

        public DbUnitOfWork(ApplicationDbContext accountDbContext)
        {
            DonationDB = accountDbContext;
        }
    }
}