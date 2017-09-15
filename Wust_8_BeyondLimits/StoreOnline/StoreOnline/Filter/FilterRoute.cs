using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StoreOnline.Filter
{
    public class FilterRoute : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            
            //throw new NotImplementedException();
            if(requestContext.HttpContext.Session["User"]==null)
            {
                requestContext.RouteData.Values["Controller"] = "Home";
                requestContext.RouteData.Values["action"] = "Index";
            }
            else
            {
                if(requestContext.HttpContext.Session["Entry"].ToString()!=
                    Enum.GetName(typeof(Admit),Admit.Admin).ToString()&&
                    requestContext.RouteData.Values["Controller"].ToString()=="Admin")
                {
                    requestContext.RouteData.Values["Controller"] = "Home";
                    requestContext.RouteData.Values["action"] = "Index";
                }
            }
            return new MvcHandler(requestContext);
        }
    }
}