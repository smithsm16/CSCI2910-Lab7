using Blazor_Lab_Starter_Code;
namespace TestProject1
{


    [TestClass]
    public class ManageBooksTests
    {
        [TestInitialize]
        public void Setup()
        {
            Program.books = new List<Book>();
        }

        [TestMethod]
        public void ManageBooks_AddBook_AddsBookToList()
        {
            // Arrange
            string input = "1\nUnit Test Book\nUnit Tester\n0001112223334\n5\n";
            Console.SetIn(new StringReader(input));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.ManageBooks();

            // Assert
            Assert.AreEqual(1, Program.books.Count);
            Assert.AreEqual("Unit Test Book", Program.books[0].Title);
        }

        [TestMethod]
        public void ManageBooks_EditBook_UpdatesBookTitle()
        {
            // Arrange
            Program.books.Add(new Book { Id = 101, Title = "Old Title", Author = "Author", ISBN = "999" });

            string input = "2\n101\nNew Title\nNew Author\n1234567890\n5\n";
            Console.SetIn(new StringReader(input));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.ManageBooks();

            // Assert
            Assert.AreEqual("New Title", Program.books[0].Title);
            Assert.AreEqual("New Author", Program.books[0].Author);
            Assert.AreEqual("1234567890", Program.books[0].ISBN);
        }

        [TestMethod]
        public void ManageBooks_DeleteBook_RemovesBookFromList()
        {
            // Arrange
            Program.books.Add(new Book { Id = 102, Title = "To Delete", Author = "Author", ISBN = "123" });

            string input = "3\n102\n5\n";
            Console.SetIn(new StringReader(input));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.ManageBooks();

            // Assert
            Assert.AreEqual(0, Program.books.Count);
        }

        [TestMethod]
        public void ManageBooks_ListBooks_PrintsBookInfo()
        {
            // Arrange
            Program.books.Add(new Book { Id = 103, Title = "List Me", Author = "L. A.", ISBN = "456789" });

            string input = "4\n5\n";
            Console.SetIn(new StringReader(input));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.ManageBooks();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("List Me"));
            Assert.IsTrue(output.Contains("L. A."));
        }
    }
}
