using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAngularSignalR.SignalRHubs
{
    public class NewsItem
    {
        public string Header { get; set; }
        public string NewsText { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }

        public string NewsGroup { get; set; }
    }
}
