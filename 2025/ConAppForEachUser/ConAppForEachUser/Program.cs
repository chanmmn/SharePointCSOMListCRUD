using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConAppForEachUser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string siteUrlSource = "https://someone.sharepoint.com/DevSite/";

            ClientContext clientContextSource = new ClientContext(siteUrlSource);
            string password = "password";
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            clientContextSource.Credentials = new SharePointOnlineCredentials("someone@someone.onmicrosoft.com", securePassword);
            GetUserPermission(clientContextSource, "TestList", "TestItem1");
        }
        public static void GetUserPermission(ClientContext clientContext, string listTitle, string itemTitle)
        {
            List list = clientContext.Web.Lists.GetByTitle(listTitle);

            var permissions = list.GetUserEffectivePermissions("i:0#.f|membership|someone@someone.onmicrosoft.com");
            clientContext.ExecuteQuery();

            foreach (PermissionKind permission in Enum.GetValues(typeof(PermissionKind)))
            {
                if (permissions.Value.Has(permission))
                {
                    Console.WriteLine($"User has permission: {permission}");
                }
            }
        }
    }
}
