using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Class
{
    public class Subsite
    {
        public enum SubsiteStatus
        {
            INITIAL,
            INPROGRESS,
            FINISHED,
            TIMEOUT,
            CANCELLED,
            FAILED,
            MAXDEPTHCANCELLED
        }
        public SubsiteStatus status { get; set; }
        public int levelOfDepth { get; set; }
        public string url { get; set; }
        public string body { get; set; }
        public List<Subsite> childList { get; set; } = new List<Subsite>();
        public Subsite parent { get; set; }

        public Subsite() { }
        public Subsite(int l, string u, Subsite p)
        {
            status = SubsiteStatus.INITIAL;
            levelOfDepth = l;
            url = u;
            parent = p;

        }
        
    }

}
