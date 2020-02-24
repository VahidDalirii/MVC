using MongoDB.Bson;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LibraryApp
{
    class LibraryApp
    {
        
        public void Start()
        {
            string selectedOption = ShowMainMenue();

            switch (selectedOption)
            {
                case "1":
                    CreateItem();
                    break;
                case "2":
                    EditItem();
                    break;
                case "3":
                    ShowItems();
                    break;
                case "4":
                    DeleteItems();
                    break;
                case "5":
                    CreateMember();
                    break;
                case "6":
                    ShowMembers();
                    PressKeyToGoBackToStart();
                    break;
                case "7":
                    DeleteMember();
                    break;
                case "8":
                    RentMenu();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    ShowMainMenue();
                    break;
            }

        }

        /// <summary>
        /// Shows main menu and returns chosen option
        /// </summary>
        /// <returns>returns chosen option</returns>
        private string ShowMainMenue()
        {
            Console.Clear();

            Console.WriteLine("Main Menu");
            Console.WriteLine("---------\n");
            Console.WriteLine("1. Create item");
            Console.WriteLine("2. Edit item");
            Console.WriteLine("3. Show all items");
            Console.WriteLine("4. Delete item\n");
            Console.WriteLine("-------------------");
            Console.WriteLine("5. Create member");
            Console.WriteLine("6. Show all members");
            Console.WriteLine("7. Delete member\n");
            Console.WriteLine("-------------------");
            Console.WriteLine("8. Rent menu\n");
            Console.WriteLine("-------------------");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect an option: ");

            string input = Console.ReadLine();

            return input;
        }

        /// <summary>
        /// Shows rent menu
        /// </summary>
        private void RentMenu()
        {
            Console.Clear();
            Console.WriteLine("\nRent Menu");
            Console.WriteLine("---------\n");
            Console.WriteLine("1. Rent an item");
            Console.WriteLine("2. Show rents");
            Console.WriteLine("3. Return an item");
            Console.WriteLine("----------------");
            Console.WriteLine("0. Main menu");
            Console.Write("\nSelect an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RentItem();
                    break;

                case "2":
                    ShowRents();
                    break;
                case "3":
                    ReturnItem();
                    break;
                case "0":
                    Start();
                    break;
                default:
                    RentMenu();
                    break;
            }
            PressKeyToGoBackToRentMenu();
        }

        /// <summary>
        /// Creates book or film object and saves in db
        /// </summary>
        private void CreateItem()
        {
            Console.Clear();

            Console.WriteLine("Create a new ITEM");
            Console.WriteLine("-----------------\n");
            Console.WriteLine("\n** Select ITEM category **");

            string itemType = "";
            while (itemType.ToLower() != "b" && itemType.ToLower() != "f")
            {
                Console.Write("Select (b)ook or (f)ilm: ");
                itemType = Console.ReadLine();
            }

            string title = "";
            while (title == "")
            {
                Console.Write("Enter TITLE: ");
                title = Console.ReadLine();
            }

            string genre = "";
            while (genre == "")
            {
                Console.Write("Enter GENRE: ");
                genre = Console.ReadLine();
            }


            if (itemType.ToLower() == "b")
            {
                string author = "";
                while (author == "")
                {
                    Console.Write("Enter AUTHOR name: ");
                    author = Console.ReadLine();
                }

                string publishedYear = "";
                while (true)
                {
                    Console.Write("Enter PUBLISHED year(yyyy): ");
                    string input = Console.ReadLine();
                    if (input.Length != 0 && input.Length == 4 && IsDigitsOnly(input) && int.Parse(input) > 0)
                    {
                        publishedYear = input;
                        break;
                    }

                }

                int copies = 0;
                while (true)
                {
                    Console.Write("Enter amount COPIES: ");
                    string input = Console.ReadLine();
                    if (input.Length != 0 && IsDigitsOnly(input) && int.Parse(input) > 0)
                    {
                        int.TryParse(input, out copies);
                        break;
                    }
                }

                Book newBook = new Book(title, genre, copies, author, publishedYear);
                BookRepository.CreateBook(newBook);
                Console.WriteLine($"\n{newBook.Copies} Copies of '{newBook.Title}' created SUCCESSFULLY");
            }
            else if (itemType.ToLower() == "f")
            {
                string director = "";
                while (director == "")
                {
                    Console.Write("Enter DIRECTOR name: ");
                    director = Console.ReadLine();
                }

                string releasedYear = "";
                while (true)
                {
                    Console.Write("Enter RELEASE year(yyyy): ");
                    string input = Console.ReadLine();
                    if (input.Length != 0 && input.Length == 4 && IsDigitsOnly(input) && int.Parse(input) > 0)
                    {
                        releasedYear = input;
                        break;
                    }
                }

                int copies = 0;
                while (true)
                {
                    Console.Write("Enter amount COPIES: ");
                    string input = Console.ReadLine();
                    if (input.Length != 0 && IsDigitsOnly(input) && int.Parse(input) > 0)
                    {
                        int.TryParse(input, out copies);
                        break;
                    }
                }
                Film newFilm = new Film(title, genre, copies, director, releasedYear);
                FilmRepository.CreateFilm(newFilm);
                Console.WriteLine($"\n{newFilm.Copies} Copies of '{newFilm.Title}' created SUCCESSFULLY");
            }
            PressKeyToGoBackToStart();
        }

        /// <summary>
        /// Edits amount copies in db
        /// </summary>
        private void EditItem()
        {
            Console.Clear();

            Console.WriteLine("Edit COPIES an ITEM");
            Console.WriteLine("-----------------\n");
            Console.WriteLine("\n** Select ITEM category **");

            string itemType = "";
            while (itemType.ToLower() != "b" && itemType.ToLower() != "f")
            {
                Console.Write("Select (b)ook or (f)ilm: ");
                itemType = Console.ReadLine();
            }

            if (itemType.ToLower() == "b")
            {
                Console.Clear();
                Console.WriteLine("\nEdit COPIES OF BOOK");
                Console.WriteLine("-------------\n");

                List<Book> books = ShowBooks();

                Console.Write("\nEnter BOOK ID you want to EDIT: ");
                string bookId = Console.ReadLine();

                while (true)
                {
                    if (bookId.Length != 0 && IsDigitsOnly(bookId) && int.Parse(bookId) > 0 && int.Parse(bookId) <= books.Count)
                    {
                        int id = int.Parse(bookId) - 1;

                        Console.Write("\nHow many copies do you have now: ");
                        string input = Console.ReadLine();
                        if (input.Length != 0 && IsDigitsOnly(input))
                        {
                            int newCopies = int.Parse(input);
                            BookRepository.UpdateCopies(typeof(Book), books[id].Id, newCopies);
                            Console.WriteLine($"\nCopies of '{books[id].Title}' updated SUCCESSFULLY to {newCopies} copies");
                            break;
                        }

                    }
                }
            }
            else if (itemType.ToLower() == "f")
            {
                Console.Clear();
                Console.WriteLine("Edit COPIES OF FILM");
                Console.WriteLine("-------------\n");

                List<Film> films = ShowFilms();

                Console.Write("\nEnter FILM ID you want to EDIT: ");
                string filmId = Console.ReadLine();

                while (true)
                {
                    if (filmId.Length != 0 && IsDigitsOnly(filmId) && int.Parse(filmId) > 0 && int.Parse(filmId) <= films.Count)
                    {
                        int id = int.Parse(filmId) - 1;

                        Console.Write("\nHow many copies do you have now: ");
                        string input = Console.ReadLine();
                        if (input.Length != 0 && IsDigitsOnly(input))
                        {
                            int newCopies = int.Parse(input);
                            FilmRepository.UpdateCopies(typeof(Film), films[id].Id, newCopies);
                            Console.WriteLine($"\nCopies of '{films[id].Title}' updated SUCCESSFULLY to {newCopies} copies");
                            break;
                        }
                    }
                }
            }
            PressKeyToGoBackToStart();
        }

        /// <summary>
        /// Shows all saved items in db 
        /// </summary>
        private void ShowItems()
        {
            Console.Clear();
            Console.WriteLine("Show all ITEMS");
            Console.WriteLine("----------------\n");
            Console.WriteLine("List of all items SORTED by TITLE name\n");

            ShowBooks();

            Console.WriteLine();

            ShowFilms();

            PressKeyToGoBackToStart();
        }

        /// <summary>
        /// Deleted an item from db
        /// </summary>
        private void DeleteItems()
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("Delete an ITEM");
            Console.WriteLine("--------------\n");
            string itemType = "";
            while (itemType.ToLower() != "b" && itemType.ToLower() != "f")
            {
                Console.Write("Select (b)ook or (f)ilm: ");
                itemType = Console.ReadLine();
            }

            if (itemType.ToLower() == "b")
            {
                Console.Clear();
                Console.WriteLine("Delete a BOOK");
                Console.WriteLine("-------------\n");

                List<Book> books = ShowBooks();

                while (true)
                {
                    Console.Write("\nEnter ID of BOOK to delete: ");
                    string bookId = Console.ReadLine();

                    if (bookId.Length != 0 && IsDigitsOnly(bookId) && int.Parse(bookId) > 0 && int.Parse(bookId) <= books.Count)
                    {
                        int id = int.Parse(bookId) - 1;
                        BookRepository.DeleteBookById(books[id].Id);
                        Console.WriteLine($"\n** '{books[id].Title}' deleted SUCCESSFULLY **");
                        break;
                    }
                }
            }
            else if (itemType.ToLower() == "f")
            {
                Console.Clear();
                Console.WriteLine("Delete a FILM");
                Console.WriteLine("-----------------\n");

                List<Film> films = ShowFilms();

                while (true)
                {
                    Console.Write("\nEnter ID of FILM to delete: ");
                    string filmId = Console.ReadLine();

                    if (filmId.Length != 0 && IsDigitsOnly(filmId) && int.Parse(filmId) > 0 && int.Parse(filmId) <= films.Count)
                    {
                        int id = int.Parse(filmId) - 1;
                        FilmRepository.DeleteFilmById(films[id].Id);
                        Console.WriteLine($"\n** {films[id].Title} deleted SUCCESSFULLY **");
                        break;
                    }
                }
            }

            PressKeyToGoBackToStart();
        }

        /// <summary>
        /// creates an member object and saves in db
        /// </summary>
        private void CreateMember()
        {
            Console.Clear();
            Console.WriteLine("Create a new MEMBER");
            Console.WriteLine("-------------------\n");

            string name="";
            while (true) //Checks if entered name is unique 
            {
                Console.Write("Enter USER NAME(user name must be unique): ");
                name = Console.ReadLine();
                if (name.Length!=0 && MemberRepository.IfNameIsUnique(name))
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

            string password = "";
            while (password == "")
            {
                Console.Write("Enter a password: ");
                password = Console.ReadLine();
            }


            Member member = new Member(name, address, telNumber,password);

            MemberRepository.CreateMember(member);
            Console.WriteLine("\n** Member created SUCCESSFULLY **");

            PressKeyToGoBackToStart();
        }

        /// <summary>
        /// Gets a list of all members from db and Shows and returns the list
        /// </summary>
        /// <returns>returns a list of all members in db</returns>
        private List<Member> ShowMembers()
        {
            List<Member> members = MemberRepository.GetMembers();

            Console.Clear();
            Console.WriteLine($"List of all {members.Count} MEMBERS in library");
            Console.WriteLine("--------------------------------\n");

            if (members.Count == 0)
            {
                Console.WriteLine("There is NO MEMBER registered");
                PressKeyToGoBackToStart();
            }
            else
            {
                for (int i = 0; i < members.Count; i++)
                {
                    int j = i + 1;
                    Console.WriteLine($"[{j}][User Name]: {members[i].Name} [Address]: {members[i].Address} [Mobile Number]: {members[i].TelNumber}");
                }
            }
            return members;
        }

        /// <summary>
        /// Gets a member and deletes from db
        /// </summary>
        private void DeleteMember()
        {
            Console.Clear();
            List<Member> members = ShowMembers();

            Console.WriteLine("\nDelete a MEMBER");
            Console.WriteLine("---------------\n");

            while (true)
            {
                Console.Write("Enter member's ID to delete: ");
                string inputedId = Console.ReadLine();

                if (inputedId.Length != 0 && IsDigitsOnly(inputedId) && int.Parse(inputedId) > 0 && int.Parse(inputedId) <= members.Count)
                {
                    int id = int.Parse(inputedId) - 1;
                    MemberRepository.DeleteMemberById(members[id].Id);
                    Console.WriteLine("\n** Member deleted SUCCESSFULLY **");
                    break;
                }
            }
            PressKeyToGoBackToStart();
        }

        /// <summary>
        /// Creates an rent object and saves in data base
        /// </summary>
        private void RentItem()
        {
            Console.Clear();
            Console.WriteLine("Rent an ITEM");
            Console.WriteLine("------------\n");

            List<Member> members = ShowMembers();
            int id;
            while (true)
            {
                Console.Write("\nEnter member's ID to start rent: ");
                string inputedMemberId = Console.ReadLine();

                if (inputedMemberId.Length != 0 && IsDigitsOnly(inputedMemberId) && int.Parse(inputedMemberId) > 0 && int.Parse(inputedMemberId) <= members.Count)
                {
                    id = int.Parse(inputedMemberId) - 1;
                    break;
                }
            }

            Member rentingMember = members[id]; //Creates renting member to rent

            Console.WriteLine("\n** Select ITEM category **");

            string itemType = "";
            while (itemType.ToLower() != "b" && itemType.ToLower() != "f")
            {
                Console.Write("Select (b)ook or (f)ilm: ");
                itemType = Console.ReadLine();
            }

            if (itemType.ToLower() == "b")//If item to rent is a book
            {
                Console.Clear();
                Console.WriteLine($"You are goting to rent a BOOK to '{rentingMember.Name}'\n");

                List<Book> books = ShowBooks();

                if (books.Count==0)
                {
                    PressKeyToGoBackToRentMenu();
                }

                int bookId;
                while (true)
                {
                    Console.Write("\nEnter BOOK ID you want to rent: ");
                    string inputBookId = Console.ReadLine();
                    if (inputBookId.Length != 0 && IsDigitsOnly(inputBookId) && int.Parse(inputBookId) > 0 && int.Parse(inputBookId) <= books.Count)
                    {
                        bookId = int.Parse(inputBookId) - 1;
                        break;
                    }
                }

                Book rentingBook = books[bookId];//Creates renting book to rent

                List<Rent> AllrentedThisBook = RentRepository.GetAllSameBookRented(rentingBook);

                foreach (Rent thisBookRent in AllrentedThisBook)
                {
                    Console.WriteLine($"{thisBookRent.RentedBook.Title} is rented from {thisBookRent.StartDate} to {thisBookRent.EndDate}");
                }

                DateTime startDate = GetStartDate();
                DateTime endDate = GetEndDate(startDate);
                
                Rent newRent = new Rent(rentingMember, rentingBook, null, startDate, endDate); //Creates a new rent object

                if (BookRepository.BookIsFreeToRent(newRent))
                {
                    RentRepository.CreateRent(newRent);//Inserts a rent in db
                    Console.WriteLine($"\n'{rentingMember.Name}' rented '{rentingBook.Title}' SUCCESSFULLY");
                }
                else
                {
                    Console.WriteLine("\n** All copies of this book are rented out between these entered dates **");
                }
            }



            else if (itemType.ToLower() == "f")//If item to rent is a film
            {
                Console.Clear();
                Console.WriteLine($"You are goting to rent a Film to '{rentingMember.Name}'\n");

                List<Film> films = ShowFilms();

                if (films.Count == 0)
                {
                    PressKeyToGoBackToRentMenu();
                }

                int filmId;
                while (true)
                {
                    Console.Write("\nEnter FILM ID you want to rent: ");
                    string inputFilmId = Console.ReadLine();
                    if (inputFilmId.Length != 0 && IsDigitsOnly(inputFilmId) && int.Parse(inputFilmId) > 0 && int.Parse(inputFilmId) <= films.Count)
                    {
                        filmId = int.Parse(inputFilmId) - 1;
                        break;
                    }
                }

                Film rentingFilm = films[filmId];//Creates renting book to rent

                List<Rent> AllrentedThisFilm = RentRepository.GetAllSameFilmRented(rentingFilm);

                foreach (Rent thisFilmRent in AllrentedThisFilm)
                {
                    Console.WriteLine($"{thisFilmRent.RentedFilm.Title} is rented from {thisFilmRent.StartDate} to {thisFilmRent.EndDate}");
                }

                DateTime startDate = GetStartDate();
                DateTime endDate = GetEndDate(startDate);

                Rent newRent = new Rent(rentingMember, null, rentingFilm, startDate, endDate);

                if (FilmRepository.FilmIsFreeToRent(newRent))
                {
                    RentRepository.CreateRent(newRent);
                    Console.WriteLine($"\n'{rentingMember.Name}' rented '{rentingFilm.Title}' SUCCESSFULLY");
                }
                else
                {
                    Console.WriteLine("\n** All copies of this book are rented out between these entered dates **");
                }
            }
            PressKeyToGoBackToRentMenu();
        }

        /// <summary>
        /// Gets all rents for a member from db and shows
        /// </summary>
        /// <returns>A list of members rents</returns>
        private List<Rent> ShowRents()
        {
            Console.Clear();
            Console.WriteLine("SHOWING RENTS");
            Console.WriteLine("---------------------\n");

            List<Member> members = ShowMembers();
            int id;
            while (true)
            {
                Console.Write("\nEnter member's ID: ");
                string inputedMemberId = Console.ReadLine();

                if (inputedMemberId.Length != 0 && IsDigitsOnly(inputedMemberId) && int.Parse(inputedMemberId) > 0 && int.Parse(inputedMemberId) <= members.Count)
                {
                    id = int.Parse(inputedMemberId) - 1;
                    break;
                }
            }

            Member member = members[id]; //Creates member to show rents of this member

            Console.Clear();
            Console.WriteLine($"SHOWING {member.Name}'s RENTS");
            Console.WriteLine("----------------------------------\n");

            List<Rent> memberRents = ShowRentsByMemberId(member.Id);

            return memberRents;
        }

        /// <summary>
        /// gets a rented item and delete it from db
        /// </summary>
        private void ReturnItem()
        {
            List<Rent> memberRents = ShowRents();

            int rentIdToReturn;
            while (true)
            {
                Console.Write("\nEnter RENT ID you want to DELETE: ");
                string inputRentId = Console.ReadLine();
                if (inputRentId.Length != 0 && IsDigitsOnly(inputRentId) && int.Parse(inputRentId) > 0 && int.Parse(inputRentId) <= memberRents.Count)
                {
                    rentIdToReturn = int.Parse(inputRentId) - 1;
                    break;
                }
            }

            RentRepository.DeleteRentById(memberRents[rentIdToReturn].Id);

            Console.WriteLine($"\nItem returned SUCCESSFULLY");

            PressKeyToGoBackToRentMenu();
        }


        //----------------------------------------------------------- HELP METHODS -------------------------------------------------------------------

        /// <summary>
        /// Gets all books from db and shows
        /// </summary>
        /// <returns>A list of all books in db</returns>
        private List<Book> ShowBooks()
        {
            List<Book> books = BookRepository.GetBooks();

            Repository.Models.SortBooksByTitle sortBooksByTitle = new SortBooksByTitle();
            books.Sort(sortBooksByTitle);

            Console.WriteLine("------------------------------");
            Console.WriteLine($"List of all {books.Count} BOOKS in library");
            Console.WriteLine("------------------------------");

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
            return books;
        }

        /// <summary>
        /// Gets all films from db and shows
        /// </summary>
        /// <returns>A list of all films in db</returns>
        private List<Film> ShowFilms()
        {
            List<Film> films = FilmRepository.GetFilms();

            SortFilmsByTitle sortFilmsByTitle = new SortFilmsByTitle();
            films.Sort(sortFilmsByTitle);

            Console.WriteLine("------------------------------");
            Console.WriteLine($"List of all {films.Count} FILMS in library");
            Console.WriteLine("------------------------------");

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
            return films;
        }

        /// <summary>
        /// Gets member's all rents from db and shows
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of member's rents</returns>
        private List<Rent> ShowRentsByMemberId(ObjectId id)
        {
            List<Rent> rents = RentRepository.GetRentsByMemberId(id);

            if (rents.Count == 0)
            {
                Console.WriteLine("\nThere is no rents registered");
                PressKeyToGoBackToRentMenu();
            }
            else
            {
                for (int i = 0; i < rents.Count; i++)
                {
                    int j = i;
                    if (rents[i].RentedBook != null)
                    {
                        Console.WriteLine($"[{++j}][Book] [Title]: {rents[i].RentedBook.Title} is rented from {rents[i].StartDate} until {rents[i].EndDate}");
                    }
                    else
                    {
                        Console.WriteLine($"[{++j}][Film] [Title]: {rents[i].RentedFilm.Title} is rented from {rents[i].StartDate} until {rents[i].EndDate}");
                    }
                }
            }

            return rents;
        }

        /// <summary>
        /// Gets a date and 
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private DateTime GetEndDate(DateTime startDate)
        {
            DateTime endDate = DateTime.Now;
            while (true)
            {
                int endDateResultat = -2;
                try
                {
                    Console.Write("\nEnter rent's END DATE(yyyy,mm,dd): ");
                    endDate = DateTime.Parse(Console.ReadLine()).Date;
                    endDateResultat = DateTime.Compare(endDate, startDate);//Checks to not enter an end date before start date
                }
                catch (Exception)
                {
                    Console.WriteLine("You entered a WRONG  DATE FORMAT. Must be yyyy/mm/dd");
                }

                if (endDateResultat > 0)//checks if date is in right format and end date is not before start date
                {
                    Console.WriteLine($"Rent end date is {endDate}");
                    return endDate;
                }
                else
                {
                    Console.WriteLine("Don't Entere a date before rent's start date");
                }
            }
        }

        /// <summary>
        /// Gets a date, checks if is correct and returns it
        /// </summary>
        /// <returns>A date</returns>
        private DateTime GetStartDate()
        {
            DateTime startDate = DateTime.Now;
            while (true)
            {
                int startDateResultat = 2;
                try
                {
                    Console.Write("\nEnter rent's START DATE(yyyy,mm,dd): ");
                    startDate = DateTime.Parse(Console.ReadLine()).Date;
                    startDateResultat = DateTime.Compare(DateTime.Now.Date, startDate);//Checks to not enter a date before today's date
                }
                catch (Exception)
                {
                    Console.WriteLine("You entered a WRONG  DATE FORMAT. Must be yyyy/mm/dd");
                }

                if (startDateResultat <= 0)//checks if date is in right format and start date is not before today's date
                {
                    Console.WriteLine($"Rent start date is {startDate}");
                    return startDate;
                }
                else if (startDateResultat > 0)
                {
                    Console.WriteLine("Don't Entere a date before Today's date");
                }
            }
        }

        

        /// <summary>
        /// Goes back to Start menu
        /// </summary>
        private void PressKeyToGoBackToStart()
        {
            Console.Write("\nPress any key to go back to MAIN MENU ...");
            Console.ReadKey();
            Start();
        }

        /// <summary>
        /// Goes back to Main menu
        /// </summary>
        private void PressKeyToGoBackToRentMenu()
        {
            Console.Write("\nPress any key to go back to MAIN MENU ...");
            Console.ReadKey();
            RentMenu();
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

    }
}
