using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class ForumMessages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PublishedDateTime { get; set; }

        public int? ReplyToId { get; set; }
        public ForumMessages ReplyTo { get; set; } 
        public ICollection<ForumMessages> Answers { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
