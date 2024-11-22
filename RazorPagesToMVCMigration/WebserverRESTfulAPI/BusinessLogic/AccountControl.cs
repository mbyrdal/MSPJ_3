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
            bool accountExists = false;
            bool wasAccountInserted = false;
            int numberOfRowsInserted;
            try
            {
                accountExists = _dbAccountAccess.AccountExists(account.Email);
                if (!accountExists)
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
            bool accountExists = false;
            bool wasAccountUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                accountExists = _dbAccountAccess.AccountExists(account.Email);
                if (accountExists) // CASE: Product does exist in DB.
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
            bool accountExists;
            bool wasProductDeleted = false;
            try
            {
                accountExists = _dbAccountAccess.AccountExists(email);
                if (accountExists) // CASE: Product does exist in DB.
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