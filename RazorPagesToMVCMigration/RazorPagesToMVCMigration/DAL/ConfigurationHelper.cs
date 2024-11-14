namespace RazorPagesToMVCMigration.DAL
{
    public class ConfigurationHelper
    {
        private string? _connectionString;
        private readonly IConfiguration _configuration;

        public ConfigurationHelper(IConfiguration configuration)
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
