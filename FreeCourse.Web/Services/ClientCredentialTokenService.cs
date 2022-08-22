using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings; // Appsettings den tip güvenli aldığımız istek yapılacak adres.
        private readonly ClientSettings _clientSettings; // Appsettings den tip güvenli aldığımız client ıd client secret değeleri.

        private readonly HttpClient _httpClient;  // Projemizin bir başka uç noktayla (endpoint) iletişim kurması ve bu noktaya Http istekleri atabilmesini sağlayan bir sınıftır.
                                                 // Http isteklerimizi Get, Put, Delete veya Post olarak asenkron olarak yapmamızı sağlamaktadır. .

        private readonly IClientAccessTokenCache _clientAccessTokenCache; //Password'da oturum açıldığında cookie oluşturuyor. Onun üzerinde verileri tutuyorduk
        // Burada ise cache yapısında tutacağız verileri.

        public ClientCredentialTokenService(IOptions<ServiceApiSettings>serviceApiSettings,IOptions<ClientSettings>clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
             _httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken");
            // Daha önceden token almışmıyız diyerekten kontrol ediyoruz varsa eğer elimizde token return ediyoruz.
            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }

            // GetDiscoveryDocumentAsync ile settingslerden ıdentityservice adresimize bağlanmak verisini alıyoruz. 
            // Aslında direkt http://localhost:5001 verisine erişiyoruz.

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            // Elimizde olan istek url'si client ıd ve secret ile bir token alma requestinde bulunacak şablonu hazırlıyoruz.
            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = disco.TokenEndpoint
            };

            // Elimizdeki token şablonu ile token talebinde bulunuyoruz.
            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken,newToken.ExpiresIn);

            return newToken.AccessToken;

        }
    }
}
