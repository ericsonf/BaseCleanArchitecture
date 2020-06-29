using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCleanArchitecture.Core.Entities;
using BaseCleanArchitecture.Core.Interfaces;
using BaseCleanArchitecture.Core.Shared.Interfaces;

namespace BaseCleanArchitecture.UseCases
{
    public class UserUseCase : IUser
    {
        private readonly IRepository _repository;

        public UserUseCase(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> List()
        {
            return await _repository.ListAsync<User>();
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.GetByIdAsync<User>(id);
        }

        public async Task<User> Create(User user)
        {
            return await _repository.AddAsync(user);
        }

        public async Task<User> Update(User user)
        {
            return await _repository.UpdateAsync(user);
        }

        public async Task Delete(User user)
        {
            await _repository.DeleteAsync(user);
        }
    }
}
