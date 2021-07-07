using Roi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace Roi.Analysis.Api.Models
{
	public class WebPrincipal : IPrincipal
	{
		public Tester Id => id;
		private readonly Tester id;
		public WebPrincipal(string webId)
		{
			var IdPw = Encoding.UTF8.GetString((Convert.FromBase64String(webId)));
			int index = IdPw.IndexOf(':');
			var userId = IdPw.Substring(0, index);

			id = new RoiDb().authorized_ids.FirstOrDefault(w => w.userid == userId);
			identity = new WebIdentity(Id);
		}

		public string Role => "Handset";
		private readonly IIdentity identity;
		public IIdentity Identity => identity;

		public bool IsInRole(string role)
		{
			return role == Role;
		}
	}

	public class WebIdentity : IIdentity
	{
		public WebIdentity(Tester id)
		{
			name = id?.userid;
			isAuthenticated = !string.IsNullOrWhiteSpace(id?.company);
		}
		public string AuthenticationType => "Basic";

		private readonly bool isAuthenticated;
		public bool IsAuthenticated => isAuthenticated;

		private readonly string name;
		public string Name => name;
	}
}