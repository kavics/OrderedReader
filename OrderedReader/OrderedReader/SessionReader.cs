using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedReader
{
    public class SessionReader : Reader
    {
        List<Reader> _readers;

        public SessionReader(IEnumerable<Reader> perAppDomainReaders)
        {
            _readers = perAppDomainReaders.ToList();
        }

        public override IEnumerator<Entry> GetEnumerator()
        {
            return new SessionReaderEnumerator(_readers);
            //UNDONE: throw new NotImplementedException();
            throw new NotImplementedException();
        }
    }
}
