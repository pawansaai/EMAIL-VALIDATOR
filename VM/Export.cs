using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VM
{
    internal class Export
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public static void WriteToFileThreadSafe(string text, string path)
        {
            Export._readWriteLock.EnterWriteLock();
            try
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(text);
                    streamWriter.Close();
                }
            }
            finally
            {
                Export._readWriteLock.ExitWriteLock();
            }
        }
    }
}
