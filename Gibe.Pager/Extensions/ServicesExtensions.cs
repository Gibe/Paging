using Gibe.Pager.Interfaces;
using Gibe.Pager.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gibe.Pager.Extensions
{
	public static class NetCoreServicesExtensions
	{
		public static void AddGibePager<T>(this IServiceCollection services) where T : class, ICurrentUrlService
		{
			services.AddTransient<IPagerService, PagerService>();
			services.AddTransient<ICurrentUrlService, T>();
		}
	}
}
