using Domain_Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Library
{
    public class BookLending
    {
        public List<BookLending> bookLendings = new List<BookLending>();
        private List<BookLending> bookReturns = new List<BookLending>();

        private Book _book;

        private LibraryMember _libraryMember;

        private LibraryMember LibraryMember { get => _libraryMember; }

        internal DateTime transactionDate;

        public BookLending(Book book, LibraryMember libraryMember)
        {
            _book = book;
            _libraryMember = libraryMember;
            transactionDate = DateTime.Now;
        }

        public BookLending()
        {
            _book = new Book();
            _libraryMember = new LibraryMember();
        }

        public void CreateBookLending(Book book, LibraryMember libraryMember)
        {
            BookLending bookLending = new BookLending(book, libraryMember);
            bookLendings.Add(bookLending);
        }

        public void CreateBookLending(Book book, VipMember libraryMember)
        {
            BookLending bookLending = new BookLending(book, libraryMember);
            bookLendings.Add(bookLending);
        }

        public String ShowLendings()
        {
            if (!bookLendings.Any())
            {
                return "No Lendings";
            }
            else
            {
                String lendingRecord = "\n";

                foreach (BookLending bl in bookLendings)
                {
                    lendingRecord = $"{lendingRecord}\n" + $"Book: {bl._book.Name}, Lent to: {bl._libraryMember.Name}, Date: {bl.transactionDate}.\n";
                }
                return lendingRecord;
            }
        }

        public void CreateBookReturn(Book book, LibraryMember libraryMember)
        {
            BookLending bookReturn = new BookLending(book, libraryMember);
            bookReturns.Add(bookReturn);
        }

        public void CreateBookReturn(Book book, VipMember libraryMember)
        {
            BookLending bookReturn = new BookLending(book, libraryMember);
            bookReturns.Add(bookReturn);
        }

        public String ShowBookReturns()
        {
            if (!bookReturns.Any())
            {
                return "No Returns";
            }
            else
            {
                String bookReturnsRecord = "\n";

                foreach (BookLending br in bookReturns)
                {
                    bookReturnsRecord = $"{bookReturnsRecord}\n" + $"Book: {br._book.Name}, Lent to: {br._libraryMember.Name}, Date: {br.transactionDate}.\n";
                }
                return bookReturnsRecord;
            }
        }
    }
}
