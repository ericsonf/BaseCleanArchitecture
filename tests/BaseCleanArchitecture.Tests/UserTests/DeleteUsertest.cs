using System.Threading.Tasks;
using BaseCleanArchitecture.Core.Entities;
using Xunit;

namespace BaseCleanArchitecture.Tests.UserTests
{
    public class DeleteUserTest : BaseEfRepoTest
    {
        [Fact]
        public async Task DeleteUser()
        {
            var repository = GetRepository();
            const string initialName = "user_to_delete"; 
            
            var item = new User()
            {
                Name = initialName,
                Email = "test@email.com"
            };
            await repository.AddAsync(item);

            // delete the item
            await repository.DeleteAsync(item);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync<User>(),
                i => i.Name == initialName);
        }
    }
}
