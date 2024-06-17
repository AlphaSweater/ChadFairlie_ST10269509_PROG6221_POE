using Microsoft.Extensions.DependencyInjection;
using ChadFairlie_PROG6221_POE_GUI.Services;

namespace ChadFairlie_PROG6221_POE_GUI.Services
{
	public static class ServiceProviderFactory
	{
		private static ServiceProvider _serviceProvider;

		public static void ConfigureServices()
		{
			var services = new ServiceCollection();

			// Register RecipeService as a singleton
			services.AddSingleton<RecipeService>();

			_serviceProvider = services.BuildServiceProvider();
		}

		public static T GetService<T>()
		{
			return _serviceProvider.GetService<T>();
		}
	}
}