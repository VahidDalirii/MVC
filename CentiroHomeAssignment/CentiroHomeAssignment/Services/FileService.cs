using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public class FileService
    {
        //Get all existing files from path
        public static List<string> GetFiles(string path)
        {
            return Directory.GetFiles(path).ToList();
        }
    }
}
