using Application.Common.Interfaces.Persistance;
using Application.Entities;
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Persistance.Repositories;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Application.Tests
{
    public class AccountRepositoryTests : IDisposable
    {
        private ITestOutputHelper Output { get; set; }
        private Faker Faker { get; set; }
        private DataContext DataContext { get; set; }

        private IAccountRepository Repository { get; set; }

        public AccountRepositoryTests(ITestOutputHelper output)
        {
            Output = output;
            Faker = new Faker();

            var builder = new DbContextOptionsBuilder()
                .UseSqlServer("Server=DESKTOP-43HN7TU;Database=messagerdb;Trusted_Connection=True;TrustServerCertificate=True;");

            DataContext = new DataContext(builder.Options);

            DataContext.Database.EnsureCreated();

            Repository = new AccountRepository(DataContext);
        }

        [Fact]
        public void CreateAccount_ShouldReturnAccount() 
        {
            var account = new Account()
            {
                Name = Faker.Person.FullName,
                Email = Faker.Person.Email,
                Login = Faker.Person.UserName,
                Password = Faker.Random.Word(),
            };

            var created = Repository.Insert(account);

            Assert.Equal(account.Name, created.Name);
            Assert.Equal(account.Email, created.Email);
            Assert.Equal(account.Login, created.Login);
            Assert.Equal(account.Password, created.Password);
            
            //created.Email

            Output.WriteLine(created.Id.ToString());
        }

        [Fact]
        public void UpdateAccount_ShouldBeEqual()
        {
            var account = new Account()
            {
                Name = Faker.Person.FullName,
                Email = Faker.Person.Email,
                Login = Faker.Person.UserName,
                Password = Faker.Random.Word(),
            };

            var created = Repository.Insert(account);
            var id = created.Id;

            created.Password = Faker.Random.Word();

            var updated = Repository.Update(id, created);

            Assert.NotNull(updated);
            Assert.Equal(created.Password, updated.Password);

            Output.WriteLine(created.Id.ToString());
        }

        [Fact]
        public void DeleteAccount_ShouldBeNull()
        {
            var account = new Account()
            {
                Name = Faker.Person.FullName,
                Email = Faker.Person.Email,
                Login = Faker.Person.UserName,
                Password = Faker.Random.Word(),
            };

            var created = Repository.Insert(account);

            var result = Repository.Delete(created);

            Assert.True(result);

            Output.WriteLine(created.Id.ToString());
        }

        public void Dispose()
        {
            DataContext.Database.EnsureDeleted();
            DataContext.Dispose();
        }
    }
}
