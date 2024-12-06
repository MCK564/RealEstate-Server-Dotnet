namespace BuildingWebApi.Repositories
{
    using BuildingWebApi.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommunicationRepository : ICommunicationRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Communication>> FindByKeywordAsync(string keyword, int page, int pageSize)
        {
            var query = _context.Communications
                .Include(c => c.Building)
                .Include(c => c.BuyerRenter)
                .Include(c => c.Saler)
                .Where(c => EF.Functions.Like(c.Phone, $"%{keyword}%") ||
                            EF.Functions.Like(c.Note, $"%{keyword}%") ||
                            EF.Functions.Like(c.Building.Name, $"%{keyword}%") ||
                            EF.Functions.Like(c.BuyerRenter.FullName, $"%{keyword}%") ||
                            EF.Functions.Like(c.Saler.FullName, $"%{keyword}%"));

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PaginatedList<Communication>(items, totalCount, page, pageSize);
        }

        public async Task<PaginatedList<Communication>> FindByKeywordAndSalerOrBuyerRenterIdAsync(string keyword, int page, int pageSize, long salerId, long buyerRenterId)
        {
            var query = _context.Communications
                .Include(c => c.Building)
                .Include(c => c.BuyerRenter)
                .Include(c => c.Saler)
                .Where(c => (c.BuyerRenterId == buyerRenterId || c.SalerId == salerId) &&
                            (EF.Functions.Like(c.Phone, $"%{keyword}%") ||
                             EF.Functions.Like(c.Note, $"%{keyword}%") ||
                             EF.Functions.Like(c.Building.Name, $"%{keyword}%") ||
                             EF.Functions.Like(c.BuyerRenter.FullName, $"%{keyword}%") ||
                             EF.Functions.Like(c.Saler.FullName, $"%{keyword}%")));

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PaginatedList<Communication>(items, totalCount, page, pageSize);
        }

        public async Task<PaginatedList<Communication>> FindAllBySalerOrBuyerRenterIdAsync(long salerId, long buyerRenterId, string buildingName, int page, int pageSize)
        {
            var query = _context.Communications
                .Include(c => c.Building)
                .Where(c => (c.SalerId == salerId || c.BuyerRenterId == buyerRenterId || c.Building.Name == buildingName));

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PaginatedList<Communication>(items, totalCount, page, pageSize);
        }

        public async Task<List<Communication>> FindAllByBuildingIdAsync(long id)
        {
            return await _context.Communications
                .Where(c => c.BuildingId == id)
                .ToListAsync();
        }
    }
}
