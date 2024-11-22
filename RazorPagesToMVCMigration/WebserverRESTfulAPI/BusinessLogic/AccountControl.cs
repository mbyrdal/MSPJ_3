using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class AccountControl : IAccountControl
    {
        private readonly DbAccount _dbAccountAccess;

        public AccountControl(DbAccount dbAccountAccess)
        {
            _dbAccountAccess = dbAccountAccess;
        }

        public Account GetAccountByEmail(string email)
        {
            Account accountPlaceholder = null;
            try
            {
                accountPlaceholder = _dbAccountAccess.GetByIdentifier(email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return accountPlaceholder;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> allAccounts = new List<Account>();
            try
            {
                allAccounts = _dbAccountAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allAccounts = null;
                Debug.WriteLine(ex.Message);
            }
            return allAccounts;
        }

        public bool AddAccount(Account account)
        {
            bool guestExists = false;
            bool accountExists = false;
            bool wasAccountInserted = false;
            int numberOfRowsInserted;
            try
            {
                guestExists = _dbAccountAccess.GuestExists(account.Email);
                if (!guestExists)
                {
                    throw new InvalidOperationException($"A Guest with the email '{account.Email}' does not exist in the dbo.Guest table.");
                }

                accountExists = _dbAccountAccess.AccountExists(account.Email);
                if (!accountExists) // CASE: Account does not exist in DB
                {
                    numberOfRowsInserted = _dbAccountAccess.CreateEntity(account);
                    wasAccountInserted = (numberOfRowsInserted == 1);
                }
            }
            catch (Exception ex)
            {
                account = null;
                Debug.WriteLine(ex.Message);
            }
            return wasAccountInserted;
        }

        public bool UpdateAccount(Account account)
        {
            bool guestExists = false;
            bool accountExists = false;
            bool wasAccountUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                guestExists = _dbAccountAccess.GuestExists(account.Email);
                if (!guestExists)
                {
                    throw new InvalidOperationException($"A Guest with the email '{account.Email}' does not exist in the dbo.Guest table.");
                }

                accountExists = _dbAccountAccess.AccountExists(account.Email);
                if (accountExists) // CASE: Account does exist in DB
                {
                    numberOfRowsUpdated = _dbAccountAccess.UpdateEntity(account);
                    wasAccountUpdated = (numberOfRowsUpdated == 1);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasAccountUpdated;
        }

        public bool DeleteAccount(string email)
        {
            bool guestExists = false;
            bool accountExists;
            bool wasProductDeleted = false;
            try
            {
                guestExists = _dbAccountAccess.GuestExists(email);
                if (!guestExists)
                {
                    throw new InvalidOperationException($"A Guest with the email '{email}' does not exist in the dbo.Guest table.");
                }

                accountExists = _dbAccountAccess.AccountExists(email);
                if (accountExists) // CASE: Account does exist in DB
                {
                    wasProductDeleted = _dbAccountAccess.DeleteEntity(email);
                }
            }
            catch (Exception ex)
            {
                wasProductDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasProductDeleted;
        }
    }
}