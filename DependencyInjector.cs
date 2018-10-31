using Microsoft.Extensions.DependencyInjection;
using PhotoGallery.Controllers;
using PhotoGallery.Services;
using PhotoGallery.Services.Interfaces;
using WebApi.Services;

namespace PhotoGallery
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
        }
    }
}