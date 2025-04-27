namespace TestProject1
{
    [TestClass]
    public class ReadBooksTests
    {
        [TestInitialize]
        public void Init()
        {
            Program.books.Clear();
            Directory.CreateDirectory("./Data");
        }

        [TestMethod]
        public void ReadBooks_ValidCSV_AddsBooks()
        {
            // Arrange
            File.WriteAllLines("./Data/Books.csv", new[]
            {
            "1,Book A,Author A,ISBN1",
            "2,Book B,Author B,ISBN2"
        });

            // Act
            Program.ReadBooks();

            // Assert
            Assert.AreEqual(2, Program.books.Count);
            Assert.AreEqual("Book A", Program.books[0].Title);
        }

        [TestMethod]
        public void ReadBooks_InvalidLine_SkipsLine()
        {
            // Arrange
            File.WriteAllLines("./Data/Books.csv", new[]
            {
            "Incomplete,Line",
            "3,Book C,Author C,ISBN3"
        });

            // Act
            Program.ReadBooks();

            // Assert
            Assert.AreEqual(1, Program.books.Count);
            Assert.AreEqual("Book C", Program.books[0].Title);
        }

        [TestMethod]
        public void ReadBooks_EmptyFile_AddsNothing()
        {
            // Arrange
            File.WriteAllText("./Data/Books.csv", "");

            // Act
            Program.ReadBooks();

            // Assert
            Assert.AreEqual(0, Program.books.Count);
        }

        [TestMethod]
        public void ReadBooks_FileDoesNotExist_HandlesException()
        {
            // Arrange
            if (File.Exists("./Data/Books.csv"))
                File.Delete("./Data/Books.csv");

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.ReadBooks();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("An error occurred"));
        }
    }
}
