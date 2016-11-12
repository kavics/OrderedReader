using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedReaderTests
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        public void Reader_FileReader()
        {
            var sb = new StringBuilder();
            sb.AppendLine("111\t2016-06-12 11:12:13.001\tASDF-1");
            sb.AppendLine("112\t2016-06-12 11:12:13.002\tASDF-2");
            sb.AppendLine("113\t2016-06-12 11:12:13.003\tASDF-3");
            var log = sb.ToString();

            var entries = new List<Entry>();

            using (var reader = new FileReader(new StringReader(log)))
                foreach (var entry in reader)
                    entries.Add(entry);

            Assert.AreEqual(3, entries.Count);
            Assert.AreEqual(111, entries[0].Id);
            Assert.AreEqual(112, entries[1].Id);
            Assert.AreEqual(113, entries[2].Id);
        }

        [TestMethod]
        public void Reader_DirectoryReader()
        {
            var readers = new[] {
                GetFileReader(new[] {
                "111\t2016-06-12 11:12:13.001\tASDF-1",
                "112\t2016-06-12 11:12:13.002\tASDF-2",
                "113\t2016-06-12 11:12:13.003\tASDF-3" }),
                GetFileReader(new[] {
                "114\t2016-06-12 11:12:13.004\tASDF-4",
                "115\t2016-06-12 11:12:13.005\tASDF-5",
                "116\t2016-06-12 11:12:13.006\tASDF-6" }),
                GetFileReader(new[] {
                "117\t2016-06-12 11:12:13.007\tASDF-7",
                "118\t2016-06-12 11:12:13.008\tASDF-8",
                "119\t2016-06-12 11:12:13.009\tASDF-9" }) };

            var entries = new List<Entry>();

            using (var reader = new DirectoryReader(readers))
                foreach (var entry in reader)
                    entries.Add(entry);

            Assert.AreEqual(9, entries.Count);
            for (int i = 1; i < entries.Count; i++)
            {
                Assert.AreEqual(entries[i - 1].Id + 1, entries[i].Id);
                Assert.IsTrue(entries[i - 1].Time < entries[i].Time);
            }
        }

        [TestMethod]
        public void Reader_SessionReader()
        {
            var readers = new[] {
                new DirectoryReader(new[] {
                    GetFileReader(new[] {
                        "111\t2016-06-12 11:12:13.001\tASDF-1",
                        "112\t2016-06-12 11:12:13.002\tASDF-2",
                        "113\t2016-06-12 11:12:13.003\tASDF-3" }),
                    GetFileReader(new[] {
                        "114\t2016-06-12 11:12:13.004\tASDF-4",
                        "115\t2016-06-12 11:12:13.005\tASDF-5",
                        "116\t2016-06-12 11:12:13.006\tASDF-6" }),
                    GetFileReader(new[] {
                        "117\t2016-06-12 11:12:13.007\tASDF-7",
                        "118\t2016-06-12 11:12:13.008\tASDF-8",
                        "119\t2016-06-12 11:12:13.009\tASDF-9" }) }),
                new DirectoryReader(new[] {
                    GetFileReader(new[] {
                        "111\t2016-06-12 11:12:13.0015\tQWER-1",
                        "112\t2016-06-12 11:12:13.0025\tQWER-2",
                        "113\t2016-06-12 11:12:13.0035\tQWER-3" }),
                    GetFileReader(new[] {
                        "114\t2016-06-12 11:12:13.0045\tQWER-4",
                        "115\t2016-06-12 11:12:13.0055\tQWER-5",
                        "116\t2016-06-12 11:12:13.0065\tQWER-6" }),
                    GetFileReader(new[] {
                        "117\t2016-06-12 11:12:13.0075\tQWER-7",
                        "118\t2016-06-12 11:12:13.0085\tQWER-8",
                        "119\t2016-06-12 11:12:13.0095\tQWER-9" }) }),
                new DirectoryReader(new[] {
                    GetFileReader(new[] {
                        "111\t2016-06-12 11:12:13.0017\tYXCV-1",
                        "112\t2016-06-12 11:12:13.0027\tYXCV-2",
                        "113\t2016-06-12 11:12:13.0037\tYXCV-3" }),
                    GetFileReader(new[] {
                        "114\t2016-06-12 11:12:13.0047\tYXCV-4",
                        "115\t2016-06-12 11:12:13.0057\tYXCV-5",
                        "116\t2016-06-12 11:12:13.0067\tYXCV-6" }),
                    GetFileReader(new[] {
                        "117\t2016-06-12 11:12:13.0077\tYXCV-7",
                        "118\t2016-06-12 11:12:13.0087\tYXCV-8",
                        "119\t2016-06-12 11:12:13.0097\tYXCV-9" }) })
            };

            var entries = new List<Entry>();

            using (var reader = new SessionReader(readers))
                foreach (var entry in reader)
                    entries.Add(entry);

            Assert.AreEqual(27, entries.Count);
            for (int i = 1; i < entries.Count; i++)
                Assert.IsTrue(entries[i - 1].Time < entries[i].Time);
        }


        private FileReader GetFileReader(string[] lines)
        {
            var sb = new StringBuilder();
            foreach(var line in lines)
                sb.AppendLine(line);
            return new FileReader(new StringReader(sb.ToString()));
        }

    }
}
