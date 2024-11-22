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
                allProducts = null;
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

        public bool DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }
        public bool UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
