namespace StudentManagement.Api.JWT
{
    public class JwtConfigurations
    {
        protected readonly string _secretKey;
        protected readonly string _issuer;
        protected readonly string _audience;
        protected readonly IConfiguration _configuration;

        public JwtConfigurations() { }

        public JwtConfigurations(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._secretKey = _configuration.GetValue<string>("JWT:SecretKey");
            this._issuer = _configuration.GetValue<string>("JWT:Issuer");
            this._audience = _configuration.GetValue<string>("JWT:Audience");
        }

    }
}
