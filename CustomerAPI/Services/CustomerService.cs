using CustomerAPI.DB;
using CustomerAPI.Models;
using CustomerAPI.Models.Response;
using CustomerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private CustomersDbContext _dbContext;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(CustomersDbContext dbContext, ILogger<CustomerService> logger)
        {
            _dbContext = dbContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomerResponse> AddCustomer(AddCustomerRequest customerDetails, string correlationId)
        {
            try
            {
                _logger.LogInformation($"Adding the customer with firstName: {customerDetails.FirstName} (Correlation Id = {correlationId})");

                _dbContext.Customers.Add(new Customer
                {
                    FirstName = customerDetails.FirstName,
                    LastName = customerDetails.LastName,
                    DateOfBirth = customerDetails.DateOfBirth
                });
                _dbContext.SaveChanges();
                _logger.LogInformation($"Succesfully added the customer with firstName: {customerDetails.FirstName} (Correlation Id = {correlationId})");

                return new CustomerResponse { IsSuccess = true, Error = null};

            }
            catch(Exception e)
            {
                _logger.LogError($"Adding the customer with firstName: {customerDetails.FirstName} has failed with excpetion {e}.(Correlation Id = {correlationId})");

                return new CustomerResponse
                {
                    IsSuccess = false,
                    Error = new CommonError
                    {
                        Code = "101",
                        Message = $"Excpetion thrown while adding the customer - {e}"
                    }
                };
            }           
          
        }

        public Task<IList<Customer>> GetCustomers(string correlationId)
        {
            _logger.LogInformation($"Fetching all the customers saved from Ddatabase. (Correlation Id = {correlationId})");

            var customers = GetCustomersFromDb();
            return customers;
        }
                
        public async Task<IList<Customer>> SearchCustomer(string name, string correlationId)
        {
            _logger.LogInformation($"Searching for all the customers whose firstName/lastName matches with {name}. (Correlation Id = {correlationId})");

            using (var context = _dbContext)
            {
                var customers =  context.Customers
                    .Where(b => b.FirstName.Contains(name) || b.LastName.Contains(name))
                    .ToList();

                return customers;
            }
        }

        public UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest customerDetails, string correlationId)
        {
           try
            {
                _logger.LogInformation($"Updating customer details for the given customer with customerId {customerDetails.Id}. (Correlation Id = {correlationId})");

                var customerEntity = new Customer
                {
                    CustomerId = customerDetails.Id,
                    DateOfBirth = customerDetails.DateOfBirth,
                    FirstName = customerDetails.FirstName,
                    LastName = customerDetails.LastName
                };

                using (var context = _dbContext)
                {
                    context.Customers
                         .Update(customerEntity);
                    context.SaveChanges();
                }
                _logger.LogInformation($"Successfully updated customer details for the given customer with customerId {customerDetails.Id}. (Correlation Id = {correlationId})");

                return new UpdateCustomerResponse { IsSuccess = true, Error = null };
                
            } catch(Exception e)
            {
                _logger.LogError($"Updating the customer with firstName: {customerDetails.FirstName} has failed with excpetion {e}.(Correlation Id = {correlationId})");

                return new UpdateCustomerResponse { IsSuccess = false,
                    Error = new CommonError
                    {
                        Code = "102",
                        Message = $"Excpetion {e} thrown while updating the customer with customerId: {customerDetails.Id}"
                    }
                };
            }
        }

        private async Task<IList<Customer>> GetCustomersFromDb()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            if (customers != null && customers.Any())
            {
                return customers;
            }

            return null;
        }
    }
}
