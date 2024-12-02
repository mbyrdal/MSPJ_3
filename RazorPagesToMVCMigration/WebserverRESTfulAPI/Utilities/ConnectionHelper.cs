using Microsoft.Extensions.Configuration;

namespace ServiceAPI.Utilities
{
    /// <summary>
    /// Helper class for retrieving the database connection string from configuration.
    /// </summary>
    public class ConnectionHelper
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionHelper"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration containing connection strings.</param>
        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves the database connection string from configuration.
        /// </summary>
        /// <returns>The connection string.</returns>
        public string GetDBConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Database connection string 'DefaultConnection' is not configured.");
            }

            return connectionString;
        }
    }
}
