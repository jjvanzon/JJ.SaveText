using System;
using System.Web;
using System.Web.UI;

namespace JJ.Demos.JavaScript.Account
{
	public partial class Login : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterHyperLink.NavigateUrl = "Register";
			OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

			var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
			if (!string.IsNullOrEmpty(returnUrl))
			{
				RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
			}
		}
	}
}