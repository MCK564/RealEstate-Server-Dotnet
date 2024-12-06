using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.buildingImages
{
    public interface IBuildingImageRepository
    {
        Task<List<BuildingImage>> FindAllByBuildingIdAsync(long buildingId);
        Task DeleteAllByBuildingIdAsync(long buildingId);
    }
}