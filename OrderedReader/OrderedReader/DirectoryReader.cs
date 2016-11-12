using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedReader
{
    public class DirectoryReader : Reader
    {
        IEnumerable<FileReader> _readers;

        public DirectoryReader(IEnumerable<FileReader> readersInOrder)
        {
            _readers = readersInOrder;
        }

        public override IEnumerator<Entry> GetEnumerator()
        {
            foreach (var reader in _readers)
                foreach (var entry in reader)
                    yield return entry;
        }
    }
}
