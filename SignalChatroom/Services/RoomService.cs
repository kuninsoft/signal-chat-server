using SignalChatroom.Data;

namespace SignalChatroom.Services;

public class RoomService(ChatState chat, ILogger<RoomService> logger) : IRoomService
{
    public void JoinRoom(string roomName, string username)
    {
        ChatRoomState room = chat.GetOrCreateRoom(roomName);

        lock (room)
        {
            room.UserCount++;
            room.Users.Add(username);
        }
    }

    public void ExitRoom(string roomName, string username)
    {
        ChatRoomState room = chat.GetOrCreateRoom(roomName);

        lock (room)
        {
            room.UserCount--;
            room.Users.Remove(username);

            if (room.UserCount >= 1) return;
            
            chat.TryRemoveRoom(roomName);
            
            logger.LogInformation("Removed room {room}", roomName);
        }
    }

    public ICollection<string> ListRooms() => chat.GetRoomNames();
}