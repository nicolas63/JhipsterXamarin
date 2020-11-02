using JhipsterXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public interface IMyEntityService
    {
        Task<List<MyEntityModel>> GetEntities();
        Task<MyEntityModel> GetEntity(int v);
    }
}
