using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PhotoGallery.Helpers
{
    public class Environment : IEnvironment
    {
        public IOptions<AppSettings> AppSettings { get; }

        public Environment(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings;
        }

        public string Url => AppSettings?.Value?.Url?.AbsoluteUri;
    }

    public interface IEnvironment
    {
        string Url { get; }
    }
}
