using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Input;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    //Auth 2.0 yetkilendirme ile ilgili , openId ise Authentication ile ilgildir.
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpclient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient client, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpclient = client;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<bool>> SıgnIn(SigninInput signInInput)
        {
            // GetDiscoveryDocumentAsync ile settingslerden ıdentityservice adresimize bağlanmak verisini alıyoruz. 
            // Aslında direkt http://localhost:5001 verisine erişiyoruz.

            var disco = await _httpclient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            //  Elde ettiğimiz url ile devam ediyoruz. Settings tarafında verdiğimiz client ıd ve secret a 
            // Kullanıcıdan gelen email password verilerini ekliyoruz. Sonradan bu veriler ile ulaşmak istediğimiz servise
            // IdentityService üzerinden istek yapıp verileri alacağız.
            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                UserName = signInInput.Email,
                Password = signInInput.Password,
                Address = disco.TokenEndpoint
            };

            // Veee tokenı almak için istek yaptık eğer başarılı olursa buradan token sahibi olacağız.
            var token = await _httpclient.RequestPasswordTokenAsync(passwordTokenRequest);

            // İstek neticisinde hata alırsak eğer;
            if (token.IsError)
            {
                var responseContent = await token.HttpResponse.Content.ReadAsStringAsync(); // Hatalı tokenın responsesini okuyoruz
                // Responseyi shared librarydeki errorDto içerisindeki error liste propertysine gönderiyoruz, küçük harf büyük hard duyarlılığını kapatıyoruz.
                var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Response<bool>.Fail(errorDto.Errors, 400);
            }

            // İlk istek neticesinde gelen tokenımızın içerisinde email, prfile vs gibi alanlar eklemiştik
            // Amma velakin bu tokenı iyice şişirmemek için userınforequest ile kullanıcı bilgilerimizi ayrı olarak talep ediyoruz.
            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = disco.UserInfoEndpoint
            };

            var userInfo = await _httpclient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError)
            {
                throw userInfo.Exception;
            }

            //      Özet
            // Şimdik apiye ulaşabilmek için mailimizi şifremizi yazdık yetmedi üstüne client ıd client secretımızı yazdık gönderdik.
            // Eğer giriş yapabildiysek elimizdeki token ile ıdentityserverın userınfoendpointine bize verdıgı tokendaki kullanıcı adı ve şifre ile
            // uyuşan tüm verileri userınfo field ı üzerine aldık. Artık elimizde kullanıcıyı ait tüm veriler ve ve kullanıcının ulaşabildiği
            // servislere ulaşabilmelerini sağlayacak bir token var. Bu kullanıcı bilgileri ile kullanıcı ıcın bir cookie oluşturuyoruz.


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();
            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                  new AuthenticationToken
                  {
                       Name = OpenIdConnectParameterNames.AccessToken,Value = token.AccessToken
                  },
                  new AuthenticationToken
                  {
                       Name = OpenIdConnectParameterNames.RefreshToken,Value = token.RefreshToken
                  },
                  new AuthenticationToken
                  {
                       Name = OpenIdConnectParameterNames.ExpiresIn,Value = DateTime.Now.AddSeconds(token.ExpiresIn)
                       .ToString("o",CultureInfo.InvariantCulture)
                  }
            });

            authenticationProperties.IsPersistent = signInInput.IsRemember;
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal,authenticationProperties);

            return Response<bool>.Success(200);
        }
    }
}
