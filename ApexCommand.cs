using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexScheduler
{
    public class ApexCommand
    {
        public List<string> status { get; set; }
        public string name { get; set; }
        public string gid { get; set; }
        public string type { get; set; }
        public int ID { get; set; }
        public string did { get; set; }
    }
}