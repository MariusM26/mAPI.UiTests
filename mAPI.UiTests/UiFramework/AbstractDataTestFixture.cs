using mAPI.UiTests.Database;

namespace mAPI.UiTests.UiFramework
{
    public abstract partial class AbstractDataTestFixture : AbstractTestFixture
    {
        protected DbUnitOfWork Db { get; }

        protected AbstractDataTestFixture()
        {
            Db = Resolve<DbUnitOfWork>();
        }
    }
}