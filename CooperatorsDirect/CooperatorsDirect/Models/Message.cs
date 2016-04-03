using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public DateTime SentTime { get; set; }
        public string Content { get; set; }
    }
}