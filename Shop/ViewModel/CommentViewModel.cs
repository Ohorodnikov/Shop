using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PublishedDateTime { get; set; }

        public ICollection<CommentViewModel> Answers { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
