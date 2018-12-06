using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Entities;

namespace PhotoGallery.Extensions
{
    public static class ExtensionMethods
    {
        public static void Inject(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.RegisterAssemblyPublicNonGenericClasses(
                    Assembly.GetExecutingAssembly())
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

        }
    }
}
