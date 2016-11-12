using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedReader
{
    public class FileReader : Reader
    {
        TextReader _reader;

        public FileReader(TextReader reader)
        {
            _reader = reader;
        }

        public override IEnumerator<Entry> GetEnumerator()
        {
            string line = null;
            while ((line = _reader.ReadLine()) != null)
                yield return Entry.Parse(line);
        }
    }
}
