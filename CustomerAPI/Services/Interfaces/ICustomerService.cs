using CustomerAPI.DB;
using CustomerAPI.Models;
using CustomerAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetCustomers(string correlationId);
        Task<Models.Response.CustomerResponse> AddCustomer(AddCustomerRequest customerDetails, string correlationId);
        UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest customerDetails, string correlationId);
        Task<IList<Customer>> SearchCustomer(string name, string correlationId);
    }
}
