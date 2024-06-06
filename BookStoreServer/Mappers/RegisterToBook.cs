using BookStoreServer.Models.DTOs;
using BookStoreServer.Models;

namespace BookStoreServer.Mappers
{
    public class RegisterToBook
    {
        Book book;

        public RegisterToBook(BookDTO registerBook)
        {
            book = new Book();
            book.AuthorID = registerBook.AuthorID;
            book.BookSerialNumber = registerBook.BookSerialNumber;
            book.BookPrice = registerBook.BookPrice;
            book.BookQuantity = registerBook.BookQuantity;
            book.CategoryID = registerBook.CategoryID;
            book.BookImageLink = registerBook.BookImageLink;
            book.BookYear = registerBook.BookYear;
            book.BookTitle = registerBook.BookTitle;
        }

        public Book GetBook()
        {
            return book;
        }
    }
}
