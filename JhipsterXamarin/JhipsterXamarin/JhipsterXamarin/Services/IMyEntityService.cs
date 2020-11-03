using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IMyEntityService
    {
        Task<List<MyEntityModel>> GetEntities();
        Task<MyEntityModel> GetEntity(int v);
        Task CreateEntity(string name, int age);
        Task DeleteEntity(MyEntityModel currentElement);
        Task UpdateEntity(MyEntityModel currentElement);
    }
}