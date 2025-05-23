using ChatApp.Models.Entities;

public class Message
{
    public int MessageId { get; set; }

    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int ReceiverId { get; set; }
    public User Receiver { get; set; }

    public string Content { get; set; }
    public DateTime Created { get; set; }
}