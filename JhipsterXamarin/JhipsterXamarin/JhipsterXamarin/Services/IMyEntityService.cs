using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IMyEntityService<T>
    {
        Task<List<T>> GetEntities();
        Task<T> GetEntity(int? id);
        Task DeleteEntity(int? id);
        Task CreateEntity(T entity);
        Task UpdateEntity(T entity);
    }
}