using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Net;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCustomer : ICRUD_DB<Customer>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCustomer(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public List<Customer> GetAllEntities()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Customer", conn))
                {
                    using (SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer accountInTable = new Customer(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Email")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("PhoneNum")));

                            customers.Add(accountInTable);
                        }
                    }
                }
                conn.Close();
            }
            return customers;
        }

        public Customer GetByIdentifier(int ID)
        {
            Customer customer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT ID, Email, FirstName, LastName, Address, PhoneNum FROM Customer WHERE ID = @ID", conn))
                {
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("Email")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetString(reader.GetOrdinal("Address")),
                            reader.GetString(reader.GetOrdinal("PhoneNum")));
                        };
                    }
                }
                conn.Close();
            }
            return customer;
        }

        public int CreateEntity(Customer newCustomer)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Customer (ID, Email, FirstName, LastName, Address, PhoneNum) " +
                    "VALUES (@ID, @Email, @FirstName, @LastName, @Address, @PhoneNum)", conn))
                {
                    createCommand.Parameters.AddWithValue("@ID", newCustomer.ID);
                    createCommand.Parameters.AddWithValue("@Email", newCustomer.Email);
                    createCommand.Parameters.AddWithValue("@Firstname", newCustomer.FirstName);
                    createCommand.Parameters.AddWithValue("@Lastname", newCustomer.LastName);
                    createCommand.Parameters.AddWithValue("@Address", newCustomer.Address);
                    createCommand.Parameters.AddWithValue("@Phonenum", newCustomer.PhoneNum);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(Customer updateCustomer)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Customer " +
                                                                     "SET ID=@ID, Email=@Email, FirstName=@FirstName, LastName=@LastName, Address=@Address, PhoneNum=@PhoneNum" +
                                                                     "WHERE ID = @ID AND Email = @Email", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCustomer.ID);
                        updateCommand.Parameters.AddWithValue("@Email", updateCustomer.Email);
                        updateCommand.Parameters.AddWithValue("@Firstname", updateCustomer.FirstName);
                        updateCommand.Parameters.AddWithValue("@Lastname", updateCustomer.LastName);
                        updateCommand.Parameters.AddWithValue("@Address", updateCustomer.Address);
                        updateCommand.Parameters.AddWithValue("@Phonenum", updateCustomer.PhoneNum);

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

        public bool DeleteEntity(int ID)
        {
            bool wasCustomerDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from Customer WHERE ID = @ID", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasCustomerDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasCustomerDeleted;
        }

        internal bool CustomerExists(int ID, string email)
        {
            bool customerIdentifierExists = _dbHelper.EntityExists("Customer", "ID", ID.ToString());
            bool customerEmailExists = _dbHelper.EntityExists("Customer", "Email", email);

            bool customerExists = customerIdentifierExists && customerEmailExists;

            if (!customerExists)
            {
                return false;
            }

            return customerExists;
        }
    }
}
