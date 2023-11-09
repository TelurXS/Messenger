using Application.Entities;
using Application.Infrastructure.Persistance;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Application.Tests
{
    public class DataContextTests : IDisposable
    {
        private ITestOutputHelper Output { get; set; }
        private Faker Faker { get; set; }
        private DataContext DataContext { get; set; }

        public DataContextTests(ITestOutputHelper output)
        {
            Output = output;
            Faker = new Faker();

            var builder = new DbContextOptionsBuilder()
                .UseSqlServer("Server=KOMPUTER\\SQLEXPRESS;Database=messagerdb;Trusted_Connection=True;TrustServerCertificate=True;");

            DataContext = new DataContext(builder.Options);

            DataContext.Database.EnsureCreated();
        }

        [Fact]
        public void InsertAccount_ShouldReturnAccount()
        {
            var account = new Account()
            {
                Name = Faker.Person.FullName,
                Email = Faker.Person.Email,
                Login = Faker.Person.UserName,
                Password = Faker.Random.Word(),
            };

            var created = DataContext.Accounts.Add(account).Entity;
            DataContext.SaveChanges();

            Assert.Equal(account.Name, created.Name);
            Assert.Equal(account.Email, created.Email);
            Assert.Equal(account.Login, created.Login);
            Assert.Equal(account.Password, created.Password);

            Output.WriteLine(created.Id.ToString());
            Output.WriteLine(created.Name);
            Output.WriteLine(created.Email);
            Output.WriteLine(created.Login);
            Output.WriteLine(created.Password); 
        }

        [Fact]
        public void UpdateAccount_ShouldEqual() 
        {
            var account = new Account()
            {
                Name = Faker.Person.FullName,
                Email = Faker.Person.Email,
                Login = Faker.Person.UserName,
                Password = Faker.Random.Word(),
            };

            var created = DataContext.Accounts.Add(account).Entity;
            DataContext.SaveChanges();
            var id = created.Id;

            created.Password = Faker.Random.Word();
            DataContext.SaveChanges();

            var updated = DataContext.Accounts.SingleOrDefault(x => x.Id == id);

            Assert.NotNull(updated);
            Assert.Equal(created.Password, updated.Password);
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

            var created = DataContext.Accounts.Add(account).Entity;
            DataContext.SaveChanges();
            var id = created.Id;

            DataContext.Accounts.Remove(created);
            DataContext.SaveChanges();

            var found = DataContext.Accounts.SingleOrDefault(x => x.Id == id);

            Assert.Null(found);
        }

        public void Dispose()
        {
            DataContext.Dispose();
        }
    }
}