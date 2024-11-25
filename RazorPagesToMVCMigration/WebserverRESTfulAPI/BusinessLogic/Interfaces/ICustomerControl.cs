using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICustomerControl
    {
        Customer GetCustomerByID(int ID);
        List<Customer> GetAllCustomers();
        bool AddCustomer(Customer account);
        bool UpdateCustomer(Customer account);
        bool DeleteCustomer(int ID);
    }
}
