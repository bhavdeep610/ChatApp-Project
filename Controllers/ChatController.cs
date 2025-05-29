using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ChatController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] MessageDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
            return BadRequest("Message content is required.");

        int senderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var message = new Message
        {
            SenderId = senderId,
            ReceiverId = dto.ReceiverID,
            Content = dto.Content,
            Created = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return Ok(message);
    }

    [HttpGet("get/{receiverId}")]
    public async Task<IActionResult> GetMessages(int receiverId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var messages = await _context.Messages
            .Where(m => (m.SenderId == userId && m.ReceiverId == receiverId) ||
                        (m.SenderId == receiverId && m.ReceiverId == userId))
            .OrderBy(m => m.Created)
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateMessage(int id, [FromBody] UpdateMessage dto)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var message = await _context.Messages.FindAsync(id);

        if (message == null)
            return NotFound();

        if (message.SenderId != userId)
            return Forbid();

        message.Content = dto.NewContent;
        await _context.SaveChangesAsync();

        return Ok(message);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var message = await _context.Messages.FindAsync(id);

        if (message == null)
            return NotFound();

        if (message.SenderId != userId)
            return Forbid();

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var users = await _context.Users
            .Where(u => u.Id != currentUserId) // exclude self
            .Select(u => new
            {
                u.Id,
                u.UserName
            })
            .ToListAsync();

        return Ok(users);
    }
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
    {
        try
        {
            var messages = await _context.Messages
                .OrderByDescending(m => m.Created)
                .ToListAsync();

            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }

}