using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buildingWebApi.Models;

namespace buildingWebApi.Services
{
    public interface IBuildingService
    {
        IEnumerable<BuildingEntity> GetAll();
        BuildingEntity GetById(long id);
        void Create(BuildingEntity building);
        void Update(BuildingEntity building);
        void Delete(long id);
    }
}
