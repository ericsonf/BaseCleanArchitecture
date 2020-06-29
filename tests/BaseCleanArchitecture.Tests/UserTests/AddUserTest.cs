using System.Linq;
using System.Threading.Tasks;
using BaseCleanArchitecture.Core.Entities;
using Xunit;

namespace BaseCleanArchitecture.Tests.UserTests
{
    public class AddUserTest : BaseEfRepoTest
    {
        [Fact]
        public async Task AddUser()
        {
            var repository = GetRepository();
            var item = new User()
            {
                Name = "Test",
                Email = "test@email.com"
            };

            await repository.AddAsync(item);

            var newItem = (await repository.ListAsync<User>()).FirstOrDefault();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id > 0);
        }
    }
}
