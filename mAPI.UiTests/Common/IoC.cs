using mAPI.UiTests.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mAPI.UiTests.Common
{
    /// <summary>
    /// Inversion of Control is a design principle in which a software component is designed to receive its dependencies from an external source, rather than creating them itself. 
    /// This is in contrast to traditional software design, where a component is responsible for creating and managing its own dependencies
    /// </summary>
    public class IoC
    {
        private static volatile bool _isInitialized;
        private static readonly ServiceCollection Services;
        private static readonly IConfigurationRoot Configuration;
        private static readonly object ServiceProviderLock = new();
        private static ServiceProvider? _serviceProvider;


        static IoC()
        {
            Services = new ServiceCollection();
            Configuration = CreateConfiguration();
        }

        #region Setup
        public static IServiceScope CreateScope()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException($"The {nameof(IoC)} is not initialized.");
            }

            if (_serviceProvider == null)
            {
                lock (ServiceProviderLock)
                {
                    _serviceProvider ??= Services.BuildServiceProvider();
                }
            }

            return _serviceProvider.CreateScope();
        }

        private static IConfigurationRoot CreateConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
        #endregion


        #region Services registrations
        public static void Init()
        {
            if (!_isInitialized)
            {
                RegisterCredentials();
                InitInternal();

                _isInitialized = true;
            }
        }

        private static void RegisterCredentials()
        {
            Services.Configure<ApplicationUserCredentials>(Configuration.GetSection("ApplicationUserCredentials"));
        }

        private static void InitInternal()
        {
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetSection("DatabaseSettings:DevConnection").Value));
            Services.AddTransient<DbUnitOfWork>();
        }
        #endregion
    }
}