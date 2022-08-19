namespace FreeCourse.Web.Models
{
    // Servislerle iletişim kurmamız için göndereceğimiz Client ID, Client Screet
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client WebClientForUser { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
