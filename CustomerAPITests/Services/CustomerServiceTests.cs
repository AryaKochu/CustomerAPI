using CustomerAPI.DB;
using CustomerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using FluentAssertions;
using CustomerAPI.Models;

namespace CustomerAPITests.Services
{
    public class CustomerServiceTests : MockCustomerDbContext
    {
        private ILogger<CustomerService> _logger;
        private CustomerService _customerService;

        public CustomerServiceTests() : base(
        new DbContextOptionsBuilder<CustomersDbContext>()
            .UseInMemoryDatabase("CustomersTest")
            .Options)
        {
            _logger = Substitute.For<ILogger<CustomerService>>();
        }

        [Fact]
        public async void RetrieveCustomers_WithSuccess()
        {
            //Arrage
            using (var context = new CustomersDbContext(_contextOptions))
            {
                _customerService = Substitute.For<CustomerService>(context, _logger);

                //Act
                var result = await _customerService.GetCustomers("123");

                //Assert
                result.Count.Should().Be(3);
                result[0].FirstName.Should().Be("Luke");
                result[1].DateOfBirth.Should().Be(new System.DateTime(1998-02-11));
            }
        }

        [Fact]
        public void AddCustomers_WithSuccess()
        {

            using (var context = new CustomersDbContext(_contextOptions))
            {
                //Arrage
                _customerService = Substitute.For<CustomerService>(context, _logger);
                var customer = new AddCustomerRequest
                {
                    FirstName = "ABC",
                    LastName = "XYZ",
                    DateOfBirth = new System.DateTime(2009-09-10)
                };
                //Act
                var result = _customerService.AddCustomer(customer,"123").Result;

                //Assert
                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public void SearchCustomers_WithGivenName_Success()
        {

            using (var context = new CustomersDbContext(_contextOptions))
            {
                //Arrage
                _customerService = Substitute.For<CustomerService>(context, _logger);
                var name = "Luke";
                //Act
                var result = _customerService.SearchCustomer(name, "123").Result;

                //Assert
                result.Count.Should().Be(1);
                result[0].FirstName.Should().Be(name);
                result[0].LastName.Should().Be("Leily");
            }
        }

        [Fact]
        public void SearchCustomers_WithGivenName_ReturnNoMatches()
        {

            using (var context = new CustomersDbContext(_contextOptions))
            {
                //Arrage
                _customerService = Substitute.For<CustomerService>(context, _logger);
                var name = "abc";
                //Act
                var result = _customerService.SearchCustomer(name, "123").Result;

                //Assert
                result.Count.Should().Be(0);               
            }
        }

        [Fact]
        public void UpdateCustomer_ForAGivenCustomerID_WithDetails_Success()
        {

            using (var context = new CustomersDbContext(_contextOptions))
            {
                //Arrage
                _customerService = Substitute.For<CustomerService>(context, _logger);
                var customer = new UpdateCustomerRequest
                {
                    Id = 1,
                    FirstName = "ABC",
                    LastName = "XYZ",
                    DateOfBirth = new System.DateTime(2009 - 09 - 10)
                };
                //Act
                var result = _customerService.UpdateCustomer(customer, "123");

                //Assert
                result.IsSuccess.Should().Be(true);
            }
        }

        [Fact]
        public void UpdateCustomer_WithInvalidCustomerID_WithDetails_Fail()
        {

            using (var context = new CustomersDbContext(_contextOptions))
            {
                //Arrage
                _customerService = Substitute.For<CustomerService>(context, _logger);
                var customer = new UpdateCustomerRequest
                {
                    Id = 10,
                    FirstName = "ABC",
                    LastName = "XYZ",
                    DateOfBirth = new System.DateTime(2009 - 09 - 10)
                };
                //Act
                var result = _customerService.UpdateCustomer(customer, "123");

                //Assert
                result.IsSuccess.Should().Be(false);
            }
        }
    }
}
