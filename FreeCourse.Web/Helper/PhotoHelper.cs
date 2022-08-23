using FreeCourse.Web.Models;
using Microsoft.Extensions.Options;

namespace FreeCourse.Web.Helper
{
    public class PhotoHelper
    {
        // Verilen url'yi PhotoStock API wwwroot alanından getirir. Burada Helper'ı bir servis gibi tanımalamadık IOptions Paterni kullanarak istek yapacağımız adresi
        // direkt olarak appsettings.json dan çektik. 
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/{photoUrl}";
        }

    }
}
