using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buildingWebApi.Models;
using buildingWebApi.Repositories;

namespace buildingWebApi.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IGenericRepository<BuildingEntity> _buildingRepository;

        public BuildingService(IGenericRepository<BuildingEntity> repository)
        {
            _buildingRepository = repository;
        }

        public IEnumerable<BuildingEntity> GetAll()
        {
            return _buildingRepository.GetAll();
        }

        public BuildingEntity GetById(long id)
        {
            return _buildingRepository.GetById(id);
        }

        public void Create(BuildingEntity building)
        {
            _buildingRepository.Add(building);
        }

        public void Update(BuildingEntity building)
        {
            _buildingRepository.Update(building);
        }

        public void Delete(long id)
        {
            var existingBuilding = _buildingRepository.GetById(id);
            if (existingBuilding != null)
            {
                _buildingRepository.Delete(existingBuilding.Id);
            }
        }
    }
}