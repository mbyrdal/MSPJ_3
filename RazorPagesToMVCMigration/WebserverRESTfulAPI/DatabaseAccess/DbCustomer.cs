using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
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
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetString(reader.GetOrdinal("Address")),
                                reader.GetString(reader.GetOrdinal("PhoneNum")),
                                reader.GetString(reader.GetOrdinal("Email")));

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
                using (SqlCommand readCommand = new SqlCommand("SELECT ID, FirstName, LastName, Address, PhoneNum, Email FROM Customer WHERE ID = @ID", conn))
                {
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetString(reader.GetOrdinal("Address")),
                            reader.GetString(reader.GetOrdinal("PhoneNum")),
                            reader.GetString(reader.GetOrdinal("Email")));
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
                    "INSERT INTO Customer (FirstName, LastName, Address, PhoneNum, Email) " +
                    "VALUES (@FirstName, @LastName, @Address, @PhoneNum, @Email)", conn))
                {
                    createCommand.Parameters.AddWithValue("@FirstName", newCustomer.FirstName);
                    createCommand.Parameters.AddWithValue("@LastName", newCustomer.LastName);
                    createCommand.Parameters.AddWithValue("@Address", newCustomer.Address);
                    createCommand.Parameters.AddWithValue("@PhoneNum", newCustomer.PhoneNum);
                    createCommand.Parameters.AddWithValue("@Email", newCustomer.Email);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        // DTO VERSION
        public int CreateEntityDTO(CustomerViewModel newCustomerDTO)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Customer (FirstName, LastName, Address, PhoneNum, Email) " +
                    "VALUES (@FirstName, @LastName, @Address, @PhoneNum, @Email)", conn))
                {
                    createCommand.Parameters.AddWithValue("@FirstName", newCustomerDTO.FirstName);
                    createCommand.Parameters.AddWithValue("@LastName", newCustomerDTO.LastName);
                    createCommand.Parameters.AddWithValue("@Address", newCustomerDTO.Address);
                    createCommand.Parameters.AddWithValue("@PhoneNum", newCustomerDTO.PhoneNum);
                    createCommand.Parameters.AddWithValue("@Email", newCustomerDTO.Email);
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
                                                                     "SET FirstName=@FirstName, LastName=@LastName, Address=@Address, PhoneNum=@PhoneNum, Email=@Email " +
                                                                     "WHERE ID = @ID AND Email = @Email", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@Firstname", updateCustomer.FirstName);
                        updateCommand.Parameters.AddWithValue("@Lastname", updateCustomer.LastName);
                        updateCommand.Parameters.AddWithValue("@Address", updateCustomer.Address);
                        updateCommand.Parameters.AddWithValue("@Phonenum", updateCustomer.PhoneNum);
                        updateCommand.Parameters.AddWithValue("@Email", updateCustomer.Email);

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

        // DTO VERSION
        public int UpdateEntityDTO(CustomerViewModel updateCustomerDTO)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Customer " +
                                                                     "SET FirstName=@FirstName, LastName=@LastName, Address=@Address, PhoneNum=@PhoneNum, Email=@Email " +
                                                                     "WHERE ID = @ID AND Email = @Email", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@Firstname", updateCustomerDTO.FirstName);
                        updateCommand.Parameters.AddWithValue("@Lastname", updateCustomerDTO.LastName);
                        updateCommand.Parameters.AddWithValue("@Address", updateCustomerDTO.Address);
                        updateCommand.Parameters.AddWithValue("@Phonenum", updateCustomerDTO.PhoneNum);
                        updateCommand.Parameters.AddWithValue("@Email", updateCustomerDTO.Email);

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
