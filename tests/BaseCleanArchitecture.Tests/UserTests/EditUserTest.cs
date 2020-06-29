using System.Linq;
using System.Threading.Tasks;
using BaseCleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BaseCleanArchitecture.Tests.UserTests
{
    public class EditUserTest : BaseEfRepoTest
    {
        [Fact]
        public async Task UpdatesItemAfterAddingIt()
        {
            var repository = GetRepository();
            var initialEmail = "test_email@email.com";
            var item = new User()
            {
                Name = "Test Email",
                Email = initialEmail
            };

            await repository.AddAsync(item);

            _dbContext.Entry(item).State = EntityState.Detached;

            var newItem = (await repository.ListAsync<User>())
                .FirstOrDefault(i => i.Email == initialEmail);
            Assert.NotNull(newItem);
            Assert.NotSame(item, newItem);
            var newEmail = "new_test_email@email.com";
            newItem.Email = newEmail;

            await repository.UpdateAsync(newItem);
            var updatedItem = (await repository.ListAsync<User>())
                .FirstOrDefault(i => i.Email == newEmail);

            Assert.NotNull(updatedItem);
            Assert.NotEqual(item.Email, updatedItem.Email);
            Assert.Equal(newItem.Id, updatedItem.Id);
        }
    }
}
