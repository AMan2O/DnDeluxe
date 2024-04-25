using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class FileReader : IDisposable
    {
        private StreamReader File;

        public static FileReader FromAbsolutePath(string path) => new()
        {
            File = new StreamReader(path),
        };

        public string? ReadLine() => File.ReadLine();

        public List<string> ReadAll()
        {
            var lines = new List<string>();

            for (var line = ReadLine(); line != null; line = ReadLine())
                lines.Add(line);

            return lines;
        }

        public void Dispose() => File.Dispose();
    }
}
