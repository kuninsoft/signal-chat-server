using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SignalChatroom.Data;

[Index(nameof(Name), IsUnique = true)]
public class Chatroom(string name)
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; } = name;

    public int UserCount = 0;
}