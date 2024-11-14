namespace RazorPagesToMVCMigration.DAL
{
    public class ConfigurationHelper
    {
        private static string? _connectionString;

        public ConfigurationHelper()
        {
            // Call Initialize when class is accessed
            // Ensures that the connection string is set
            Initialize();
        }

        static void Initialize()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            _connectionString = config.GetConnectionString("HildurConnection");
        }

        public static string GetDBConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not initialized.");
            }
            // hildur.ucn.dk
            return _connectionString;
        }
    }
}
