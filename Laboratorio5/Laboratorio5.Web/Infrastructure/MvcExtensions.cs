using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

namespace Laboratorio5.Web.Infrastructure
{
    public static class MvcExtensions
    {
        public static CultureInfo CurrentCulture(this RazorPage page)
        {
            var rqf = page.Context.Features.Get<IRequestCultureFeature>();
            return rqf.RequestCulture.Culture;
        }
    }
}
