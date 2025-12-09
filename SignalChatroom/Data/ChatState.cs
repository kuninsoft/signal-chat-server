using System.Collections.Concurrent;

namespace SignalChatroom.Data;

public class ChatState
{
    private readonly ConcurrentDictionary<string, ChatRoomState> _rooms = new();

    public ICollection<string> GetRoomNames()
        => _rooms.Keys;

    public ChatRoomState GetOrCreateRoom(string name)
        => _rooms.GetOrAdd(name, _ => new ChatRoomState());
    
    public bool TryRemoveRoom(string name)
        => _rooms.TryRemove(name, out _);
}

public class ChatRoomState
{
    public int UserCount;
    public readonly HashSet<string> Users = [];
}