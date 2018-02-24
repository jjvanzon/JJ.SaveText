using System;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.Membership.OpenAuth;

namespace JJ.Demos.JavaScript.Account
{
	public partial class Register : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
		}

		protected void RegisterUser_CreatedUser(object sender, EventArgs e)
		{
			FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

			string continueUrl = RegisterUser.ContinueDestinationPageUrl;
			if (!OpenAuth.IsLocalUrl(continueUrl))
			{
				continueUrl = "~/";
			}
			Response.Redirect(continueUrl);
		}
	}
}