using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.DatabaseAccess
{
    public class DbUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        // Configuration steps
        private readonly string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbUserStore(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand createUserCommand = new SqlCommand("(INSERT INTO Admins (Id, UserName, Email, PasswordHash, SecurityStamp) " +
                                                              "VALUES " +
                                                              "(@Id, @UserName, @Email, @PasswordHash, @SecurityStamp)", conn);
                createUserCommand.Parameters.AddWithValue("@Id", user.Id);
                createUserCommand.Parameters.AddWithValue("@UserName", user.Username);
                createUserCommand.Parameters.AddWithValue("@Email", user.Email);
                createUserCommand.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                createUserCommand.Parameters.AddWithValue("@SecurityStamp", user.SecurityStamp);

                await createUserCommand.ExecuteNonQueryAsync();
            }
            return IdentityResult.Success;
        }

        public async Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = null;

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand findByIdCommand = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", conn);
                findByIdCommand.Parameters.AddWithValue("@Id", userId);

                using (SqlDataReader reader = await findByIdCommand.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync(cancellationToken))
                    {
                        applicationUser = new ApplicationUser
                        {
                            Id = reader.GetString(reader.GetOrdinal("Id")),
                            Username = reader.GetString(reader.GetOrdinal("UserName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            SecurityStamp = reader.GetString(reader.GetOrdinal("SecurityStamp"))
                        };
                    } 
                }
            }
            return applicationUser;
        }

        public async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            // Placeholder ApplicationUser
            ApplicationUser applicationUser = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand findByNameCommand = new SqlCommand("SELECT * FROM Users WHERE UserName = @UserName", conn);
                findByNameCommand.Parameters.AddWithValue("@UserName", normalizedUserName);

                using(SqlDataReader reader = await findByNameCommand.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        applicationUser = new ApplicationUser
                        {
                            Id = reader.GetString(reader.GetOrdinal("Id")),
                            Username = reader.GetString(reader.GetOrdinal("UserName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            SecurityStamp = reader.GetString(reader.GetOrdinal("SecurityStamp"))
                        };
                    }
                }
            }
            return applicationUser;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand deleteUserCommand = new SqlCommand("DELETE FROM Users WHERE Id = @Id", conn);
                deleteUserCommand.Parameters.AddWithValue("@Id", user.Id);

                int rowsAffected = await deleteUserCommand.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return IdentityResult.Success;
                }
            }
            IdentityError noUserFound = new IdentityError() { Description = $"User with ID {user.Id} could not be deleted." };
            return IdentityResult.Failed(noUserFound);
        }

        // We typically implement Dispose() to clean up unmanaged resources.
        // However, since we are use 'using' with ADO.NET for Database Access, implementation is unnecessary.
        public void Dispose()
        {
            
        }

        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            // Retrieves the normalized () UserName from the user. Normalization using UPPERCASE variant.
            return Task.FromResult(user.Username?.ToUpperInvariant());
        }

        public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.Username = normalizedName?.ToUpperInvariant();
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand updateUserCommand = new SqlCommand("UPDATE Users SET " +
                                                              "UserName = @UserName, Email = @Email, " +
                                                              "PasswordHash = @PasswordHash, SecurityStamp = @SecurityStamp " +
                                                              "WHERE Id = @Id", conn);
                updateUserCommand.Parameters.AddWithValue("@Id", user.Id);
                updateUserCommand.Parameters.AddWithValue("@UserName", user.Username);
                updateUserCommand.Parameters.AddWithValue("@Email", user.Email);
                updateUserCommand.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                updateUserCommand.Parameters.AddWithValue("@SecurityStamp", user.SecurityStamp);

                int rowsAffected = await updateUserCommand.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return IdentityResult.Success;
                }
            }
            IdentityError userNotUpdated = new IdentityError() { Description = $"User with ID {user.Id} could not be updated." };
            return IdentityResult.Failed(userNotUpdated);
        }
    }
}
