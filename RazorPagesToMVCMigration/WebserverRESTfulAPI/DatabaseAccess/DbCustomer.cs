using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCustomer : ICRUD_DB<Customer>
    {
        private readonly string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCustomer(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public async Task<List<Customer>> GetAllEntitiesAsync()
        {
            var customers = new List<Customer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Customer", conn))
                    {
                        using (SqlDataReader reader = await readAllCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = MapCustomerFromReader(reader);
                                customers.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving customers: {ex.Message}");
            }
            return customers;
        }

        public async Task<Customer> GetByIdentifierAsync(int ID)
        {
            Customer customer = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readCommand = new SqlCommand(
                        "SELECT ID, FirstName, LastName, Address, PhoneNum, Email FROM Customer WHERE ID = @ID", conn))
                    {
                        readCommand.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = await readCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                customer = MapCustomerFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving customer with ID {ID}: {ex.Message}");
            }
            return customer;
        }

        public async Task<int> CreateEntityAsync(Customer newCustomer)
        {
            int rowsInserted = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand createCommand = new SqlCommand(
                        "INSERT INTO Customer (FirstName, LastName, Address, PhoneNum, Email) VALUES (@FirstName, @LastName, @Address, @PhoneNum, @Email)", conn))
                    {
                        createCommand.Parameters.AddWithValue("@FirstName", newCustomer.FirstName);
                        createCommand.Parameters.AddWithValue("@LastName", newCustomer.LastName);
                        createCommand.Parameters.AddWithValue("@Address", newCustomer.Address);
                        createCommand.Parameters.AddWithValue("@PhoneNum", newCustomer.PhoneNum);
                        createCommand.Parameters.AddWithValue("@Email", newCustomer.Email);

                        rowsInserted = await createCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating a customer: {ex.Message}");
            }
            return rowsInserted;
        }

        public async Task<int> UpdateEntityAsync(Customer updateCustomer)
        {
            int rowsUpdated = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Address = @Address, PhoneNum = @PhoneNum, Email = @Email WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCustomer.ID);
                        updateCommand.Parameters.AddWithValue("@FirstName", updateCustomer.FirstName);
                        updateCommand.Parameters.AddWithValue("@LastName", updateCustomer.LastName);
                        updateCommand.Parameters.AddWithValue("@Address", updateCustomer.Address);
                        updateCommand.Parameters.AddWithValue("@PhoneNum", updateCustomer.PhoneNum);
                        updateCommand.Parameters.AddWithValue("@Email", updateCustomer.Email);

                        rowsUpdated = await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating customer with ID {updateCustomer.ID}: {ex.Message}");
            }
            return rowsUpdated;
        }

        public async Task<bool> DeleteEntityAsync(int ID)
        {
            bool isDeleted = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Customer WHERE ID = @ID", conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@ID", ID);
                        isDeleted = await deleteCommand.ExecuteNonQueryAsync() == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting customer with ID {ID}: {ex.Message}");
            }
            return isDeleted;
        }

        public async Task<bool> CustomerExistsAsync(int ID, string email)
        {
            try
            {
                bool idExists = await _dbHelper.EntityExistsAsync("Customer", "ID", ID.ToString());
                bool emailExists = await _dbHelper.EntityExistsAsync("Customer", "Email", email);
                return idExists && emailExists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while checking if customer exists: {ex.Message}");
                return false;
            }
        }

        // Helper Method to Map Customer from SqlDataReader
        private Customer MapCustomerFromReader(SqlDataReader reader)
        {
            return new Customer(
                reader.GetInt32(reader.GetOrdinal("ID")),
                reader.GetString(reader.GetOrdinal("FirstName")),
                reader.GetString(reader.GetOrdinal("LastName")),
                reader.GetString(reader.GetOrdinal("Address")),
                reader.GetString(reader.GetOrdinal("PhoneNum")),
                reader.GetString(reader.GetOrdinal("Email"))
            );
        }
    }
}
