using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.chats
{
    public class ChatMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatMessage>> FindByConversationIdAsync(long conversationId)
        {
            return await _context.ChatMessages
                .Where(cm => cm.ConversationId == conversationId)
                .ToListAsync();
        }
    }
}