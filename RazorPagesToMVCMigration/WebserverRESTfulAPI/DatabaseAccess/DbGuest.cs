using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Diagnostics;
using System.Security.Principal;

namespace ServiceAPI.DatabaseAccess
{
    public class DbGuest : ICRUD_DB_Guest
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbGuest(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public List<Guest> GetAllEntities()
        {
            List<Guest> guests = new List<Guest>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Guest", conn))
                {
                    using(SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guest guest = new Guest
                            {
                                Email = reader.GetString(reader.GetOrdinal("Email"))
                            };
                            guests.Add(guest);
                        }
                    }
                }
                conn.Close();
            }
            return guests;
        }

        public Guest GetByIdentifier(string email)
        {
            Guest guest = null;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand readCommand = new SqlCommand("SELECT Email FROM Guest WHERE Email = @email", conn))
                {
                    readCommand.Parameters.AddWithValue("@email", email);
                    using(SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            guest = new Guest
                            {
                                Email = reader.GetString(reader.GetOrdinal("Email"))
                            };
                        }
                    }
                    conn.Close();
                }
                return guest;
            }
        }

        public int CreateEntity(Guest newGuest)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand("INSERT INTO Guest (Email) VALUES (@email)", conn))
                {
                    createCommand.Parameters.AddWithValue("@email", newGuest.Email);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(Guest updateGuest)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Guest SET Email=@email WHERE Email = @email", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@email", updateGuest.Email);

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

        public int UpdateEntityWithParameters(string email, Guest updateGuest)
        {
            int numberOfRowsUpdated = 0;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Guest SET Email = @NewEmail WHERE Email = @CurrentEmail", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@NewEmail", updateGuest.Email);
                        updateCommand.Parameters.AddWithValue("@CurrentEmail", email);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                }
                catch(SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return numberOfRowsUpdated;
            }
        }

        public bool DeleteEntity(string email)
        {
            bool wasGuestDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from Guest WHERE Email = @email", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@email", email);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasGuestDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasGuestDeleted;
        }

        internal bool GuestExists(string email)
        {
            return _dbHelper.EntityExists("Guest", "Email", email);
        }
    }
}
