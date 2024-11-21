namespace ServiceAPI.DatabaseAccess.Utilities
{
    public class ConnectionHelper
    {
        private string? _connectionString;
        private readonly IConfiguration _configuration;

        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            // Set connection string
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public string GetDBConnectionString()
        {
            // hildur.ucn.dk
            return _connectionString;
        }
    }
}
