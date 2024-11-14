namespace RazorPagesToMVCMigration.DAL
{
    public class ConfigurationHelper
    {
        private readonly string _connectionString;

        public ConfigurationHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
