namespace BuildingWebApi.Repositories
{
    using BuildingWebApi.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> FindBySenderAndReceiverIdAsync(long senderId, long receiverId)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c => c.SenderId == senderId && c.ReceiverId == receiverId);
        }

        public async Task<Conversation> FindBySenderOrReceiverIdAsync(long senderId, long receiverId)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c => c.SenderId == senderId || c.ReceiverId == receiverId);
        }

        public async Task<bool> ExistsBySenderAndReceiverIdAsync(long senderId, long receiverId)
        {
            return await _context.Conversations
                .AnyAsync(c => c.SenderId == senderId && c.ReceiverId == receiverId);
        }

        public async Task<List<Conversation>> FindAllBySenderOrReceiverIdAsync(long senderId, long receiverId)
        {
            return await _context.Conversations
                .Where(c => c.SenderId == senderId || c.ReceiverId == receiverId)
                .ToListAsync();
        }
    }
}
