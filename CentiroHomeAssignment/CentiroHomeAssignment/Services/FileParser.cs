using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public class FileParser<T> where T: class, new() 
    {
        private readonly char _splitChar;
        private bool _skipHeader;
        private bool _skipFooter;
        private string _fileFormat;

        public FileParser(char splitChar, bool skipHeader, bool skipFooter, string fileFormat)
        {
            _splitChar = splitChar;
            _skipHeader = skipHeader;
            _skipFooter = skipFooter;
            _fileFormat = fileFormat;
        }

        public List<string> GetRows(string fileName, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var rows = new List<string>();
            try
            {
                if (_fileFormat.Equals("txt",StringComparison.InvariantCultureIgnoreCase))
                {
                    var stringBuilder = new StringBuilder();
                    var lines = File.ReadAllLines(fileName);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (_skipHeader && i == 0)
                            continue;

                        if (_skipFooter && i == lines.Length - 1)
                            continue;

                        var line = lines[i];
                        rows.Add(line);
                    }
                }
                if (_fileFormat.Equals("xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Future xml files 
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Parse error: {ex.Message}");
            }
            

            return rows;
        }    
        
        public List<string> GetSplitedRow(string row)
        {
            var splitedRow = row.Split(_splitChar).ToList();
            splitedRow.RemoveAt(0);
            splitedRow.RemoveAt(splitedRow.Count - 1);   //Remove both empty elements in begin and end of the list

            return splitedRow;
        }
    }
}
