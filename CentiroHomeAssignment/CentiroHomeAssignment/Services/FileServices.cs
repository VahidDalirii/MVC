using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public static class FileServices
    {
        public static List<string> GetCsvFiles(string path)
        {
            return Directory.GetFiles(path, "*.txt",  SearchOption.AllDirectories).ToList();
        }
    }
}
