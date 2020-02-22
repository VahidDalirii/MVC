using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface
{
    class UI_LibraryApp
    {
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Enter your Name and Passeord to SIGN IN");
            Console.WriteLine("---------------------------------------\n");

            List<Member> allMembers = MemberRepository.GetMembers();

            Console.Write("\nEnter your USER NAME: ");
            string userName= Console.ReadLine();

            Console.Write("Enter your PASSWORD: ");
            string password = Console.ReadLine();

            foreach (Member member in allMembers)
            {
                if (member.Name==userName && member.Password==password)
                {
                    Member loggedInMember = member;
                    ShowMainMenu(loggedInMember);
                }
            }

            Console.WriteLine("\nThere is no USER registered with this USERNAME and PASSWORD");
            Console.Write("Press any key to try again... ");
            Console.ReadKey();
            Start();
        }

        /// <summary>
        /// Shows Main menu
        /// </summary>
        /// <param name="member"></param>
        private void ShowMainMenu(Member member)
        {
            Console.Clear();
            Console.WriteLine($"{member.Name} Signed in\n");

            Console.WriteLine("\nMain Menu");
            Console.WriteLine("---------\n");
            Console.WriteLine("1. My profile");
            Console.WriteLine("2. Show all my rents\n");
            Console.WriteLine("----------------------------");
            Console.WriteLine("3. Show all items in library");
            Console.WriteLine("4. Opening hours and contact\n");
            Console.WriteLine("----------------------------");
            Console.WriteLine("0. Exit");

            Console.Write("\nSelect an option: ");
            string selectedOption = Console.ReadLine();

            switch (selectedOption)
            {
                case "1":
                    ShowMyProfile(member);
                    break;
                case "2":
                    ShowMyRents(member);
                    break;
                case "3":
                    ShowAllItems(member);
                    break;
                case "4":
                    LibraryInfo(member);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    ShowMainMenu(member);
                    break;
            }
        }

        /// <summary>
        /// Shows member's My profile 
        /// </summary>
        /// <param name="member"></param>
        private void ShowMyProfile(Member member) 
        {
            Console.Clear();
            Console.WriteLine("Your profile");
            Console.WriteLine("------------");
            Console.WriteLine($"Member's Name: {member.Name}");
            Console.WriteLine($"Member's Address: {member.Address}");
            Console.WriteLine($"Member's Mobile number: {member.TelNumber}\n");
            Console.WriteLine("------------------------");
            Console.WriteLine("\n1. Edit my profile");
            Console.WriteLine("2. Change my password");
            Console.WriteLine("3. Go back to main menu");
            Console.Write("\nSelect an option: ");

            string enteredOption=Console.ReadLine();

            switch (enteredOption)
            {
                case "1":
                    EditMyProfile(member);
                    break;
                case "2":
                    ChangeMyPassword(member);
                    break;
                case "3":
                    ShowMainMenu(member);
                    break;
                default:
                    ShowMyProfile(member);
                    break;
            }
        }

        /// <summary>
        /// Updates member's profile properties in db
        /// </summary>
        /// <param name="member"></param>
        private void EditMyProfile(Member member)
        {
            Console.Clear();
            Console.WriteLine("Editing your profile");
            Console.WriteLine("--------------------\n");

            string name = "";
            while (true) //Checks if entered name is unique 
            {
                Console.Write("Enter USER NAME(user name must be unique): ");
                name = Console.ReadLine();
                if (name.Length != 0 && IfNameIsUnique(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You either entered No Name or this name is ALREADY REGISTERED in Library");
                    Console.WriteLine("Try ANOTHER name\n");
                }
            }

            string address = "";
            while (address == "")
            {
                Console.Write("Enter ADDRESS: ");
                address = Console.ReadLine();
            }

            string telNumber = "";
            while (true)
            {
                Console.Write("Enter MOBILE NUMBER(10 numbers): ");
                string input = Console.ReadLine();
                if (input.Length != 0 && IsDigitsOnly(input) && input.Length == 10)
                {
                    telNumber = input;
                    break;
                }
            }

            Member editedMember = new Member(name, address, telNumber, member.Password);
            editedMember.Id = member.Id;

            MemberRepository.UpdateMember(editedMember);//Updates member in db

            Console.WriteLine("\nYour profile edited SUCCESSFLLY");
            PressKeyToGoBackToMyProfile(member);
        }

        /// <summary>
        /// Changes member's password in db
        /// </summary>
        /// <param name="member"></param>
        private void ChangeMyPassword(Member member)
        {
            Console.Clear();
            Console.WriteLine("Changeing your password");
            Console.WriteLine("-----------------------\n");
            string currentPass = "";
            while (currentPass=="")
            {
                Console.Write("Enter your CURRENT password: ");
                currentPass = Console.ReadLine();
            }
            

            if (currentPass==member.Password)
            {
                while (true)
                {
                    string newPass = "";
                    while (newPass == "")
                    {
                        Console.Write("\nEnter your NEW password: ");
                        newPass = Console.ReadLine();
                    }

                    string newPassword = "";
                    while (newPassword == "")
                    {
                        Console.Write("\nEnter your NEW password AGAIN: ");
                        newPassword = Console.ReadLine();
                    }

                    if (newPass == newPassword)
                    {
                        MemberRepository.ChangePassword(member, newPassword); // Updates password in db
                        member.Password = newPassword;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nYou need to try again and enter same new password 2 TIMES");
                        Console.Write("\nPress any key continue... ");
                        Console.ReadKey();
                    }
                } 
            }
            else
            {
                Console.WriteLine("\nYou entered a Wrong Password");
                Console.Write("\nPress any key continue... ");
                Console.ReadKey();
                ChangeMyPassword(member);
            }

            Console.WriteLine("\nYour password changed SUCCESSFULLY");
            PressKeyToGoBackToMyProfile(member);
        }

        /// <summary>
        /// Shows member's all rents
        /// </summary>
        /// <param name="member"></param>
        private void ShowMyRents(Member member)
        {
            Console.Clear();
            Console.WriteLine("All my Rents");
            Console.WriteLine("------------\n");

            
            List<Rent> myRents = RentRepository.GetMyRents(member);

            if (myRents.Count == 0)
            {
                Console.WriteLine("\nThere is no rents registered");
            }
            else
            {
                for (int i = 0; i < myRents.Count; i++)
                {
                    int j = i;
                    if (myRents[i].RentedBook != null)
                    {
                        Console.WriteLine($"[{++j}][Book] [Title]: {myRents[i].RentedBook.Title} is rented from {myRents[i].StartDate} until {myRents[i].EndDate}");
                    }
                    else
                    {
                        Console.WriteLine($"[{++j}][Film] [Title]: {myRents[i].RentedFilm.Title} is rented from {myRents[i].StartDate} until {myRents[i].EndDate}");
                    }
                }
            }
            PressKeyToGoBackToMainMenu(member);
        }

        /// <summary>
        /// Gets all items from db and Shows all library's items
        /// </summary>
        /// <param name="member"></param>
        private void ShowAllItems(Member member)
        {
            Console.Clear();

            List<Book> books = BookRepository.GetBooks();

            Repository.Models.SortBooksByTitle sortBooksByTitle = new SortBooksByTitle();
            books.Sort(sortBooksByTitle);

            Console.WriteLine("------------------------------\n");
            Console.WriteLine($"List of all {books.Count} BOOKS in library");
            Console.WriteLine("------------------------------\n");

            if (books.Count == 0)
            {
                Console.WriteLine("\nThere is no BOOKS in library");
            }
            else
            {
                Console.WriteLine("[ID]");
                for (int i = 0; i < books.Count; i++)
                {
                    int j = i;
                    Console.WriteLine($"[{++j}] [Title]:{books[i].Title} [Genre]:{books[i].Genre} [Author]:{books[i].Author} [Published year]:{books[i].PublishedYear} [copies]:{books[i].Copies}");
                }
            }

            List<Film> films = FilmRepository.GetFilms();

            SortFilmsByTitle sortFilmsByTitle = new SortFilmsByTitle();
            films.Sort(sortFilmsByTitle);

            Console.WriteLine("\n------------------------------");
            Console.WriteLine($"List of all {films.Count} FILMS in library");
            Console.WriteLine("------------------------------\n");

            if (films.Count == 0)
            {
                Console.WriteLine("\nThere is no FILMS in library");
            }
            else
            {
                Console.WriteLine("[ID]");
                for (int i = 0; i < films.Count; i++)
                {
                    int j = i;
                    Console.WriteLine($"[{++j}] [Title]:{films[i].Title} [Genre]:{films[i].Genre} [Director]:{films[i].Director} [Released year]:{films[i].ReleasedYear} [copies]:{films[i].Copies}");
                }
            }
            PressKeyToGoBackToMainMenu(member);

        }

        /// <summary>
        /// Shows information about library
        /// </summary>
        /// <param name="member"></param>
        private void LibraryInfo(Member member)
        {
            Console.Clear();

            Console.WriteLine("You are always welcome to visit us");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("\nOur visit address: Storgatan 8");
            Console.WriteLine("                   Ulricehamn");
            Console.WriteLine("\nTel.number  0331-23564\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("\nOur opening houers is:");
            Console.WriteLine("\nMonday - Thursday    9:00 - 17:00");
            Console.WriteLine("Friday               9:00 - 15:00");
            Console.WriteLine("Saturday and Sundat  Close\n");

            PressKeyToGoBackToMainMenu(member);
        }


        //----------------------------------------------------------- HELP METHODS -------------------------------------------------------------------

        /// <summary>
        /// Checks if new name to register is unique in db
        /// </summary>
        /// <param name="name"></param>
        /// <returns>returns true if name is unique in db and flase if not</returns>
        private bool IfNameIsUnique(string name)
        {
            List<Member> members = MemberRepository.GetMembers();
            foreach (Member member in members)
            {
                if (member.Name.ToLower() == name.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if input is numbers or not
        /// </summary>
        /// <param name="str"></param>
        /// <returns>True if input is numbers and false if not</returns>
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Goes back to Start menu
        /// </summary>
        private void PressKeyToGoBackToMainMenu(Member member)
        {
            Console.Write("\nPress any key to go back to MAIN MENU ...");
            Console.ReadKey();
            ShowMainMenu(member);
        }

        /// <summary>
        /// Goes back to My profile 
        /// </summary>
        private void PressKeyToGoBackToMyProfile(Member member)
        {
            Console.Write("\nPress any key to continue ...");
            Console.ReadKey();
            ShowMyProfile(member);
        }
    }
}
