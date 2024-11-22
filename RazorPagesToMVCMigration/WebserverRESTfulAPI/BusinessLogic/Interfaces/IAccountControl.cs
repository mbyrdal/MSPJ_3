using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IAccountControl
    {
        Account GetAccountByEmail(string email);
        List<Account> GetAllAccounts();
        bool AddAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
    }
}
