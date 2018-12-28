using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Extensions
{
    public static class Extensions
    {
        public static string RemoveWhiteSpace(this string self)
        {
            return new string(self.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}
