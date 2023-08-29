namespace EmpCore.WebApi.Swagger
{
    public class OAuth2Options
    {
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public List<string> Scopes { get; set; }
    }
}