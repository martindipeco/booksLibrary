using Domain_Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Library
{
    public class VipMember : LibraryMember
    {
        //parallel lists for members and members´ borrowing quota (TODO: immplement Dictionary)
        public List<VipMember> _vipMembers = new List<VipMember>();
        public List<int> _vipMembersQuota = new List<int>();

        private Dictionary<VipMember, int> _vipsQuota = new Dictionary<VipMember, int>();
        public Dictionary<VipMember, int> VipsQuota { get { return _vipsQuota; } }

        internal float fee;
        internal float Fee { get => fee; }
        
        public VipMember(string name, string surname, int idNum, float fee)
        {
            this.name = name;
            this.surname = surname;
            this.idNum = idNum;
            this.fee = fee;
        }
        public VipMember()
        {
            this.name = "";
            this.surname = "";
            this.idNum = 0;
            this.fee = 0;
        }

        public void CreateVipMember(string name, string surname, int idNum, float fee)
        {
            VipMember vipMember = new VipMember(name, surname, idNum, fee);
            _vipMembers.Add(vipMember);
            _vipMembersQuota.Add(0);
        }
        public string ShowVipMemberList()
        {
            if (!_vipMembers.Any())
            {
                return "No registered members";
            }
            else
            {
                String listOfMembers = "";

                foreach (VipMember vm in _vipMembers)
                {
                    listOfMembers = $"{listOfMembers}\n" + $"Index: {_vipMembers.IndexOf(vm)}. Member: {vm.Name} {vm.Surname} Id: {vm.IdNum}, Fee; {vm.Fee}";
                }
                return listOfMembers;
            }
        }

        public override bool HasQuota(int indexPos)
        {
            if (_vipMembersQuota[indexPos] >= 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string BorrowBook(int indexMember, Book book)
        {
            string message = "";
            VipMember selectedMember = _vipMembers[indexMember];

            if (!HasQuota(indexMember))
            {
                message = "Member has reached Quota Limit";
            }
            else
            {
                selectedMember._borrowedBooks.Add(book);
                _vipMembersQuota[indexMember] = _vipMembersQuota[indexMember] + 1;
                message = "Transaction completed. You can check the status from the main menu";
            }
            return message;
        }

        public override void GiveCopyBack(int indexMember, Book book)
        {
            VipMember selectedMember = _vipMembers[indexMember];
            selectedMember._borrowedBooks.Remove(book);

            _vipMembersQuota[indexMember] = _vipMembersQuota[indexMember] - 1;
        }

        public override string ShowBorrowedBooks(int indexMember)
        {
            LibraryMember selectedMember = _vipMembers[indexMember];
            string listOfBorrowedBooks = "";
            foreach (Book bb in selectedMember.BorrowedBooks)
            {
                listOfBorrowedBooks = $"{listOfBorrowedBooks}\n" + $"Index: {selectedMember.BorrowedBooks.IndexOf(bb)}, Book Title:{bb.Name}, Author: {bb.Author}";
            }
            return listOfBorrowedBooks;
        }

        public VipMember GetVipMemberFromIndex(int index)
        {
            VipMember vipMember = _vipMembers[index];
            return vipMember;
        }
    }
}
