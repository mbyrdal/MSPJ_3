namespace ServiceAPI.DatabaseAccess
{
    public class DbHelper
    {
        private string? _connectionString;
        private readonly IConfiguration _configuration;

        public DbHelper(IConfiguration configuration)
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
