using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain_Biblioteca;
using Domain_Library;

namespace Presenter_ElSoldado
{
    public class Presenter
    {
        private readonly Book _book;
         
        private readonly BookLending _bookLending;
        private readonly LibraryMember _libraryMember;
        private readonly VipMember _vipMember;
        private readonly List<LibraryMember> _membersList;
        private readonly IView _view;

        public Presenter(IView view)
        {
            _membersList = new List<LibraryMember>();
            _book = new Book();
            _libraryMember = new LibraryMember();
            _vipMember = new VipMember();
            _bookLending = new BookLending();  

            _view = view;
        }

        #region "Use Cases"

        public void CreateBook(string name, string author, int isbn)
        {
            _book.CreateBook(name, author, isbn);
        }

        public void ShowBooksList()
        {
            _view.Show_text(_book.ShowBooksList());
        }

        public int ShowBooksCount()
        {
            int bookCount = _book.ShowBooksCount();
            return bookCount;
        }

        public void ShowBorrowedBooksList(bool isVip, int indexMember)
        {
            if (!isVip)
            {
                _view.Show_text(_libraryMember.ShowBorrowedBooks(indexMember));
            }
            else
            {
                _view.Show_text(_vipMember.ShowBorrowedBooks(indexMember));
            }
        }

        public Book GetBookFromIndex(int indexPos)
        {
            Book bookToReturn = _book.ReturnABookFromList(indexPos);
            return bookToReturn;
        }
        
        public void CreateCopies(int indexPos, int numberOfcopies, string location)
        {
            _book.CreateCopies(indexPos, numberOfcopies, location);
        }

        public void ShowCopiesList(int indexPos)
        {
            _view.Show_text(_book.ShowCopiesList(indexPos));
        }

        public void CreateMember(string name, string surname, int idNum)
        {
            _libraryMember.CreateMember(name, surname, idNum);
        }

        public void ShowMembersList()
        {
            _view.Show_text(_libraryMember.ShowMembersList()); 
        }

        public void CreateVIPMember(string name, string surname, int idNum, float fee)
        {
            _vipMember.CreateVipMember(name, surname, idNum, fee);
        }

        public void ShowVipMembersList()
        {
            _view.Show_text(_vipMember.ShowVipMemberList());
        }

        public void QueryStandardQuota(int indexPos)
        {
            string quotaMessage = "";
            if (_libraryMember.HasQuota(indexPos))
            {
                quotaMessage = $"Member can borrow a book";
            }
            else
            {
                quotaMessage = $"Member has reached Quota limit. No more books can be borrowed";
            }
            _view.Show_text(quotaMessage);
        }

        public void QueryVipQuota(int indexPos)
        {
            string quotaMessage = "";
            if (_vipMember.HasQuota(indexPos))
            {
                quotaMessage = $"Member can borrow a book";
            }
            else
            {
                quotaMessage = $"Member has reached Quota limit. No more books can be borrowed";
            }
            _view.Show_text(quotaMessage);
        }

        public void QueryBookCopies(int indexPos)
        {
            string message = "";
            if (_book.AvailableCopies(indexPos))
            {
                message = "You can borrow a copy of that title";
            }
            else
            {
                message = "No available copies";
            }
            _view.Show_text(message);
        }

        public void CreateALending(bool isVip, int indexMember, int indexBook)
        {
            string message = "";
            if (!_book.AvailableCopies(indexBook))
            {
                message = "No available copies of that title";
            }
            else
            {
                Book bookForMember = GetBookFromIndex(indexBook);

                if (!isVip)
                {
                    LibraryMember libraryMember = _libraryMember.GetMemberFromIndex(indexMember);
                    if (_libraryMember.HasQuota(indexMember))
                    {
                        _bookLending.CreateBookLending(bookForMember, libraryMember);
                        _book.SubtractCopy(bookForMember);
                    }
                    message = _libraryMember.BorrowBook(indexMember, bookForMember);
                }
                else
                {
                    VipMember vipMember = _vipMember.GetVipMemberFromIndex(indexMember);
                    if (_vipMember.HasQuota(indexMember))
                    {
                        _bookLending.CreateBookLending(bookForMember, vipMember);
                        _book.SubtractCopy(bookForMember);

                    }
                    message = _vipMember.BorrowBook(indexMember, bookForMember);
                }
            }
            _view.Show_text(message);
        }

        public void GetBookBack(bool isVip, int indexMember, int indexBook)
        {
            Book bookForMember = GetBookFromIndex(indexBook);
            if (!isVip)
            {
                LibraryMember libraryMember = _libraryMember.GetMemberFromIndex(indexMember);
                _libraryMember.GiveCopyBack(indexMember, bookForMember);
                _bookLending.CreateBookReturn(bookForMember, libraryMember);
            }
            else
            {
                VipMember vipMember = _vipMember.GetVipMemberFromIndex(indexMember);
                _vipMember.GiveCopyBack(indexMember, bookForMember);
                _bookLending.CreateBookReturn(bookForMember, vipMember);
            }
            _book.AddCopy(bookForMember);
        }

        public void ShowLendingList()
        {
            _view.Show_text(_bookLending.ShowLendings());
        }

        public void ShowReturnsList()
        {
            _view.Show_text(_bookLending.ShowBookReturns());
        }

        #endregion
    }
}
