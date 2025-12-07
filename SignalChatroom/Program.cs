using SignalChatroom.Hubs;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddSignalR();

WebApplication app = builder.Build();

app.MapHub<ChatHub>("/chat");

app.Run();