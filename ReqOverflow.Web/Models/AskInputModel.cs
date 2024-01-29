using System;

namespace ReqOverflow.Web.Models
{
    public class AskInputModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string[] Tags { get; set; }

        public override string ToString()
            => $"Title: {Title}, Body: {Body}, Title: {string.Join(",", Tags ?? new string[0])}";
    }
}
