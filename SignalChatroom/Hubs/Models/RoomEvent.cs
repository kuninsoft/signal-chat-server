namespace SignalChatroom.Hubs.Models;

public record RoomEvent(string Username, RoomEventType EventType);