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

        private AzureKeyVaultService()
        {
            keyVaultClient = new KeyVaultClient(GetAccessToken);
        }

        public static AzureKeyVaultService Instance
        {
            get { return _instance ?? (_instance = new AzureKeyVaultService()); }
        }


        public string SendGridUserName {
            get
            {
                var secret =
                    keyVaultClient.GetSecretAsync(
                        "https://miamijobsfinder.vault.azure.net:443/secrets/SendGridUserName/86cac771cd5c4c5ebe49d746c4237f72")
                        .GetAwaiter()
                        .GetResult();
                return secret.Value;
            }
        }

        public string SendGridPassword {
            get
            {
                var secret =
                    keyVaultClient.GetSecretAsync(
                        "https://miamijobsfinder.vault.azure.net:443/secrets/SendGridPassword/cd167b9f76fa4c998cda6bf52b4434fb")
                        .GetAwaiter()
                        .GetResult();
                return secret.Value;
            }
        }


        public static Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            var client_id = ConfigurationManager.AppSettings["AuthClientId"];
            var client_secret = ConfigurationManager.AppSettings["AuthClientSecret"];

            var clientCredential = new ClientCredential(client_id, client_secret);
            var context = new AuthenticationContext(authority, null);
            var result = context.AcquireToken(resource, clientCredential);

            return Task.FromResult(result.AccessToken);
        }

    }
}
