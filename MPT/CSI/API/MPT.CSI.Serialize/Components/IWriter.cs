using System.Collections.Generic;
using MPT.CSI.Serialize.Models;

namespace MPT.CSI.Serialize.Components
{
    public interface IWriter
    {
        string FileExtension { get; }

        string[] ToArray(string filePath, Tables tables);

        Dictionary<string, List<Dictionary<string, string>>> FillTablesFromModel(Model model);
    }
}
