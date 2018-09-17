using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebUI {
	public class Program {
		public static void Main(string[] args) {
			var host = CreateWebHostBuilder(args).Build();
			Seed(host);
			host.Run();
		}

		private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

		private static void Seed(IWebHost host) {
			using (var scope = host.Services.CreateScope()) {
				var services = scope.ServiceProvider;
				try {
					SeedData.InitializeAsync(services).Wait();
				}
				catch (Exception ex) {
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "Error while seeding the database");
				}
			}
		}
	}
}