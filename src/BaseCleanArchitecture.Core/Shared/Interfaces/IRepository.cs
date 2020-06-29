using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCleanArchitecture.Core.Shared.Classes;

namespace BaseCleanArchitecture.Core.Shared.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;

        Task<List<T>> ListAsync<T>() where T : BaseEntity;

        Task<T> AddAsync<T>(T entity) where T : BaseEntity;

        Task<T> UpdateAsync<T>(T entity) where T : BaseEntity;

        Task DeleteAsync<T>(T entity) where T : BaseEntity;
    }
}
