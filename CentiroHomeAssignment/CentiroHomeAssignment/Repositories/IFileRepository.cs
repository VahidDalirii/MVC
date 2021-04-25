using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Repositories
{
    public interface IFileRepository
    {
        void AddFileToDb(FileModel file);
        List<FileModel> GetFiles();
    }
}
