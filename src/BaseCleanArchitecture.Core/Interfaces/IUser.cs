using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCleanArchitecture.Core.Entities;

namespace BaseCleanArchitecture.Core.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> List();

        Task<User> GetById(int id);

        Task<User> Create(User user);

        Task<User> Update(User user);

        Task Delete(User user);
    }
}
