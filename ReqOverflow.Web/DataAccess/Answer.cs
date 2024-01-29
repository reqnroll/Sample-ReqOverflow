using System;

namespace ReqOverflow.Web.DataAccess
{
    public class Answer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Content { get; set; }
        public int Votes { get; set; }

        public DateTime AnsweredAt { get; set; } = DateTime.Now;
        public Guid AnsweredBy { get; set; }
    }
}
