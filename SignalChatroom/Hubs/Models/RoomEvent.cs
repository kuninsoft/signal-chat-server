namespace SignalChatroom.Hubs.Models;

// Should be handled on client — e.g. "<username> has <eventtype> the chat"
public record RoomEvent(string Username, RoomEventType EventType);

public enum RoomEventType
{
    Joined,
    Left
}