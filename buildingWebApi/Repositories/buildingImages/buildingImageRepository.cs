using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.buildingImages
{
    public class buildingImageRepositoryn:: IBuildingImageRepository
    {
        private readonly ApplicationDbContext _context;

        public BuildingImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BuildingImage>> FindAllByBuildingIdAsync(long buildingId)
        {
            return await _context.BuildingImages
                .Where(b => b.BuildingId == buildingId)
                .ToListAsync();
        }

        public async Task DeleteAllByBuildingIdAsync(long buildingId)
        {
            var images = _context.BuildingImages
                .Where(b => b.BuildingId == buildingId);

            _context.BuildingImages.RemoveRange(images);
            await _context.SaveChangesAsync();
        }
    }
}