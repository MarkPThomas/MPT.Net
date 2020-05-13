using System.Collections.Generic;
using MPT.CSI.API.Core.Support;
using MPT.CSI.Serialize.SAP2000;
using NUnit.Framework;

namespace MPT.CSI.Serialize.EndToEndTests
{
    [TestFixture]
    public class TablesTests
    {
        [Test]
        public void Intialize_Empty_Initialises_Empty()
        {
            Tables tables = new Tables();
            Assert.IsFalse(tables.HasTables);
            Assert.IsNullOrEmpty(tables.ModelExtension);
            Assert.IsNullOrEmpty(tables.ModelName);
            Assert.IsNullOrEmpty(tables.ProgramName);
        }

        [TestCase(@"D:\GitHub\personal\MPT.Net\MPT\CSI\API\MPT.CSI.Serialize.SAP2000\RCDF 2017 CFD Ex001.$2k", ".$2k", "RCDF 2017 CFD Ex001", "SAP2000")]
        [TestCase(@"RCDF 2004 CFD Ex003.$et", ".$et", "RCDF 2004 CFD Ex003", "ETABS")]
        public void Intialize_Valid_Path_Initialises_Valid_Properties(string path, string expectedExtension, string expectedModelName, string expectedProgramName)
        {
            Tables tables = new Tables(path);
            Assert.IsFalse(tables.HasTables);
            Assert.AreEqual(expectedExtension, tables.ModelExtension);
            Assert.AreEqual(expectedModelName, tables.ModelName);
            Assert.AreEqual(expectedProgramName, tables.ProgramName);
        }

        [Test]
        public void AddTable_Does_Not_Add_Null()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = null;

            Assert.IsFalse(tables.AddTable(null, null));
            Assert.IsFalse(tables.AddTable("Foo", null));

            values = new List<Dictionary<string, string>>();
            Assert.IsFalse(tables.AddTable("Foo", values));
        }

        [Test]
        public void AddTable_Does_Not_Add_Existing_Table()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            values.Add(tableRow);

            Assert.IsTrue(tables.AddTable("Foo", values));
            Assert.IsFalse(tables.AddTable("Foo", values));
        }

        [Test]
        public void AddTable_Adds_New_Existing_Table()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            values.Add(tableRow);

            List<Dictionary<string, string>> otherValues = new List<Dictionary<string, string>>();
            Dictionary<string, string> otherTableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            otherValues.Add(otherTableRow);

            Assert.IsTrue(tables.AddTable("Foo", values));
            Assert.IsFalse(tables.AddTable("Foo", otherValues));
            Assert.IsTrue(tables.AddTable("Bar", otherValues));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void RemoveTable_Does_Not_Remove_Null_Table(string tableName)
        {
            Tables tables = new Tables();
            Assert.IsFalse(tables.RemoveTable(tableName));
        }

        [Test]
        public void RemoveTable_Does_Not_Remove_Nonexisting_Table()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            values.Add(tableRow);
            tables.AddTable("Foo", values);

            List<Dictionary<string, string>> otherValues = new List<Dictionary<string, string>>();
            Dictionary<string, string> otherTableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            otherValues.Add(otherTableRow);
            tables.AddTable("Bar", otherValues);

            Assert.IsFalse(tables.RemoveTable("Moo"));
        }

        [Test]
        public void RemoveTable_Removes_Existing_Table()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            values.Add(tableRow);
            tables.AddTable("Foo", values);

            List<Dictionary<string, string>> otherValues = new List<Dictionary<string, string>>();
            Dictionary<string, string> otherTableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            otherValues.Add(otherTableRow);
            tables.AddTable("Bar", otherValues);

            Assert.IsTrue(tables.RemoveTable("Foo"));
            Assert.IsTrue(tables.HasTables);
        }

        [Test]
        public void RemoveTable_Sets_HasTable_To_False_When_Removing_Last_Table()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            values.Add(tableRow);
            tables.AddTable("Foo", values);

            List<Dictionary<string, string>> otherValues = new List<Dictionary<string, string>>();
            Dictionary<string, string> otherTableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            otherValues.Add(otherTableRow);
            tables.AddTable("Bar", otherValues);

            Assert.IsTrue(tables.RemoveTable("Foo"));
            Assert.IsTrue(tables.RemoveTable("Bar"));
            Assert.IsFalse(tables.HasTables);
        }

        // TODO: Mock an adaptor
        [Test]
        public void GenerateModelFromTables_Does_Not_Generate_Model_When_No_Tables_Exist()
        {
            Tables tables = new Tables();
            Assert.IsFalse(tables.GenerateModelFromTables(new SAP2000Adaptor()));
        }

        [Test]
        public void GenerateModelFromTables_Of_Unsupported_Table_Throws_Exception()
        {
            Tables tables = new Tables();
            List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            values.Add(tableRow);
            tables.AddTable("Foo", values);

            List<Dictionary<string, string>> otherValues = new List<Dictionary<string, string>>();
            Dictionary<string, string> otherTableRow = new Dictionary<string, string>
            {
                ["Foo"] = "Bar",
                ["Moo"] = "Nar"
            };
            otherValues.Add(otherTableRow);
            tables.AddTable("Bar", otherValues);

            Assert.That(() => tables.GenerateModelFromTables(new SAP2000Adaptor()),
                Throws.Exception
                    .TypeOf<CSiException>()
                    .With.Property("Message")
                    .EqualTo("Table Foo not currently supported."));
        }

        [Test]
        public void GenerateModelFromTables_Generates_Model()
        {
            Adaptor adaptor = new SAP2000Adaptor();
            string path = @"D:\GitHub\personal\MPT.Net\MPT\CSI\API\MPT.CSI.Serialize.SAP2000\RCDF 2017 CFD Ex001.$2k";
            Tables tables = Reader.ReadFile(path, adaptor);
            Assert.IsTrue(tables.GenerateModelFromTables(adaptor));
        }

        [Test]
        public void GenerateTablesFromModel__Does_Not_Generate_Tables_When_No_Model_Exists()
        {
            Tables tables = new Tables();
        }

        [Test]
        public void GenerateTablesFromModel__Generates_Tables()
        {
            Tables tables = new Tables();
        }
    }
}
