using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SrvsTool
{
    public class RecentFile
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public RecentFile(string filePath)
        {
            FilePath = filePath;
            Name = Path.GetFileNameWithoutExtension(filePath);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
