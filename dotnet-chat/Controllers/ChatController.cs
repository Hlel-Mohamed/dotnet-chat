using dotnet_chat.DTOs;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace dotnet_chat.Controllers
{
    /// <summary>
    /// ChatController is a controller class that handles chat related requests.
    /// </summary>
    [Route("chat")]
    [ApiController]
    public class ChatController : Controller
    {
        /// <summary>
        /// Message is an action method that handles HTTP POST requests.
        /// It triggers a 'message' event on a 'chat' channel using the Pusher service.
        /// </summary>
        /// <param name="messageDto">The data transfer object containing the message details.</param>
        /// <returns>An ActionResult that represents the result of the action method.</returns>
        [HttpPost("message")]
        public async Task<ActionResult> Message(MessageDTO messageDto) 
        {
            // Create a new PusherOptions object with the specified options.
            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };

            // Create a new Pusher object with the specified app ID, key, secret, and options.
            var pusher = new Pusher(
                "1797901",
                "95e40c80d534b8b68d50",
                "58441db9f37b0e0a436f",
                options);

            // Trigger a 'message' event on a 'chat' channel with the specified data.
            await pusher.TriggerAsync(
                "chat",
                "message",
                new {
                    username = messageDto.username,
                    message = messageDto.message
                } );

            // Return a 200 OK response.
            return Ok();
        }
    }
}