using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;


namespace ConAppSPAdd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //youe url must be correct
            string siteUrlSource = "https://someone.sharepoint.com/DevSite/";
            //this is where you use your declared url
            ClientContext clientContextSource = new ClientContext(siteUrlSource);
            //For SharePoint Online this is the important section
            string password = "password";
            SecureString securePassword = new SecureString();
            foreach (char c in password)            {
                securePassword.AppendChar(c);
            }
            clientContextSource.Credentials = new SharePointOnlineCredentials("someone@someone.onmicrosoft.com", securePassword);
            AddItemToList(clientContextSource, "TestList", "Item 4");

            //ClientContext clientContext = new ClientContext(siteUrlSource); // 403 error line
            //AddItemToList(clientContext, "TestList", "Item 4");// 403 error line

            // Section 1
            //SP.List oList = clientContext.Web.Lists.GetByTitle("TestList");

            //ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            //ListItem oListItem = oList.AddItem(itemCreateInfo);
            ////oListItem["CategoryId"] = "2";
            //oListItem["Title"] = "Item 2";
            ////oListItem["CategoryName"] = "Balo world!";

            //oListItem.Update();

            //clientContext.ExecuteQuery();

            // Section 2

            //// The SharePoint web at the URL.
            //Web web = clientContext.Web;

            //web.Title = "New Title";
            ////web.Description = "New Description";

            //// Note that the web.Update() doesn't trigger a request to the server.
            //// Requests are only sent to the server from the client library when
            //// the ExecuteQuery() method is called.
            //web.Update();

            //// Execute the query to server.
            //clientContext.ExecuteQuery();
        }

        public static void AddItemToList(ClientContext clientContext, string listTitle, string itemTitle)
        {
            List list = clientContext.Web.Lists.GetByTitle(listTitle);
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem listItem = list.AddItem(itemCreateInfo);
            listItem["Title"] = itemTitle;
            listItem.Update();
            clientContext.ExecuteQuery();
        }
    }
}
