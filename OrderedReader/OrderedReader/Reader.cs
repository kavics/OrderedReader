using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedReader
{
    public abstract class Reader : IEnumerable<Entry>, IDisposable
    {
        public void Dispose()
        {
            //UNDONE: Dispose chain
        }

        public abstract IEnumerator<Entry> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
