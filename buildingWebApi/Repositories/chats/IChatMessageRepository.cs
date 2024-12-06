using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories.chats
{
    public interface IChatMessageRepository
    {
         Task<List<ChatMessage>> FindByConversationIdAsync(long conversationId);
    }
}