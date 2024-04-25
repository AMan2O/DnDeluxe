using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class FileWriter : IDisposable
    {
        public StreamWriter File;

        public static FileWriter FromAbsolutePath(string path) => new()
        {
            File = new StreamWriter(path)
        };

        public void WriteLine(string text) => File.WriteLine(text);

        public void Write(string text) => File.Write(text);

        public void Dispose() => File.Dispose();
    }
}
