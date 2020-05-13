using System;
using System.IO;
using MPT.CSI.Serialize.Components;

namespace MPT.CSI.Serialize
{
    public static class Writer
    {
        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="tables">The tables.</param>
        /// <param name="writer">The writer.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WriteToFile(string directoryPath, Tables tables, IWriter writer)
        {
            try
            {
                string filePath = Path.Combine(directoryPath, tables.ModelName + tables.ModelExtension);
                File.WriteAllLines(
                    filePath, 
                    writer.ToArray(filePath, tables));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
