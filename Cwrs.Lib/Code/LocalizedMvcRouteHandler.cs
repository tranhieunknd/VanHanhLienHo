using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using MvcGlobalisationSupport;
 
public class LocalizedMvcRouteHandler : MvcRouteHandler
{
	protected override IHttpHandler GetHttpHandler (RequestContext requestContext)
	{
        string strCulture=requestContext.RouteData.Values["culture"].ToString();
        if (CultureFormatChecker.FormattedAsCulture(strCulture))
        {
            CultureInfo ci = new CultureInfo(strCulture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }
		return base.GetHttpHandler(requestContext);
	}
}