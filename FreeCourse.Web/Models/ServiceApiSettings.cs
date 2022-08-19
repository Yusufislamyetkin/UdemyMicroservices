﻿namespace FreeCourse.Web.Models
{
    //Oluşturduğumuz Gateway ve PhotoStock servisleri ile iletişim kuracağımız URL'ler
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
    }
}
