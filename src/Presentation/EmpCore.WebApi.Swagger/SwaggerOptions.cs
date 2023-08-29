namespace EmpCore.WebApi.Swagger
{
    public class SwaggerOptions
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool SerializeAsOpenApiV2 { get; set; }
        public OAuth2Options OAuth2 { get; set; }
        public JwtOptions Jwt { get; set; }
    }
}