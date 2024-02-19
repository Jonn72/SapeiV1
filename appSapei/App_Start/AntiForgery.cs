using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace appSapei.App_Start
{

     [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]

     public class AntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
     {

          private readonly AcceptVerbsAttribute _acceptVerbs;
          public AntiForgeryAttribute():this(HttpVerbs.Post)
          {
          }

          public AntiForgeryAttribute(HttpVerbs verbs)
          {
               _acceptVerbs = new AcceptVerbsAttribute(verbs);
          }

          public void OnAuthorization(AuthorizationContext filterContext)
          {

               var request = filterContext.RequestContext.HttpContext.Request;
               var requestType = request.RequestType;
               if (!_acceptVerbs.Verbs.Contains(requestType))
               {
                    return;
               }
               var cookie = request.Cookies[AntiForgeryConfig.CookieName];
               var cookieToken = cookie != null ? cookie.Value : "";
               var name = "__RequestVerificationToken";
               var formToken = request.Form[name];
               if (string.IsNullOrWhiteSpace(formToken))
               {
                    formToken = request.Headers[name];
               }

               if (string.IsNullOrWhiteSpace(formToken))
               {
                    formToken = request.QueryString[name];
               }
               AntiForgery.Validate(cookieToken, formToken);
          }

     }
}