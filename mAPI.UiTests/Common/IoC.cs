using mAPI.UiTests.Common.Models.AppSettings;
using mAPI.UiTests.Database;
using mAPI.UiTests.Logger;
using mAPI.UiTests.UiFramework.Driver;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace mAPI.UiTests.Common
{
    /// <summary>
    /// Inversion of Control is a design principle in which a software component is designed to receive its dependencies from an external source, rather than creating them itself. 
    /// This is in contrast to traditional software design, where a component is responsible for creating and managing its own dependencies
    /// </summary>
    public class IoC
    {
        private static volatile bool _isInitialized;

        private static ServiceProvider? _serviceProvider;

        private static readonly object ServiceProviderLock = new();
        private static readonly ServiceCollection Services;
        private static readonly IConfigurationRoot Configuration;

        static IoC()
        {
            Services = new ServiceCollection();

            Configuration = CreateConfiguration();
            //SetAppSettings();
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
        #endregion


        #region Services registrations
        public static void Init()
        {
            if (!_isInitialized)
            {
                InitInternal();

                _isInitialized = true;
            }
        }

        private static void InitInternal()
        {
            Services.AddLogging(loggingBuilder => loggingBuilder.ClearProviders().AddAppLogging());

            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(AppSettings.Instance.DatabaseSettings.MAPIDB));

            Services.AddTransient<DbUnitOfWork>();

            Services.AddSingleton<WebDriverProvider>();
            Services.AddScoped<Browser>();
        }

        private static void SetAppSettings()
        {
            var builder = new ConfigurationBuilder();

            var appSettings = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                     .Build()
                                     .Get<AppSettings>();

            AppSettings.Configure(appSettings);
        }

        private static IConfigurationRoot CreateConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

#if DEVELOPMENT
            configurationBuilder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
#elif EXECUTION
            configurationBuilder.AddJsonFile("appsettings.Execution.json", optional: false, reloadOnChange: true);
#endif

            return configurationBuilder.Build();
        }

        #endregion
    }
}