using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentiroHomeAssignment.Tests
{
    [TestClass]
    public class FileParseTests
    {
        [TestMethod]
        [DeploymentItem("TestFiles\\CsvOrders.txt")]
        public void GetRows_ReadTestFile_ShouldReturnAllFilesRows()
        {
            var parser = new FileParser<OrderRow>('|', true, false, "txt");
            var rows = parser.GetRows("TestFiles\\CsvOrders.txt", Encoding.UTF8);

            Assert.AreEqual(rows.Count, 4);
            Assert.IsTrue(rows[0].Contains("|Daniel Johansson|737268|"));
            Assert.IsTrue(rows[1].Contains("||0.15|Normal|"));
            Assert.IsTrue(rows[2].Contains("|2014-01-25|Per Johansson|737268|"));
            Assert.IsTrue(rows[3].Contains("|0004|4000-CAA|8|"));
        }

        [TestMethod]
        [DeploymentItem("TestFiles\\CsvOrders.txt")]
        public void GetSplitedRow_SampleRowToSplite_ShouldReturnSplitedRow()
        {
            var parser = new FileParser<OrderRow>('|', true, false, "txt");
            var rows = parser.GetRows("TestFiles\\CsvOrders.txt", Encoding.UTF8);

            var splitedRows = parser.GetSplitedRow(rows[0]);

            Assert.AreEqual(splitedRows.Count, 11);
            Assert.AreEqual(splitedRows[0], "17890");
            Assert.AreEqual(splitedRows[2], "123451324A");
            Assert.AreEqual(splitedRows[5], "Super awesome starfighter");
            Assert.AreEqual(splitedRows[10], "737268");
        }
    }


}
