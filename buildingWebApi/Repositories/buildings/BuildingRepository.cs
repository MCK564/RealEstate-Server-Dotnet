using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.buildings
{
    public class BuildingRepository
    {
        private readonly ApplicationDbContext _context;

    public BuildingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Building>> FindAllByNameLikeAsync(string name)
    {
        return await _context.Buildings
            .Where(b => b.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<List<Building>> FindAllByStatusAsync(int status)
    {
        return await _context.Buildings
            .Where(b => b.Status == status)
            .ToListAsync();
    }

    public async Task<PaginatedList<Building>> FindAllAsync(int page, int pageSize)
    {
        var query = _context.Buildings.AsQueryable();
        var count = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PaginatedList<Building>(items, count, page, pageSize);
    }

    public async Task<List<Building>> FindByUserIdAsync(long userId)
    {
        return await _context.Buildings
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Building>> FindTop30BuildingsWithMostLikesAsync()
    {
        return await _context.Buildings
            .OrderByDescending(b => b.Likes)
            .Take(30)
            .ToListAsync();
    }

    public async Task<List<Building>> FindAllByLocationAsync(string district, string ward, string street)
    {
        return await _context.Buildings
            .Where(b => b.District == district && b.Ward == ward && b.Street == street)
            .ToListAsync();
    }

    public async Task<PaginatedList<Building>> FindAllWithFiltersAsync(BuildingSpecification spec, int page, int pageSize)
    {
        var query = _context.Buildings.AsQueryable();

        // Apply filters from BuildingSpecification
        if (!string.IsNullOrEmpty(spec.Name))
        {
            query = query.Where(b => b.Name.Contains(spec.Name));
        }
        if (spec.Status.HasValue)
        {
            query = query.Where(b => b.Status == spec.Status);
        }
        if (!string.IsNullOrEmpty(spec.Location))
        {
            query = query.Where(b => b.Location.Contains(spec.Location));
        }

        var count = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PaginatedList<Building>(items, count, page, pageSize);
    }
    }
}