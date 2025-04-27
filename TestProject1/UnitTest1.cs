using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace TestProject1
{
    [TestClass]
    public class ReturnBookTests
    {
        [TestInitialize]
        public void Setup()
        {
            Program.books.Clear();
            Program.users.Clear();
            Program.borrowedBooks.Clear();
        }

        [TestMethod]
        public void ReturnBook_SuccessfullyReturnsBook()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Alice" };
            var book = new Book { Id = 1, Title = "Test Book", Author = "Author A", ISBN = "1234" };

            Program.users.Add(user);
            Program.borrowedBooks[user] = new List<Book> { book };

            var input = new StringReader("1\n1\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.ReturnBook();

            // Assert
            Assert.IsTrue(Program.books.Contains(book));
            Assert.IsFalse(Program.borrowedBooks[user].Contains(book));
            Assert.IsTrue(output.ToString().Contains("Book returned successfully"));
        }

        [TestMethod]
        public void ReturnBook_InvalidUserId_ShowsError()
        {
            // Arrange
            var input = new StringReader("abc\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.ReturnBook();

            // Assert
            Assert.IsTrue(output.ToString().Contains("Invalid input"));
        }

        [TestMethod]
        public void ReturnBook_UserNotFound_ShowsError()
        {
            // Arrange
            var input = new StringReader("999\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.ReturnBook();

            // Assert
            Assert.IsTrue(output.ToString().Contains("User not found or no borrowed books"));
        }

        [TestMethod]
        public void ReturnBook_InvalidBookNumber_ShowsError()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Bob" };
            var book = new Book { Id = 2, Title = "Another Book", Author = "Author B", ISBN = "5678" };

            Program.users.Add(user);
            Program.borrowedBooks[user] = new List<Book> { book };

            var input = new StringReader("1\n99\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.ReturnBook();

            // Assert
            Assert.IsTrue(output.ToString().Contains("Invalid input"));
        }
    }

}