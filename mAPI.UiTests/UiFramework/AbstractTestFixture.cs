using mAPI.UiTests.Common;
using Microsoft.Extensions.DependencyInjection;

namespace mAPI.UiTests.UiFramework
{
    public abstract partial class AbstractTestFixture : IDisposable
    {
        private readonly IServiceScope _serviceScope;


        protected AbstractTestFixture()
        {
            _serviceScope = IoC.CreateScope();
        }


        protected object Resolve(Type type) => _serviceScope.ServiceProvider.GetRequiredService(type);

        protected T Resolve<T>() where T : notnull => _serviceScope.ServiceProvider.GetRequiredService<T>();

        public void Dispose()
        {
            _serviceScope.Dispose();
            GC.SuppressFinalize(this);
            OnDispose();
        }

        protected virtual void OnDispose() { }
    }
}