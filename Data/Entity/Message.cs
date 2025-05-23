using System.ComponentModel.DataAnnotations;

namespace ChatApp.Data.Entity
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }

        public User Receiver { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }
    }

}
