using System.Net;
using Microsoft.AspNetCore.SignalR;
using SignalChatroom.Hubs.Models;
using SignalChatroom.Services;

namespace SignalChatroom.Hubs;

public class ChatHub(IRoomService rooms, ILogger<ChatHub> logger) : Hub
{
    public async Task JoinRoom(RoomRequest request)
    {
        rooms.JoinRoom(request.Chatroom, request.Username);

        Context.Items["room"] = request.Chatroom;
        Context.Items["username"] = request.Username;
        
        await Groups.AddToGroupAsync(Context.ConnectionId, request.Chatroom);
        
        await Clients.Group(request.Chatroom).SendAsync("OnRoomEvent", 
            new RoomEvent(request.Username, RoomEventType.Joined));
    }

    public async Task LeaveRoom(RoomRequest request)
    {
        rooms.ExitRoom(request.Chatroom, request.Username);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, request.Chatroom);
        
        await Clients.Group(request.Chatroom).SendAsync("OnRoomEvent", 
            new RoomEvent(request.Username, RoomEventType.Left));
    }

    public async Task SendMessage(MessageRequest request)
    {
        await Clients.Group(request.Chatroom).SendAsync("ReceiveMessage",
            new MessageResponse(request.Username, request.Message));
    }
    
    public override async Task OnDisconnectedAsync(Exception ex)
    {
        if (Context.Items.TryGetValue("room", out object roomObj) &&
            Context.Items.TryGetValue("username", out object userObj))
        {
            var room = (string)roomObj;
            var username = (string)userObj;
            
            await LeaveRoom(new RoomRequest(username, room));
        }

        await base.OnDisconnectedAsync(ex);
    }
}