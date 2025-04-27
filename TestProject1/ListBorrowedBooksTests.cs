using Blazor_Lab_Starter_Code;
namespace TestProject1
{

    [TestClass]
    public class ListBorrowedBooksTests
    {
        [TestInitialize]
        public void Setup()
        {
            Program.books.Clear();
            Program.users.Clear();
            Program.borrowedBooks.Clear();
        }

        [TestMethod]
        public void ListBorrowedBooks_DisplaysCorrectOutput()
        {
            // Arrange
            var user = new User { Id = 3, Name = "Charlie" };
            var book = new Book { Id = 3, Title = "Sample Book", Author = "Author C", ISBN = "9999" };

            Program.borrowedBooks[user] = new List<Book> { book };

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Program.ListBorrowedBooks();

            // Assert
            string consoleOutput = output.ToString();
            Assert.IsTrue(consoleOutput.Contains("Charlie"));
            Assert.IsTrue(consoleOutput.Contains("Sample Book"));
        }

        [TestMethod]
        public void ListBorrowedBooks_NoBorrowedBooks_DisplaysNothing()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Program.ListBorrowedBooks();

            // Assert
            string consoleOutput = output.ToString();
            Assert.IsTrue(consoleOutput.Contains("Borrowed Books:"));
            Assert.IsFalse(consoleOutput.Contains("Dave"));
        }
    }
}