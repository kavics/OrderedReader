using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedReader;

namespace OrderedReaderTests
{
    [TestClass]
    public class EntryTests
    {
        [TestMethod]
        public void Entry_Parse()
        {
            var entry = Entry.Parse("42\t2016-06-12 11:12:13.12345\tASDF");
            Assert.AreEqual(42, entry.Id);
            Assert.AreEqual(DateTime.Parse("2016-06-12 11:12:13.12345"), entry.Time);
            Assert.AreEqual("ASDF", entry.Data);
        }
    }
}
