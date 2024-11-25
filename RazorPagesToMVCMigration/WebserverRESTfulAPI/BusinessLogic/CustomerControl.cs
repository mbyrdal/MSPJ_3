using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class CustomerControl : ICustomerControl
    {
        private readonly DbCustomer _dbCustomerAccess;

        public CustomerControl(DbCustomer dbCustomerAccess)
        {
            _dbCustomerAccess = dbCustomerAccess;
        }

        public Customer GetCustomerByID(int ID)
        {
            Customer customerPlaceholder = null;
            try
            {
                customerPlaceholder = _dbCustomerAccess.GetByIdentifier(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return customerPlaceholder;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> allCustomers = new List<Customer>();
            try
            {
                allCustomers = _dbCustomerAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allCustomers = null;
                Debug.WriteLine(ex.Message);
            }
            return allCustomers;
        }

        public bool AddCustomer(Customer customer)
        {
            bool customerExists = false;
            bool wasAccountInserted = false;
            int numberOfRowsInserted;
            try
            {
                customerExists = _dbCustomerAccess.CustomerExists(customer.ID, customer.Email);
                if (customerExists) // CASE: Customer does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A Customer with the ID '{customer.ID}' and Email '{customer.Email}' already exists in the Customer table.");
                }

                // Customer does not exist
                numberOfRowsInserted = _dbCustomerAccess.CreateEntity(customer);
                wasAccountInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                customer = null;
                Debug.WriteLine(ex.Message);
            }
            return wasAccountInserted;
        }

        public bool UpdateCustomer(Customer customer)
        {
            bool customerExists = false;
            bool wasCustomerUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                customerExists = _dbCustomerAccess.CustomerExists(customer.ID, customer.Email);
                if (!customerExists) // CASE: Customer does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A Customer with the ID '{customer.ID}' and Email '{customer.Email}' does not exist in the Customer table.");
                }

                // Customer does exist
                numberOfRowsUpdated = _dbCustomerAccess.UpdateEntity(customer);
                wasCustomerUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCustomerUpdated;
        }

        public bool DeleteCustomer(int ID)
        {
            bool customerExists;
            bool wasCustomerDeleted = false;
            try
            {
                string customerEmail = GetCustomerByID(ID).Email;
                customerExists = _dbCustomerAccess.CustomerExists(ID, customerEmail);
                if (!customerExists)
                {
                    throw new InvalidOperationException($"A Customer with the ID '{ID}' and Email '{customerEmail}' does not exist in the Customer table.");
                }

                // CASE: Customer does exist in DB
                wasCustomerDeleted = _dbCustomerAccess.DeleteEntity(ID);
            }
            catch (Exception ex)
            {
                wasCustomerDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasCustomerDeleted;
        }
    }
}