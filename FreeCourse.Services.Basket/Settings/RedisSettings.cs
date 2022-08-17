namespace FreeCourse.Services.Basket.Settings
{
    public class RedisSettings
    {
        // Burayı Catalog da olduğu gibi interface üzerinden implente etmeye çalışacağız. daha sonra test aşaması için
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
