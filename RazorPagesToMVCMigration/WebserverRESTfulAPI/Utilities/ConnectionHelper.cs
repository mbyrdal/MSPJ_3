namespace ServiceAPI.Utilities
{
    public class ConnectionHelper
    {
        private string? _connectionString;

        private IConfigurationSection _jwtSettings;
        private string? _jwtKey;
        private string? _jwtIssuer;
        private string? _jwtAudience;

        private readonly IConfiguration _configuration;

        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            // Set connection string
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Jwt
            _jwtSettings = _configuration.GetSection("Jwt");
            _jwtKey = _jwtSettings.GetSection("SecretKey").Value;
            _jwtIssuer = _jwtSettings.GetSection("Issuer").Value;
            _jwtAudience = _jwtSettings.GetSection("Audience").Value;
        }

        public string GetDBConnectionString()
        {
            // hildur.ucn.dk
            return _connectionString;
        }

        public JwtSettings GetJwtSettings()
        {
            return _jwtSettings.Get<JwtSettings>();
        }

        public string GetJwtSecretKey()
        {
            return _jwtKey;
        }

        public string GetJwtIssuer()
        {
            return _jwtIssuer;
        }

        public string GetJwtAudience()
        {
            return _jwtAudience;
        }
    }
}
