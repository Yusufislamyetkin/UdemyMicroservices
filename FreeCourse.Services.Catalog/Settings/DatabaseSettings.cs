namespace FreeCourse.Services.Catalog.Settings
{
    // Database bilgilerini classa aktarırız ve iç tarafta bu bilgileri kullanırız. Örnek olarak bağlantı açmak için.
    public class DatabaseSettings: IDatabaseSettings
    {

        public string CourseCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
