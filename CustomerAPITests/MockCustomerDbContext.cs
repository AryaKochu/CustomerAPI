using CustomerAPI.DB;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPITests
{
    public class MockCustomerDbContext
    {

        protected DbContextOptions<CustomersDbContext> _contextOptions { get; }


        protected MockCustomerDbContext(DbContextOptions<CustomersDbContext> contextOptions)
        {
            _contextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new CustomersDbContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Add(new Customer
                {
                    CustomerId = 1,
                    FirstName = "Luke",
                    LastName = "Leily",
                    DateOfBirth = new System.DateTime(1990-12-01)
                });

               context.Add(new Customer
               {
                   CustomerId = 2,
                   FirstName = "Sara",
                   LastName = "Paul",
                   DateOfBirth = new System.DateTime(1998-02-11)
               });

                context.Add(new Customer
                {
                    CustomerId = 3,
                    FirstName = "Lewis",
                    LastName = "Lesly",
                    DateOfBirth = new System.DateTime(1990 - 12 - 01)
                });
                context.SaveChanges();
            }
        }

    }
}
