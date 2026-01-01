using System;
using System.Collections.Generic;
using System.Text;

namespace Usta.Domain.Core.CommentAgg.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public bool IsApproved { get; set; }
    }
}