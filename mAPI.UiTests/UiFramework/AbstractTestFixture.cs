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


        protected T Resolve<T>() => _serviceScope.ServiceProvider.GetRequiredService<T>();

        public void Dispose()
        {
            _serviceScope.Dispose();
            OnDispose();
        }

        protected virtual void OnDispose() { }
    }
}