using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimInfoUpload
{
    public class SimInfo
    {
        public string num { get; set; }
        public string ccid { get; set; }
        public string description { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Sim2
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Sim2 sim { get; set; }
    }

    public class Sim
    {
        public string number { get; set; }
        public string ccid { get; set; }
        public string description { get; set; }
        public string opTime { get; set; }
        public Links _links { get; set; }
    }

    public class Embedded
    {
        public List<Sim> sim { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self2 self { get; set; }
    }

    public class RootObject
    {
        public Embedded _embedded { get; set; }
        public Links2 _links { get; set; }
    }


}
