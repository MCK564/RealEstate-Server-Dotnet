using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.buildings
{
    public interface IBuildingRepository
    { 
        Task<List<Building>> FindAllByNameLikeAsync(string name);
        Task<List<Building>> FindAllByStatusAsync(int status);
        Task<PaginatedList<Building>> FindAllAsync(int page, int pageSize);
        Task<List<Building>> FindByUserIdAsync(long userId);
        Task<List<Building>> FindTop30BuildingsWithMostLikesAsync();
        Task<List<Building>> FindAllByLocationAsync(string district, string ward, string street);
        Task<PaginatedList<Building>> FindAllWithFiltersAsync(BuildingSpecification spec, int page, int pageSize);
    }
}