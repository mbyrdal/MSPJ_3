using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
        Account GetAccountsByID(int id);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(int id);
        void Save();
    }
}
