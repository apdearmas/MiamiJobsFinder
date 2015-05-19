using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace BDL
{
    public class AzureKeyVaultService : IAzureKeyVaultService
    {
        private static AzureKeyVaultService _instance;
        private readonly KeyVaultClient keyVaultClient;

        private readonly string sendGridUserName;
        private readonly string sendGridPassword;

        private AzureKeyVaultService()
        {
            keyVaultClient = new KeyVaultClient(GetAccessToken);

            sendGridUserName = GetSecret("https://miamijobsfinder.vault.azure.net:443/secrets/SendGridUserName/86cac771cd5c4c5ebe49d746c4237f72").Value;
            sendGridPassword = GetSecret("https://miamijobsfinder.vault.azure.net:443/secrets/SendGridPassword/cd167b9f76fa4c998cda6bf52b4434fb").Value;
        }


        public static AzureKeyVaultService Instance
        {
            get { return _instance ?? (_instance = new AzureKeyVaultService()); }
        }


        public string SendGridUserName {
            get
            {
                return sendGridUserName;
            }
        }

        public string SendGridPassword {
            get
            {
                return sendGridPassword;
            }
        }


        public static Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            var clientId = ConfigurationManager.AppSettings["AuthClientId"];
            var clientSecret = ConfigurationManager.AppSettings["AuthClientSecret"];

            var clientCredential = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext(authority, null);
            var result = context.AcquireToken(resource, clientCredential);

            return Task.FromResult(result.AccessToken);
        }

        private Secret GetSecret(string url)
        {
            return keyVaultClient.GetSecretAsync(url).GetAwaiter().GetResult();

        }


    }
}
