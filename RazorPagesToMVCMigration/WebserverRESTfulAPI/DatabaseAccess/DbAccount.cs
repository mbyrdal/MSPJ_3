using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DatabaseAccess.Utilities;
using ServiceAPI.Models;

namespace ServiceAPI.DatabaseAccess
{
    public class DbAccount : ICRUD_DB<Account>
    {
        // Configuration steps
        private string _connectionString;
        public DbAccount(IConfiguration configuration)
        {
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }
        public List<Account> GetAllEntities()
        {
            List<Account> accounts = new List<Account>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Account", conn))
                {
                    using (SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Account accountInTable = new Account
                            {
                                Email = reader.GetString(reader.GetOrdinal("FK_GuestEmail")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNum = reader.GetString(reader.GetOrdinal("PhoneNum"))
                            };
                            accounts.Add(accountInTable);
                        }
                    }
                }
                conn.Close();
            }
            return accounts;
        }
        public Account GetByIdentifier(string email)
        {
            Account account = new Account();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT FK_GuestEmail, FirstName, LastName, Address, PhoneNum FROM Account WHERE FK_GuestEmail = @email", conn))
                {
                    readCommand.Parameters.AddWithValue("@email", email);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account
                            {
                                Email = reader.GetString(reader.GetOrdinal("FK_GuestEmail")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNum = reader.GetString(reader.GetOrdinal("PhoneNum"))
                            };
                        }
                    }
                }
                conn.Close();
            }
            return account;
        }
        public int CreateEntity(Account newAccount)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Account (FK_GuestEmail, FirstName, LastName, Address, PhoneNum) "
                    + "VALUES (@email, @firstname, @lastname, @address, @phonenum)", conn
                    ))
                {
                    createCommand.Parameters.AddWithValue("@email", newAccount);
                    createCommand.Parameters.AddWithValue("@firstname", newAccount);
                    createCommand.Parameters.AddWithValue("@lastname", newAccount);
                    createCommand.Parameters.AddWithValue("@address", newAccount);
                    createCommand.Parameters.AddWithValue("@phonenum", newAccount);

                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }
        public int UpdateEntity(Account updateAccount)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Account " +
                                                                     "SET FK_GuestEmail=@email, FirstName=@firstname, LastName=@lastname, Address=@address, PhoneNum=@phonenum " +
                                                                     "WHERE FK_GuestEmail = @email", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@email", updateAccount);
                        updateCommand.Parameters.AddWithValue("@firstname", updateAccount);
                        updateCommand.Parameters.AddWithValue("@lastname", updateAccount);
                        updateCommand.Parameters.AddWithValue("@address", updateAccount);
                        updateCommand.Parameters.AddWithValue("@phonenum", updateAccount);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
                return numberOfRowsUpdated;
            }
        }
        public bool DeleteEntity(string email)
        {
            bool wasAccountDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from Account WHERE FK_GuestEmail = @email", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@email", email);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasAccountDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasAccountDeleted;
        }
        internal bool AccountExists(string OEM)
        {
            bool accExists = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string existQuery = "SELECT COUNT(1) FROM Account WHERE FK_GuestEmail = @email";
                using (SqlCommand checkCommand = new SqlCommand(existQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@email", OEM);
                    accExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                }
                conn.Close();
            }
            return accExists;
        }
    }
}
