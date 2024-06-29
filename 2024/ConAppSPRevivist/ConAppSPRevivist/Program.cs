
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;

namespace ConAppSPRevivist
{
    internal class Program
    {
        static void Main()
        {
            GetSharePointItem();

        }

        private static void GetSharePointItem()
        {
            //youe url must be correct
            string siteUrlSource = "https://someone.sharepoint.com/DevSite/";

            //this is where you use your declared url
            ClientContext clientContextSource = new ClientContext(siteUrlSource);

            //For SharePoint Online this is the important section
            string password = "password";

            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            clientContextSource.Credentials = new SharePointOnlineCredentials("someone@someone.onmicrosoft.com", securePassword);

            //the List name goes here
            SP.List oList = clientContextSource.Web.Lists.GetByTitle("testList");


            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View><RowLimit> 100</RowLimit></View>";

            ListItemCollection collListItem = oList.GetItems(camlQuery);
            clientContextSource.Load(collListItem,
                 items => items.Include(
                     item => item.Id));
            //item => item.Id,
            //item => item.DisplayName,
            //item => item.HasUniqueRoleAssignments));

            //if your url is wrong or list name is wrong then you will normal get exception here

            clientContextSource.ExecuteQuery();
            foreach (ListItem oListItem in collListItem)
            {
                //    Console.WriteLine("ID: { 0} \nDisplay name: { 1} \nUnique role assignments: { 2}",
                //oListItem.Id, oListItem.DisplayName, oListItem.HasUniqueRoleAssignments);
                Console.WriteLine("ID: {0}", oListItem.Id);
            }
        }
    }
}
