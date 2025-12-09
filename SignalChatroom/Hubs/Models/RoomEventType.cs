using System.Text.Json.Serialization;

namespace SignalChatroom.Hubs.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomEventType
{
    Joined,
    Left
}