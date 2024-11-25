using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class CustomerControl : ICustomerControl
    {
        private readonly DbCustomer _dbAccountAccess;

        public CustomerControl(DbCustomer dbAccountAccess)
        {
            _dbAccountAccess = dbAccountAccess;
        }

        public Customer GetAccountByEmail(string email)
        {
            Customer accountPlaceholder = null;
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

        public List<Customer> GetAllAccounts()
        {
            List<Customer> allAccounts = new List<Customer>();
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

        public bool AddAccount(Customer account)
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
                if (!accountExists) // CASE: Customer does not exist in DB
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

        public bool UpdateAccount(Customer account)
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
                if (accountExists) // CASE: Customer does exist in DB
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
                if (accountExists) // CASE: Customer does exist in DB
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