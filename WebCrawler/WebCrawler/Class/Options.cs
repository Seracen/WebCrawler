using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Class
{
    public class Options
    {
        public string url { get; set; }
        public string method { get; set; } = "BFS";
        public TimeSpan workTime { get; set; }
        public int depth { get; set; }
        public Options()
        {

        }
        public Options(string u, string m, TimeSpan w, int d)
        {
            url = u;
            method = m;
            workTime = w;
            depth = d;
        }
    }
}
