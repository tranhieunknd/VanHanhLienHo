using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
 
public class LocalizedViewEngine : RazorViewEngine
{
	public override ViewEngineResult FindPartialView (ControllerContext controllerContext, string partialViewName, bool useCache)
	{
		List<string> searched = new List<string>();
 
		if (!string.IsNullOrEmpty(partialViewName))
		{
			ViewEngineResult result;
 
			result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.Name), useCache);
 
			if (result.View != null)
			{
				return result;
			}
 
			searched.AddRange(result.SearchedLocations);
 
			result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName), useCache);
 
			if (result.View != null)
			{
				return result;
			}
 
			searched.AddRange(result.SearchedLocations);
		}
 
		return new ViewEngineResult(searched.Distinct().ToList());
	}
 
	public override ViewEngineResult FindView (ControllerContext controllerContext, string viewName, string masterName, bool useCache)
	{
		List<string> searched = new List<string>();
 
		if (!string.IsNullOrEmpty(viewName))
		{
			ViewEngineResult result;
 
			result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.Name), masterName, useCache);
 
			if (result.View != null)
			{
				return result;
			}
 
			searched.AddRange(result.SearchedLocations);
 
			result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName), masterName, useCache);
 
			if (result.View != null)
			{
				return result;
			}
 
			searched.AddRange(result.SearchedLocations);
		}
 
		return new ViewEngineResult(searched.Distinct().ToList());
	}
}