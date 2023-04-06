using Domain_Biblioteca;
using Domain_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Domain_Biblioteca.Book;

namespace Domain_Biblioteca
{
    public class Book
    {
        internal LibraryMember _libraryMember;

        private List<Copy> bookCopies;

        private List<Book> titles = new List<Book>();

        internal String name = "";
        internal String Name { get => name; }

        internal String author = "";
        internal String Author { get => author; }

        internal int isbn;

        internal int numberOfCopies;
        
        public Book(string name, string author, int isbn) 
        {
            this.name = name;
            this.author = author;
            this.isbn = isbn;
            this.bookCopies = new List<Copy>();
        }

        public Book()
        {
            this.name = "";
            this.author = "";
            this.isbn = 0;
        }
        
        public void CreateBook(string name, string author, int isbn)
        {
            Book bookFromPresenter = new Book(name, author, isbn);
            titles.Add(bookFromPresenter);
        }

        public string ShowBooksList()
        {
            if (!titles.Any())
            {
                return "No registered Titles";
            }
            else
            {
                String listOfTitles = "";

                foreach (Book b in titles)
                {
                    listOfTitles = $"{listOfTitles}\n" + $"Index: {titles.IndexOf(b)}, Book Title: {b.name}, Author: {b.author}, ISBN: {b.isbn}, Copies: {b.bookCopies.Count}";
                }
                return listOfTitles;
            }
        }

        public int ShowBooksCount()
        {
            int booksCount = 0;
            foreach (Book b in titles)
            {
                booksCount++;
            }
            return booksCount;
        }

        public void CreateCopies(int indexPos, int numberOfCopies, string location)
        {
            
            Book selectedBook = titles[indexPos];
            for (int i = 1; i <= numberOfCopies; i++)
            {
                Copy niceCopy = new Copy(selectedBook, i, location);
                selectedBook.bookCopies.Add(niceCopy);
            }
        }
     
        public Book ReturnABookFromList(int indexPos)
        {
            Book bookFromList = titles[indexPos];
            return bookFromList;
        }

        public string ShowCopiesList(int indexPos)
        {
            Book selectedBook = titles[indexPos];
            if (!selectedBook.bookCopies.Any())
            {
                return "No Copies available";
            }
            else
            {
                String listOfCopies = "";

                foreach (Copy c in selectedBook.bookCopies)
                {
                    listOfCopies = $"{listOfCopies}\n" + $"Book Title:{c._book.Name}, Author: {c._book.Author}, Location: {c.location}, Copy Number: {c.CopyNumber}";
                }
                return listOfCopies;
            }
        }

        public bool AvailableCopies(int indexPos)
        {
            //TODO: keep at least 1 copy for "in-place" consulting.
            Book requestedBook = titles[indexPos];
            if (!requestedBook.bookCopies.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SubtractCopy(Book book)
        {
            book.bookCopies.RemoveAt(0); //TODO: keep at least 1 copy for "in-place" consulting.
        }

        public void AddCopy(Book book)
        {
            Copy copyToAdd = book.bookCopies[0]; //There must always remain at least 1 copy unborrowed
            book.bookCopies.Add(copyToAdd);
        }

        internal class Copy 
        {
            internal Book _book;            

            internal Book Book { get => _book; }

            internal int copyNumber;

            internal int CopyNumber { get => copyNumber; }

            internal String location = "";
            internal String Location { get => location; }


            internal Copy(Book book, int copyNumber, string location) 
            {
                this._book = book;
                this.copyNumber = copyNumber;
                this.location = location;
            }

            internal Copy(int copyNumber, string location)
            {
                this.copyNumber = copyNumber;
                this.location = location;
            }

            internal Copy(Book book)
            {
                this.copyNumber = (book.bookCopies[0].copyNumber) + 1;
                this.location = book.bookCopies[0].location;
            }
        }
    }
}
