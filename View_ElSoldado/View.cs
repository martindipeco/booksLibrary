using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain_Biblioteca;
using Presenter_ElSoldado;

namespace View_ElSoldado
{
    public class View : IView
    {
        private readonly Presenter _Presenter;

        public View(){
            _Presenter = new Presenter(this);
            Show_main_menu();
        }

        #region "View Logic Methods"

        private void Show_main_menu()
        {
            string option;
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("-== Quark Talent Book Library ==-");
                Console.WriteLine("");
                Console.WriteLine("Please select:");
                Console.WriteLine("");
                Console.WriteLine("1- Add new book");
                Console.WriteLine("2- Add copies to book");
                Console.WriteLine("3- Create new member");
                Console.WriteLine("4- Create new VIP member");
                Console.WriteLine("5- Lend a book"); 
                Console.WriteLine("6- Get a book back"); 
                Console.WriteLine("7- Query records"); 
                Console.WriteLine("X- Exit");

                option = Console.ReadLine();

                SelectAction(option, ref exit);

            } while (exit == false);

        }

        
        private void SelectAction(string option, ref bool exit)
        {
            switch (option)
            {
                case "1":
                    ShowAddBookMenu();
                    exit = false;
                    break;
                case "2":
                    ShowAddCopyMenu();
                    exit = false;
                    break;
                case "3":
                    ShowCreateMemberMenu();
                    exit = false;
                    break;
                case "4":
                    ShowCreateVIPMemberMenu();
                    exit = false;
                    break;
                case "5":
                    ShowLendBookMenu();
                    exit = false;
                    break;
                case "6":
                    ShowGetBookBackMenu();
                    exit = false;
                    break;
                case "7":
                    ShowRecordsMenu();
                    exit = false;
                    break;
                case "x":
                case "X":
                    Environment.Exit(2);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again");
                    Console.ReadKey();
                    exit = false;
                    break;
            }
        }

        
        private void ShowAddBookMenu()
        {
            Console.Clear();
            Console.WriteLine("Adding a book to collection");
            Console.WriteLine("");
            Console.WriteLine("Please enter Title");
            string title = Console.ReadLine();
            //Intentionally possible to include numbers as Book Titles
            while (title == "")
            {
                Console.WriteLine("Please enter Title");

                title = Console.ReadLine();

            }
            
            Console.WriteLine("Please enter Author");
            string author = Console.ReadLine();
            //Intentionally possible to include numbers and / or symbols as author´s pseudonyms
            while (author == "")
            {
                Console.WriteLine("Please enter Author");

                author = Console.ReadLine();

            }

            //Console.WriteLine("Please enter ISBN");
            bool keepPrompting;
            int isbn = 1;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Please enter ISBN");
                    isbn = int.Parse(Console.ReadLine());

                    while (isbn <= 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        isbn = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting =true;
                }
            }while (keepPrompting);
            
            Console.WriteLine("Updating Titles record:");
            _Presenter.CreateBook(title, author, isbn);
            _Presenter.ShowBooksList();

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        private void ShowAddCopyMenu()
        {
            Console.Clear();
            Console.WriteLine("Adding copies to existing Book");
            Console.WriteLine("");
            Console.WriteLine("Please select among available Titles");
            _Presenter.ShowBooksList();
            int numberOfBooks = _Presenter.ShowBooksCount();

            int indexPos = 0;
            bool keepAsking;
            do
            {
                try
                {
                    keepAsking = false;
                    Console.WriteLine("");
                    Console.WriteLine("Enter book index to add copies");
                    indexPos = int.Parse(Console.ReadLine());

                    while (indexPos < 0 || indexPos > (numberOfBooks - 1) )
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexPos = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepAsking = true;
                }
            }while (keepAsking);

            bool keepPrompting;
            int numberOfCopies = 1;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Please enter number of copies to add");
                    numberOfCopies = int.Parse(Console.ReadLine());

                    while (numberOfCopies <= 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        numberOfCopies = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            Console.WriteLine("Please enter the copies location");
            string location = Console.ReadLine();
            while (location == "")
            {
                Console.WriteLine("Please enter the copies location");

                location = Console.ReadLine();

            }

            Console.WriteLine("Updating copies record:");
            _Presenter.CreateCopies(indexPos, numberOfCopies, location);
            _Presenter.ShowCopiesList(indexPos);

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }
        private void ShowCreateMemberMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter Member Name");
            string name = Console.ReadLine();

            while (name == "" || Regex.IsMatch(name, @"\d"))
            {
                if (name == "")
                {
                    Console.WriteLine("Please enter Member Name");
                }
                else
                {
                    Console.WriteLine("Please enter Member Name without numbers");
                }
                name = Console.ReadLine();
            }

            Console.WriteLine("Please enter Member surname");
            string surname = Console.ReadLine();
            while (surname == "" || Regex.IsMatch(name, @"\d"))
            {
                if (surname == "")
                {
                    Console.WriteLine("Please enter member Surname");
                }
                else
                {
                    Console.WriteLine("Please enter member Surname without numbers");
                }
                surname = Console.ReadLine();
            }

            bool keepPrompting;
            int idNum = 1;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Please enter Id number");
                    idNum = int.Parse(Console.ReadLine());

                    while (idNum <= 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        idNum = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            _Presenter.CreateMember(name, surname, idNum);
            Console.WriteLine("Updating List of members");
            _Presenter.ShowMembersList();

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        private void ShowCreateVIPMemberMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter Member Name");
            string name = Console.ReadLine();
            while (name == "" || Regex.IsMatch(name, @"\d"))
            {
                if (name == "")
                {
                    Console.WriteLine("Please enter Member Name");
                }
                else
                {
                    Console.WriteLine("Please enter Member Name without numbers");
                }
                name = Console.ReadLine();
            }

            Console.WriteLine("Please enter Member surname");
            string surname = Console.ReadLine();
            while (surname == "" || Regex.IsMatch(surname, @"\d"))
            {
                if (name == "")
                {
                    Console.WriteLine("Please enter member Surname");
                }
                else
                {
                    Console.WriteLine("Please enter member Surname without numbers");
                }
                surname = Console.ReadLine();
            }

            bool keepPrompting;
            int idNum = 1;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Please enter Id number");
                    idNum = int.Parse(Console.ReadLine());

                    while (idNum <= 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        idNum = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            bool keepAsking;
            float fee = 1;
            do
            {
                try
                {
                    keepAsking = false;
                    Console.WriteLine("Please enter monthly fee");
                    fee = float.Parse(Console.ReadLine());

                    while (fee <= 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        fee = float.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepAsking = true;
                }
            } while (keepAsking);

            _Presenter.CreateVIPMember(name, surname, idNum, fee);
            Console.WriteLine("Updating List of VIP members");
            _Presenter.ShowVipMembersList();

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        
        private void ShowLendBookMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter index of member to lend the book");
            Console.WriteLine("Library Members:");
            _Presenter.ShowMembersList();
            Console.WriteLine("");
            Console.WriteLine("Library VIP Members:");
            _Presenter.ShowVipMembersList();
            Console.WriteLine("");
            int indexMember = 0;
            bool keepPrompting;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Enter member index: ");
                    indexMember = int.Parse(Console.ReadLine());

                    while (indexMember < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexMember = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            Console.WriteLine("");

            bool keepAsking;
            bool isVip;
            int answer = 1;
            do
            {
                try
                {
                    keepAsking = false;
                    Console.WriteLine("Please enter 1 for Standard or 2 for VIP members");
                    answer = int.Parse(Console.ReadLine());

                    while (answer <= 0 || answer > 2)
                    {
                        Console.WriteLine("Please enter 1 or 2");
                        answer = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter 1 or 2");
                    keepAsking = true;
                }
            } while (keepAsking);

            if (answer == 1)
            {
                isVip = false;
            }
            else  
            {
                isVip = true;
            }
            
            Console.WriteLine("");
            Console.WriteLine("Please enter index of book to lend");
            _Presenter.ShowBooksList();

            int indexBook = 0;
            bool keepCalling;
            do
            {
                try
                {
                    keepCalling = false;
                    Console.WriteLine("Enter book index to lend");
                    indexBook = int.Parse(Console.ReadLine());

                    while (indexBook < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexBook = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepCalling = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepCalling = true;
                }
            } while (keepCalling);

            _Presenter.CreateALending(isVip, indexMember, indexBook);

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        private void ShowGetBookBackMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter index of member giving a book back");
            Console.WriteLine("Library Members:");
            Console.WriteLine("");
            _Presenter.ShowMembersList();
            Console.WriteLine("");
            Console.WriteLine("Library VIP Members:");
            _Presenter.ShowVipMembersList();
            Console.WriteLine("");

            int indexMember = 0;
            bool keepPrompting;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Enter member index: ");
                    indexMember = int.Parse(Console.ReadLine());

                    while (indexMember < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexMember = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            Console.WriteLine("");

            bool keepAsking;
            bool isVip;
            int answer = 1;
            do
            {
                try
                {
                    keepAsking = false;
                    Console.WriteLine("Please enter 1 for Standard or 2 for VIP members");
                    answer = int.Parse(Console.ReadLine());

                    while (answer <= 0 || answer > 2)
                    {
                        Console.WriteLine("Please enter 1 or 2");
                        answer = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter 1 or 2");
                    keepAsking = true;
                }
            } while (keepAsking);

            if (answer == 1)
            {
                isVip = false;
            }
            else
            {
                isVip = true;
            }

            Console.WriteLine("");
            Console.WriteLine("Please enter index of book to give back");
            _Presenter.ShowBorrowedBooksList(isVip, indexMember);
            Console.WriteLine("");
            int indexBook = 0;
            bool keepCalling;
            do
            {
                try
                {
                    keepCalling = false;
                    Console.WriteLine("Enter book index to give back");
                    indexBook = int.Parse(Console.ReadLine());

                    while (indexBook < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexBook = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepCalling = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepCalling = true;
                }
            } while (keepCalling);

            _Presenter.GetBookBack(isVip, indexMember, indexBook);

            Console.WriteLine("Transaction completed. You can check the status from the main menu");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }
        private void ShowRecordsMenu()
        {
            string option;
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please select:");
                Console.WriteLine("");
                Console.WriteLine("1- Check for Lendings record");
                Console.WriteLine("2- Check for Returns record");
                Console.WriteLine("3- Check for member´s borrowing quota");
                Console.WriteLine("4- Check for copies availability");
                Console.WriteLine("X- Exit");

                option = Console.ReadLine();

                SelectActionRecord(option, ref exit);

            } while (exit == false);
        }

        private void SelectActionRecord(string option, ref bool exit)
        {
            switch (option)
            {
                case "1":
                    ShowLendingsRecords();
                    exit = false;
                    break;
                case "2":
                    ShowReturnsReords();
                    exit = false;
                    break;
                case "3":
                    ShowQuotaMenu();
                    exit = false;
                    break;
                case "4":
                    ShowAvailableCopiesMenu();
                    exit = false;
                    break;
                case "x":
                case "X":
                    Environment.Exit(2);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again");
                    Console.ReadKey();
                    exit = false;
                    break;
            }
        }
        private void ShowReturnsReords()
        {
            Console.Clear();
            Console.WriteLine("Showing Book Returns Records");
            _Presenter.ShowReturnsList();
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        private void ShowLendingsRecords()
        {
            Console.Clear();
            Console.WriteLine("Showing Lending Records");
            _Presenter.ShowLendingList();
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        private void ShowQuotaMenu()
        {
            string option;
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please select:");
                Console.WriteLine("");
                Console.WriteLine("1- Query for Library Member");
                Console.WriteLine("2- Query for VIP Member");
                Console.WriteLine("X- Exit");

                option = Console.ReadLine();

                SelectActionQuota(option, ref exit);

            } while (exit == false);

        }

        private void SelectActionQuota(string option, ref bool exit)
        {
            switch (option)
            {
                case "1":
                    ShowLibraryMemberQuotaMenu();
                    exit = false;
                    break;
                case "2":
                    ShowVipMemberQuotaMenu();
                    exit = false;
                    break;
                case "x":
                case "X":
                    Environment.Exit(2);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again");
                    Console.ReadKey();
                    exit = false;
                    break;
            }
        }

        private void ShowLibraryMemberQuotaMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select among available library members");
            Console.WriteLine("");
            _Presenter.ShowMembersList();
            Console.WriteLine("");

            int indexPos = 0;
            bool keepPrompting;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Enter Member index number");
                    indexPos = int.Parse(Console.ReadLine());

                    while (indexPos < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexPos = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            _Presenter.QueryStandardQuota(indexPos);

            //TODO: Replace with Dictionary implementation
            //Replace ShowMembersList(); x ShowMembersDict();
            //Console.WriteLine("Please enter Member ID");
            //int idNum = int.Parse(Console.ReadLine());
            //Replace _Presenter.QueryStandardQuota(indexPos); x _Presenter.QueryStandardQuotaDict(idNum);

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();

        }

        private void ShowVipMemberQuotaMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select among available VIP members");
            Console.WriteLine("");
            _Presenter.ShowVipMembersList();
            int indexPos = 0;
            bool keepPrompting;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Enter Member index number");
                    indexPos = int.Parse(Console.ReadLine());

                    while (indexPos < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexPos = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            _Presenter.QueryVipQuota(indexPos);

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        private void ShowAvailableCopiesMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select the book to query for available copies");
            Console.WriteLine("");
            _Presenter.ShowBooksList();
            Console.WriteLine("");

            int indexPos = 0;
            bool keepPrompting;
            do
            {
                try
                {
                    keepPrompting = false;
                    Console.WriteLine("Enter book index:");
                    indexPos = int.Parse(Console.ReadLine());

                    while (indexPos < 0)
                    {
                        Console.WriteLine("Please enter a positive number");
                        indexPos = int.Parse(Console.ReadLine());
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
                catch (IndexOutOfRangeException i)
                {
                    Console.WriteLine("Please enter a correct number");
                    keepPrompting = true;
                }
            } while (keepPrompting);

            _Presenter.QueryBookCopies(indexPos);

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
            Show_main_menu();
        }

        #endregion

        #region "IView Interface methods"
        public void Show_text(string message)
        {
            Console.WriteLine(message);
        }
        
        #endregion
    }
}
