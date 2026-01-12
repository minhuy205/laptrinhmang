using Microsoft.AspNetCore.SignalR;
using ChatAppApi.Data;
using ChatAppApi.Models;
using System;
using System.Threading.Tasks;

namespace ChatAppApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageRealTime(string user1, string user2, string message)
        {
            var msg = new Message
            {
                SenderUsername = user1,
                ReceiverUsername = user2,
                Content = message,
                SentAt = DateTime.Now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user1, message, msg.SentAt);
        }
    }
}
