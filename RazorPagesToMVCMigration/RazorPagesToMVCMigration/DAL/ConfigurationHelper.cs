namespace RazorPagesToMVCMigration.DAL
{
    public class ConfigurationHelper
    {
        private static readonly ConfigurationBuilder _configurationBuilder = new ConfigurationBuilder();
        public static string GetDBConnectionString()
        {
            var Configuration = _configurationBuilder.AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            string? connectionString = Configuration.GetConnectionString("HildurConnection");
            return connectionString;
        }
    }
}
