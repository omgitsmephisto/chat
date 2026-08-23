using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace api.Modules.Chat;

[ApiController]
[Route("chat")]
public class ChatController(
    ChatService chatService,
    IHubContext<ChatHub> hub
) : ControllerBase
{
    private readonly ChatService _service = chatService;
    private readonly IHubContext<ChatHub> _hub = hub;

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetMessages());
    }

    [HttpPost]
    public async Task<IActionResult> Add(Chat chat)
    {
        _service.Add(chat);

        await _hub.Clients.All.SendAsync(
            "messageReceived",
            _service.GetMessages()
        );

        return Ok(chat);
    }
}