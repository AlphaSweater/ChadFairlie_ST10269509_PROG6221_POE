using Microsoft.Extensions.DependencyInjection;
using ChadFairlie_PROG6221_POE_GUI.Services;

namespace ChadFairlie_PROG6221_POE_GUI.Services
{
	// Provides a centralized mechanism to configure and retrieve service instances.
	public static class ServiceProviderFactory
	{
		// Holds the instance of ServiceProvider that manages service instances.
		private static ServiceProvider _serviceProvider;

		// Configures services and their lifetimes within the application.
		public static void ConfigureServices()
		{
			var services = new ServiceCollection();

			// Register RecipeService as a singleton, ensuring a single instance throughout the application's lifetime.
			services.AddSingleton<RecipeService>();

			// Build the ServiceProvider from the service collection.
			_serviceProvider = services.BuildServiceProvider();
		}

		// Retrieves a service instance of the specified type.
		public static T GetService<T>()
		{
			// Uses the ServiceProvider to get the requested service instance.
			return _serviceProvider.GetService<T>();
		}
	}
}