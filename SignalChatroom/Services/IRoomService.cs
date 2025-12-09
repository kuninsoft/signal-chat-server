namespace SignalChatroom.Services;

public interface IRoomService
{
    void JoinRoom(string roomName, string username);
    
    void ExitRoom(string roomName, string username);
    
    ICollection<string> ListRooms();
}