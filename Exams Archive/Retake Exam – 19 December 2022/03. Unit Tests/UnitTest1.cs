namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextBookConstructorShouldInitializeCorrectly()
        {
            TextBook textBook = new TextBook("Title", "Author", "Category");
            string expectedTitle = "Title";
            string expectedAuthor = "Author";
            string expectedCategory = "Category";

            Assert.AreEqual(expectedTitle, textBook.Title);
            Assert.AreEqual(expectedAuthor, textBook.Author);
            Assert.AreEqual(expectedCategory, textBook.Category);
        }

        [Test]
        public void TextBookToStringMethodShouldReturnTheCorrectMessage()
        {
            TextBook textBook = new TextBook("Test Title", " Test Author", "Test Category");
            StringBuilder sbTest = new StringBuilder();
            sbTest.AppendLine($"Book: {textBook.Title} - {textBook.InventoryNumber}");
            sbTest.AppendLine($"Category: {textBook.Category}");
            sbTest.AppendLine($"Author: {textBook.Author}");

            Assert.AreEqual(sbTest.ToString().TrimEnd(), textBook.ToString());
        }

        [Test]
        public void UniversityLibraryConstructorShouldInitializeCorrectly()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();

            Assert.AreEqual(universityLibrary, universityLibrary);
        }

        [Test]
        public void AddTextBookToLibraryShouldWorkProperly()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook textBook = new TextBook("Test title", "Test Author", "Test category");
            string expectedMessage = library.AddTextBookToLibrary(textBook);

            Assert.AreEqual(library.Catalogue.Count, 1);
            Assert.AreEqual(textBook.InventoryNumber, 1);
            Assert.AreEqual(expectedMessage,
                $"Book: {textBook.Title} - {textBook.InventoryNumber}" + Environment.NewLine
                + $"Category: {textBook.Category}" + Environment.NewLine
                + $"Author: {textBook.Author}");
        }

        [Test]
        public void LoanTextBookToLibraryMethodShouldWorkCorrectly()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook bookOne = new TextBook("Pride and prejudice", "Jane Austin", "Classics");
            TextBook bookTwo = new TextBook("Crime and punishment", "Fyodor Dostoevsky", "Classics");
            bookOne.Holder = "Misho";

            library.AddTextBookToLibrary(bookOne);
            library.AddTextBookToLibrary(bookTwo);

            string bookOneExpectedMessage = $"Misho still hasn't returned {bookOne.Title}!";
            string bookTwoExpectedMessage = $"{bookTwo.Title} loaned to Misho.";

            string bookOneActualMessage = library.LoanTextBook(1, "Misho");
            string bookTwoActualMessage = library.LoanTextBook(2, "Misho");

            Assert.AreEqual(bookOneExpectedMessage, bookOneActualMessage);
            Assert.AreEqual(bookTwoExpectedMessage, bookTwoActualMessage);
            Assert.That(bookTwo.Holder == "Misho");
        }

        [Test]
        public void ReturnTextBookMethodShouldWorkCorrectly()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("Pride and prejudice", "Jane Austin", "Classics");
            book.Holder = "Ani";
            library.AddTextBookToLibrary(book);

            string expectedMessage = $"{book.Title} is returned to the library.";
            string actualMessage = library.ReturnTextBook(1);

            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(book.Holder, string.Empty);
        }
    }
}