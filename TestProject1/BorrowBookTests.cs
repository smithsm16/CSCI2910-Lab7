using Blazor_Lab_Starter_Code;
namespace TestProject1
{

    [TestClass]
    public class BorrowBookTests
    {
        [TestInitialize]
        public void Init()
        {
            Program.books = new List<Book>();
            Program.users = new List<User>();
            Program.borrowedBooks = new Dictionary<User, List<Book>>();
        }

        [TestMethod]
        public void BorrowBook_ValidInput_Success()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book A" };
            var user = new User { Id = 10, Name = "User A" };

            Program.books.Add(book);
            Program.users.Add(user);

            Console.SetIn(new StringReader("1\n10\n"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.BorrowBook();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Book borrowed successfully"));
            Assert.AreEqual(0, Program.books.Count);
            Assert.AreEqual(1, Program.borrowedBooks[user].Count);
        }

        [TestMethod]
        public void BorrowBook_InvalidBookId_ShowsError()
        {
            // Arrange
            Console.SetIn(new StringReader("ABC\n"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.BorrowBook();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Invalid input"));
        }

        [TestMethod]
        public void BorrowBook_BookNotFound_ShowsError()
        {
            // Arrange
            Console.SetIn(new StringReader("9999999\n"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.BorrowBook();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Book not found"));
        }

        [TestMethod]
        public void BorrowBook_InvalidUserId_ShowsError()
        {
            // Arrange
            Program.books.Add(new Book { Id = 1, Title = "Book A" });

            Console.SetIn(new StringReader("1\nABC\n"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.BorrowBook();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Invalid input"));
        }

        [TestMethod]
        public void BorrowBook_UserNotFound_ShowsError()
        {
            // Arrange
            Program.books.Add(new Book { Id = 1, Title = "Book A" });

            Console.SetIn(new StringReader("1\n9999999\n"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.BorrowBook();

            // Assert
            string output = sw.ToString();
            Assert.IsTrue(output.Contains("User not found"));
        }
    }
}