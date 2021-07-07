using Roi.Data;
using System.Linq;
using System.Security.Principal;

namespace Roi.Analysis.Api.Models
{
    public class HandsetPrincipal : IPrincipal
    {
        public machine Handset => handset;
        private readonly machine handset;
        public HandsetPrincipal(string handsetId)
        {
            handset = new RoiDb().machines.FirstOrDefault(h => handsetId == h.name && h.status == 0);
            identity = new HandsetIdentity(Handset);
        }

        public string Role => "Handset";
        private readonly IIdentity identity;
        public IIdentity Identity => identity;

        public bool IsInRole(string role)
        {
            return role == Role;
        }
    }

    public class HandsetIdentity : IIdentity
    {
        public HandsetIdentity(machine handset)
        {
            name = handset?.name;
            isAuthenticated = !string.IsNullOrWhiteSpace(handset?.company);
        }
        public string AuthenticationType => "Basic";

        private readonly bool isAuthenticated;
        public bool IsAuthenticated => isAuthenticated;

        private readonly string name;
        public string Name => name;
    }
}