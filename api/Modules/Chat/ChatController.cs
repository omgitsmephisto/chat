using Microsoft.AspNetCore.Mvc;

namespace api.Modules.Chat;

[ApiController]
[Route("chat")]
public class ChatController(
    ChatService chatService
) : ControllerBase
{
    private readonly ChatService _service = chatService;

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetMessages());
    }

    [HttpPost]
    public async Task<IActionResult> Add(Chat chat)
    {
        var message = await _service.Add(chat);
        return Ok(message);
    }
}