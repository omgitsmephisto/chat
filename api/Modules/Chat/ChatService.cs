namespace api.Modules.Chat;

public class ChatService
{
    
    private readonly List<Chat> _messages =
    [
        new(DateTime.UtcNow, "Olá mundo!")
    ];
    
    public List<Chat> GetMessages()
    {
        // foreach (var chat in _messages)
        // {
        //     Console.WriteLine(chat);
        // }
        return _messages;
    }

    public Chat Add(Chat chat)
    {
        _messages.Add(new(DateTime.UtcNow, chat.Content));
        return chat;
    }
}