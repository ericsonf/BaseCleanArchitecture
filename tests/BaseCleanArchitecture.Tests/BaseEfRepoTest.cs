using BaseCleanArchitecture.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCleanArchitecture.Tests
{
    public class BaseEfRepoTest
    {
        protected AppContext _dbContext;

        protected static DbContextOptions<AppContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppContext>();
            builder.UseInMemoryDatabase("basecleanarchitecture")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected AppRepository GetRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new AppContext(options);
            return new AppRepository(_dbContext);
        }
    }
}
