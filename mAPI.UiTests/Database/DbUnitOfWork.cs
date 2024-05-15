namespace mAPI.UiTests.Database
{
    public class DbUnitOfWork(ApplicationDbContext accountDbContext)
    {
        public ApplicationDbContext DonationDB { get; } = accountDbContext;
    }
}