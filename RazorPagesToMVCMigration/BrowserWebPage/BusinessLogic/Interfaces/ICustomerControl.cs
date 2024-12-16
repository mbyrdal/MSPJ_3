using BrowserWebPage.DTOs;
using BrowserWebPage.Models;
namespace BrowserWebPage.BusinessLogic.Interfaces
{
    public interface ICustomerControl
    {
        Customer GetCustomerByID(int ID);
        List<Customer> GetAllCustomers();
        bool AddCustomer(Customer customer);
        bool AddCustomerDTO(CustomerViewModel customerDTO);
        bool UpdateCustomer(Customer customer);
        bool UpdateCustomerDTO(CustomerViewModel customerDTO);
        bool DeleteCustomer(int ID);
    }
}
