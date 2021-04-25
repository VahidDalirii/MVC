using CentiroHomeAssignment.Database;
using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    public class FileRepository : IFileRepository
    {
        readonly static FilesDb _db = new FilesDb();
        public void AddFileToDb(FileModel file)
        {
            _db.AddFileToDb(file);
        }

        public List<FileModel> GetFiles()
        {
            return _db.GetFiles();
        }
    }
}
