using System.Collections.Generic;

namespace dowd.Models
{
    public class MessageModel
    {
        public IEnumerable<DataModel> rows { get; set; }
        public string image { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
    }

    public class DataModel
    {
        public string data { get; set; }
    }
}