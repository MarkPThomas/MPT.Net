using System.IO;
using MPT.CSI.Serialize.Components;

namespace MPT.CSI.Serialize
{
    public static class Reader
    {
        public static Tables ReadFile(string filePath, IReader reader)
        {
            if (!File.Exists(filePath)) return new Tables();

            Tables tables = new Tables(Path.GetFileName(filePath));
            var lines = File.ReadLines(filePath);
            
            return reader.ReadFile(tables, lines);
        }
    }
}
