using Microsoft.AspNetCore.SignalR;

namespace api.Modules.Chat;

public class ChatService(
    IHubContext<ChatHub> hub
)
{
    private readonly IHubContext<ChatHub> _hub = hub;

    private readonly List<Chat> _messages =
    [
        new(DateTime.UtcNow, "[ADMIN] Chat para interagir.")
    ];
    
    public List<Chat> GetMessages()
    {
        return _messages;
    }

    public async Task<Chat> Add(Chat chat)
    {
        _messages.Add(new(DateTime.UtcNow, chat.Content));

        Console.WriteLine($"Chat adicionado: ID - {DateTime.UtcNow}, Conteúdo: {chat.Content}");

        await _hub.Clients.All.SendAsync(
            "messageReceived",
            chat
        );

        return chat;
    }
}