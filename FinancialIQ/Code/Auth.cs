using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FinancialIQ.Code
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity, int userId)
        {
            Identity = identity;
            UserId = userId;
        }

        public CustomPrincipal()
        {
            
        }
        public int UserId { get; set; }

        #region IPrincipal Members

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true; // everyone's a winner
        }

        #endregion
    }

    public static class SimpleSessionPersister
    {
        public static void Save(string email, bool remember, int userId)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, email,
                DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                remember, userId.ToString());
            string hashedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name)
        {
            Name = name;
        }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.Name); }
        }

        public string Name { get; private set; }

        #endregion
    }

}