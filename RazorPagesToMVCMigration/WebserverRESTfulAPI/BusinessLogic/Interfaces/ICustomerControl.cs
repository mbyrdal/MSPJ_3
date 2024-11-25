using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICustomerControl
    {
        Customer GetAccountByEmail(string email);
        List<Customer> GetAllAccounts();
        bool AddAccount(Customer account);
        bool UpdateAccount(Customer account);
        bool DeleteAccount(string email);
    }
}
