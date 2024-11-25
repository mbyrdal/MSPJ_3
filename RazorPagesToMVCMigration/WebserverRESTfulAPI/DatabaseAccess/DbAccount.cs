using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Net;

namespace ServiceAPI.DatabaseAccess
{
    public class DbAccount : ICRUD_DB<Account>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbAccount(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
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
                            Account accountInTable = new Account(
                                reader.GetString(reader.GetOrdinal("FK_GuestEmail")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("PhoneNum")),
                                reader.GetString(reader.GetOrdinal("HashPassword")));

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
            Account account = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT FK_GuestEmail, FirstName, LastName, Address, PhoneNum, HashPassword FROM Account WHERE FK_GuestEmail = @email", conn))
                {
                    readCommand.Parameters.AddWithValue("@email", email);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account(
                            reader.GetString(reader.GetOrdinal("FK_GuestEmail")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetString(reader.GetOrdinal("Address")),
                            reader.GetString(reader.GetOrdinal("PhoneNum")),
                            reader.GetString(reader.GetOrdinal("HashPassword")));
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
                    "INSERT INTO Account (FK_GuestEmail, FirstName, LastName, Address, PhoneNum, HashPassword) " +
                    "VALUES (@email, @firstname, @lastname, @address, @phonenum, @hashpw)", conn))
                {
                    createCommand.Parameters.AddWithValue("@email", newAccount.Email);
                    createCommand.Parameters.AddWithValue("@firstname", newAccount.FirstName);
                    createCommand.Parameters.AddWithValue("@lastname", newAccount.LastName);
                    createCommand.Parameters.AddWithValue("@address", newAccount.Address);
                    createCommand.Parameters.AddWithValue("@phonenum", newAccount.PhoneNum);
                    createCommand.Parameters.AddWithValue("@hashpw", newAccount.HashPassword);
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
                                                                     "SET FK_GuestEmail=@email, FirstName=@firstname, LastName=@lastname, Address=@address, PhoneNum=@phonenum, HashPassword=@hashpw" +
                                                                     "WHERE FK_GuestEmail = @email", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@email", updateAccount.Email);
                        updateCommand.Parameters.AddWithValue("@firstname", updateAccount.FirstName);
                        updateCommand.Parameters.AddWithValue("@lastname", updateAccount.LastName);
                        updateCommand.Parameters.AddWithValue("@address", updateAccount.Address);
                        updateCommand.Parameters.AddWithValue("@phonenum", updateAccount.PhoneNum);
                        updateCommand.Parameters.AddWithValue("@hashpw", updateAccount.HashPassword);

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

        internal bool AccountExists(string email)
        {
            bool guestExists = GuestExists(email);
            if(!guestExists)
            {
                return false;
            }

            return _dbHelper.EntityExists("Account", "FK_GuestEmail", email);
        }

        internal bool GuestExists(string email)
        {
            return _dbHelper.EntityExists("Guest", "Email", email);
        }
    }
}
