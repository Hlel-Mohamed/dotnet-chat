using dotnet_chat.DTOs;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace dotnet_chat.Controllers;

[Route("chat")]
[ApiController]
public class ChatController : Controller
{
    [HttpPost("message")]
    public async Task<ActionResult> Message(MessageDTO messageDTO) {
        var options = new PusherOptions
        {
            Cluster = "eu",
            Encrypted = true
        };

        var pusher = new Pusher(
            "1797901",
            "95e40c80d534b8b68d50",
            "58441db9f37b0e0a436f",
            options);

        await pusher.TriggerAsync(
            "chat",
            "message",
            new {
                username = messageDTO.username,
                message = messageDTO.message
            } );

        return Ok();
    }
}