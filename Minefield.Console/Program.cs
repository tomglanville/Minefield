namespace Minefield
{
    using Microsoft.Extensions.DependencyInjection;
    using Minefield.Core;
    using Minefield.Core.Services;

    /// <summary>
    /// Main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            // Start the app.
            serviceProvider.GetService<MinefieldApp>().Run();
        }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <returns>The services.</returns>
        private static IServiceCollection ConfigureServices()
        {
            // Setup dependency injection.
            IServiceCollection services = new ServiceCollection();

            // Register dependencies.
            services.AddTransient<IGridService, GridService>();
            services.AddTransient<MinefieldViewModel>();
            services.AddTransient<MinefieldApp>();

            return services;
        }
    }
}