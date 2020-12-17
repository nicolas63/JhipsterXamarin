using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IAbstractEntityService<T>
    {
        Task<List<T>> GetEntities();
        Task<T> GetEntity(long id);
        Task DeleteEntity(long id);
        Task CreateEntity(T entity);
        Task UpdateEntity(T entity);
    }
}