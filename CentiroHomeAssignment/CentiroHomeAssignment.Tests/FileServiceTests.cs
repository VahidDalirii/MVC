using CentiroHomeAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentiroHomeAssignment.Tests
{
    [TestClass]
    public class FileServiceTests
    {
        [TestMethod]
        public void GetFiles_SendPathToTestFiles_ShouldGetAllFilesInPath()
        {
            var path = "TestFiles\\";
            var files = FileService.GetFiles(path);

            Assert.AreEqual(files.Count, 3);
            Assert.AreEqual(files[0], "TestFiles\\CsvOrders.txt");
            Assert.AreEqual(files[2], "TestFiles\\CsvOrders2.txt");
        }

    }
}
