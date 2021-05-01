using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WSPrestamo.Utilidades
{
    public static class RequestInfo
    {
        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var urlHelper = new UrlHelper(request.RequestContext);
            var baseUrl = $"{request.Url.Scheme}://{request.Url.Authority}{urlHelper.Content("~")}";
            return baseUrl;
        }
    }
}