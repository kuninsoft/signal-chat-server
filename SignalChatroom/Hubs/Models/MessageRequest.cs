namespace SignalChatroom.Hubs.Models;

public record MessageRequest(string Username, string Chatroom, string Message);