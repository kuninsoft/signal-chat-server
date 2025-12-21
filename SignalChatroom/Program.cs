using SignalChatroom.Data;
using SignalChatroom.Hubs;
using SignalChatroom.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddSignalR();

builder.Services.AddSingleton<ChatState>();
builder.Services.AddSingleton<IRoomService, RoomService>();

WebApplication app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");

app.MapGet("/rooms", (IRoomService roomService) => Results.Ok(roomService.ListRooms()));

app.Run();