using Microsoft.EntityFrameworkCore;

namespace SignalChatroom.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Chatroom> Chatrooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ChatDB");
        
        Database.EnsureCreated();
    }
}