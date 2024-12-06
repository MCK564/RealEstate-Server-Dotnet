using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.likes
{
    public class LoveRepository
    {
        
    namespace BuildingWebApi.Repositories
{
    using BuildingWebApi.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoveRepository : ILoveRepository
    {
        private readonly ApplicationDbContext _context;

        public LoveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Love>> FindAllByUserIdAsync(long userId)
        {
            return await _context.Loves
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Love>> FindAllByBuildingIdAsync(long buildingId)
        {
            return await _context.Loves
                .Where(l => l.BuildingId == buildingId)
                .ToListAsync();
        }

        public async Task<Love> FindByUserIdAndBuildingIdAsync(long userId, long buildingId)
        {
            return await _context.Loves
                .FirstOrDefaultAsync(l => l.UserId == userId && l.BuildingId == buildingId);
        }

        public async Task<bool> ExistsByUserIdAndBuildingIdAsync(long userId, long buildingId)
        {
            return await _context.Loves
                .AnyAsync(l => l.UserId == userId && l.BuildingId == buildingId);
        }
    }
}

    }
}