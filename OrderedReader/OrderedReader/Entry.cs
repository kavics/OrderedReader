using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedReader
{
    [DebuggerDisplay("{Id} | {Time} | {Data}")]
    public class Entry
    {
        public int Id;
        public DateTime Time;
        public string Data;

        public static Entry Parse(string src)
        {
            var cols = src.Split('\t');
            return new Entry
            {
                Id = int.Parse(cols[0]),
                Time = DateTime.Parse(cols[1]),
                Data = cols[2]
            };
        }
    }
}
