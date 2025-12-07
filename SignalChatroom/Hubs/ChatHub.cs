using System.Net;
using Microsoft.AspNetCore.SignalR;
using SignalChatroom.Data;
using SignalChatroom.Hubs.Models;

namespace SignalChatroom.Hubs;

public class ChatHub(ILogger logger) : Hub
{
    public async Task JoinRoom(RoomRequest request)
    {
        string safeChatroom = WebUtility.HtmlEncode(request.Chatroom);
        string safeUsername = WebUtility.HtmlEncode(request.Username);
        
        await using var db = new ApplicationContext();

        Chatroom chatroom = db.Chatrooms.FirstOrDefault(room => room.Name == safeChatroom);

        if (chatroom is null)
        {
            chatroom = new Chatroom(safeChatroom);
            
            db.Chatrooms.Add(chatroom);
            
            await db.SaveChangesAsync();
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, safeChatroom);

        chatroom.UserCount++;

        await db.SaveChangesAsync();
        
        await Clients.Group(safeChatroom).SendAsync("OnRoomEvent",
            new RoomEvent(safeUsername, RoomEventType.Joined));
            
        logger.LogInformation("User {username} joined chatroom {room}", safeUsername, safeChatroom);
    }

    public async Task LeaveRoom(RoomRequest request)
    {
        string safeChatroom = WebUtility.HtmlEncode(request.Chatroom);
        string safeUsername = WebUtility.HtmlEncode(request.Username);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, safeChatroom);

        await using (var db = new ApplicationContext())
        {
            if (db.Chatrooms.FirstOrDefault(room => room.Name == safeChatroom)
                is { } currentRoom)
            {
                currentRoom.UserCount--;

                if (currentRoom.UserCount < 1)
                {
                    db.Chatrooms.Remove(currentRoom);
                }

                await db.SaveChangesAsync();
            }
        }
        
        await Clients.Group(safeChatroom).SendAsync("OnRoomEvent", 
            new RoomEvent(safeUsername, RoomEventType.Left));
        
        logger.LogInformation("User {username} left chatroom {room}", safeUsername, safeChatroom);
    }

    public async Task SendMessage(MessageRequest request)
    {
        string safeMessage = WebUtility.HtmlEncode(request.Message);
        string safeUsername = WebUtility.HtmlEncode(request.Username);

        await Clients.Group(request.Chatroom).SendAsync("ReceiveMessage",
            new MessageResponse(safeUsername, safeMessage));
    }
}