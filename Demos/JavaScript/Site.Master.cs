using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace JJ.Demos.JavaScript
{
    public partial class SiteMaster : MasterPage
    {
        private const string ANTI_XSRF_TOKEN_KEY = "__AntiXsrfToken";
        private const string ANTI_XSRF_USER_NAME_KEY = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            HttpCookie requestCookie = Request.Cookies[ANTI_XSRF_TOKEN_KEY];
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out Guid _))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(ANTI_XSRF_TOKEN_KEY)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[ANTI_XSRF_TOKEN_KEY] = Page.ViewStateUserKey;
                ViewState[ANTI_XSRF_USER_NAME_KEY] = Context.User.Identity.Name ?? string.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[ANTI_XSRF_TOKEN_KEY] != _antiXsrfTokenValue
                    || (string)ViewState[ANTI_XSRF_USER_NAME_KEY] != (Context.User.Identity.Name ?? string.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}