using Microsoft.Data.SqlClient;

namespace ServiceAPI.DatabaseAccess.Utilities
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public bool EntityExists(string tableName, string columnName, string columnValue)
        {
            bool entityExists = false;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string entityExistsQuery = $"SELECT COUNT(1) FROM {tableName} WHERE {columnName} = @value";
                using(SqlCommand checkCommand = new SqlCommand(entityExistsQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@value", columnValue);
                    entityExists = Convert.ToInt32(checkCommand.ExecuteScalar()) == 1;
                }
                conn.Close();
            }
            return entityExists;
        }
    }
}
