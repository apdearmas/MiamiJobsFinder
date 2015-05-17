using System.Configuration;
using System.Net;
using System.Net.Mail;
using SendGrid;

namespace BDL
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(string targetEmail, string subject, string message)
        {
            var username = ConfigurationManager.AppSettings["SendGridUserName"];
            var pswd = ConfigurationManager.AppSettings["SendGridPassword"];

            var credentials = new NetworkCredential(username, pswd);

            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            myMessage.AddTo(targetEmail);
            myMessage.From = new MailAddress("apdearmas@sendgrid.com", "Antonio Pereira");
            myMessage.Subject = subject;
            myMessage.Html = message;

            
            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);
            
        }
    }
}

//using System;
//using System.Configuration;
//using System.Net.Http;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using Microsoft.KeyVault.Client;
//
//namespace BDL
//{
//    public class SmtpClientWrapper : ISmtpClientWrapper
//    {
//        static KeyVaultClient keyVaultClient;
//
//        public void Send(string targetEmail, string subject, string message)
//        {
//            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessToken), setRequestUriCallback: SetRequestUri);
//        }
//
//        /// <summary>
//        /// Gets the access token
//        /// </summary>
//        /// <param name="authority"> Authority </param>
//        /// <param name="resource"> Resource </param>
//        /// <param name="scope"> scope </param>
//        /// <returns> token </returns>
//        public static string GetAccessToken(string authority, string resource, string scope)
//        {
//            var client_id = ConfigurationManager.AppSettings["AuthClientId"];
//            var client_secret = ConfigurationManager.AppSettings["AuthClientSecret"];
//
//            var clientCredential = new ClientCredential(client_id, client_secret);
//            var context = new AuthenticationContext(authority, null);
//            var result = context.AcquireToken(resource, clientCredential);
//
//            return result.AccessToken;
//        }
//
//        /// <summary>
//        /// Sets the target request URI and add host to the http client header
//        /// </summary>
//        /// <param name="requestUri"> the request URI </param>
//        /// <param name="httpClient"> the http client to set its header </param>
//        /// <returns> the updated request URI with new endpoint </returns>
//        public static Uri SetRequestUri(Uri requestUri, HttpClient httpClient)
//        {
//            var authority = string.Empty;
//            var targetUri = requestUri;
//
//            // NOTE: The KmsNetworkUrl setting is purely for development testing on the
//            //       Microsoft Azure Development Fabric and should not be used outside that environment.
//            string networkUrl = ConfigurationManager.AppSettings["KmsNetworkUrl"];
//
//            if (!string.IsNullOrEmpty(networkUrl))
//            {
//                authority = targetUri.Authority;
//                targetUri = new Uri(new Uri(networkUrl), targetUri.PathAndQuery);
//
//                httpClient.DefaultRequestHeaders.Add("Host", authority);
//            }
//
//            return targetUri;
//        }
//    }
//}

