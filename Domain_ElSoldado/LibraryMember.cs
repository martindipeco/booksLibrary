using Domain_Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Library
{
    public class LibraryMember
    {
        protected List<Book> _borrowedBooks; //set max to 1 for standard, 3 for vip
        public List<Book> BorrowedBooks { get { return _borrowedBooks; } }

        //parallel lists for members and members´ borrowing quota
        private List<LibraryMember> _members = new List<LibraryMember>();
        private List<int> _membersQuota = new List<int>();

        //TODO: Implement Dictionary instead of parallel lists
        private Dictionary<LibraryMember, int> _membersAndQuota = new Dictionary<LibraryMember, int>();
        public Dictionary<LibraryMember, int> MembersAndQuota { get { return _membersAndQuota; } }

        internal String name = "";
        internal String Name { get { return name; } }

        internal String surname = "";
        internal String Surname { get { return surname; } }

        internal int idNum;
        internal int IdNum { get => idNum; }

        
        public LibraryMember(string name, string surname, int idNum)
        {
            this.name = name;
            this.surname = surname;
            this.idNum = idNum;
            _borrowedBooks = new List<Book>();
        }

        public LibraryMember()
        {
            this.name = "";
            this.surname = "";
            this.idNum = 0;
            _borrowedBooks = new List<Book>();
        }

        public void CreateMember(string name, string surname, int idNum)
        {
            LibraryMember libraryMember = new LibraryMember(name, surname, idNum);
            _members.Add(libraryMember);
            _membersQuota.Add(0);
            _membersAndQuota.Add(libraryMember, 0); 
            _borrowedBooks = new List<Book>();
        }

        public string ShowMembersList()
        {
            if (!_members.Any())
            {
                return "No registered members";
            }
            else
            {
                String listOfMembers = "";

                foreach (LibraryMember lm in _members)
                {
                    listOfMembers = $"{listOfMembers}\n" + $"Index: {_members.IndexOf(lm)}. Member: {lm.Name} {lm.Surname} Id: {lm.IdNum}";
                }
                return listOfMembers;
            }
        }

        public string ShowMembersDict()
        {
            if (!_membersAndQuota.Any())
            {
                return "No registered members";
            }
            else
            {
                String dictOfMembers = "";

                foreach (KeyValuePair<LibraryMember, int> element in _membersAndQuota)
                {
                    dictOfMembers = $"{dictOfMembers}\n" + $"Member: {element.Key.Name} {element.Key.Surname} Id: {element.Key.IdNum}";
                }
                return dictOfMembers;
            }
        }


        public virtual bool HasQuota(int indexPos)
        {
            if (_membersQuota[indexPos] >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual String BorrowBook(int indexMember, Book book)
        {
            string message = "";
            LibraryMember selectedMember = _members[indexMember];
            if (!HasQuota(indexMember)) 
            {
                message = "Member has reached Quota Limit";
            }
            else
            {
                selectedMember.BorrowedBooks.Add(book);
                _membersQuota[indexMember] = _membersQuota[indexMember] + 1;
                message = "Transaction completed. You can check the status from the main menu";
            }
            return message;
        }

        public virtual void GiveCopyBack(int indexMember, Book book)
        {
            LibraryMember selectedMember = _members[indexMember];
            selectedMember.BorrowedBooks.Remove(book);

            _membersQuota[indexMember] = _membersQuota[indexMember] - 1;
        }

        public virtual string ShowBorrowedBooks(int indexMember)
        {
            LibraryMember selectedMember = _members[indexMember];
            string listOfBorrowedBooks = "";
            foreach (Book bb in selectedMember.BorrowedBooks)
            {
                listOfBorrowedBooks = $"{listOfBorrowedBooks}\n" + $"Index: {selectedMember.BorrowedBooks.IndexOf(bb)}, Book Title:{bb.Name}, Author: {bb.Author}";
            }
            return listOfBorrowedBooks;
        }

        public LibraryMember GetMemberFromIndex (int index)
        {
            LibraryMember libraryMember = _members[index];
            return libraryMember;
        }
    }
}
