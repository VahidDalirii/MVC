using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Repository;
using Repository.Models;

namespace LibraryWebApp.Controllers
{
    public class RentsController : Controller
    {
        /// <summary>
        /// Gets a list of all rents from db, sorts the list by member name and returns to view
        /// </summary>
        /// <returns>a list of all rents</returns>
        public ActionResult Index()
        {
            List<Rent> rents = RentRepository.GetRents();
            SortRentsByMemberName sortRentsByMemberName = new SortRentsByMemberName();
            rents.Sort(sortRentsByMemberName);
            return View(rents);
        }

        /// <summary>
        /// Gets all rents of a member by member id from db and returns to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MemberRents(string id)
        {
            ObjectId memberId = new ObjectId(id);
            List<Rent> rents = RentRepository.GetRentsByMemberId(memberId);
            return View(rents);
        }


        /// <summary>
        /// Gets a list of all rents of a book by book id from db, sorts the list and returns to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of all rents of a book</returns>
        [Route("/Rents/BookRents/{id}")]
        public ActionResult BookRents(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            List<Rent> rents = RentRepository.GetAllSameBookRented(book);

            return View(rents);
        }

        /// <summary>
        /// Gets a list of all rents of a film by film id from db, sorts the list and returns to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/Rents/FilmRents/{id}")]
        public ActionResult FilmRents(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            List<Rent> rents = RentRepository.GetAllSameFilmRented(film);

            return View(rents);
        }


        /// <summary>
        /// Gets a list of all books from db, sorts the list and returns to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of all </returns>
        [Route("/Rents/RentBook/{id}")]
        public ActionResult RentBook(string id)
        {
            List<Book> books = BookRepository.GetBooks();
            SortBooksByTitle sortBooksByTitle = new SortBooksByTitle();
            books.Sort(sortBooksByTitle);
            return View(books);
        }

        /// <summary>
        /// Gets value of all rent object, checks the dates and creates a rent if all is correct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>If dates are not correct return a list of books to view</returns>
        [HttpPost]
        [Route("/Rents/RentBook/{id}")]
        public ActionResult RentBook(string id, string bookId, DateTime startDate, DateTime endDate)
        {
            List<Book> books = BookRepository.GetBooks();
            SortBooksByTitle sortBooksByTitle = new SortBooksByTitle();
            books.Sort(sortBooksByTitle);

            if (RentRepository.IsStartDateCorrect(startDate))
            {
                if (RentRepository.IsEndDateCorrect(endDate,startDate))
                {
                    ObjectId memberId = new ObjectId(id);
                    Member member = MemberRepository.GetMemberById(memberId);

                    ObjectId rentingBookId = new ObjectId(bookId);
                    Book book = BookRepository.GetBookById(rentingBookId);

                    Rent rent = new Rent(member, book, null, startDate, endDate);

                    if (BookRepository.BookIsFreeToRent(rent))
                    {
                        RentRepository.CreateRent(rent);
                        return Redirect($"/Rents/MemberRents/{id}");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('This book is not free to Rent in this entered date period. Please try another dates');</script>";
                        return View(books);
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You entered a date before rent start date. Please try a date after rent start date');</script>";
                    return View(books);
                }
            }
            else
            {
                TempData["textmsg"] = "<script>alert('You entered a date before today date. Please try a date after today date');</script>";
                return View(books);
            }  
        }


        /// <summary>
        /// Gets a list of all films, sorts the list and returns to view
        /// </summary>
        /// <returns>A list of films</returns>
        [Route("/Rents/RentFilm/{id}")]
        public ActionResult RentFilm()
        {
            List<Film> films = FilmRepository.GetFilms();
            SortFilmsByTitle sortFilmsByTitle = new SortFilmsByTitle();
            films.Sort(sortFilmsByTitle);
            return View(films);
        }

        /// <summary>
        /// Gets value of all rent object, checks the dates and creates a rent if all is correct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filmId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>If dates are not correct return a list of films to view</returns>
        [HttpPost]
        [Route("/Rents/RentFilm/{id}")]
        public ActionResult RentFilm(string id, string filmId, DateTime startDate, DateTime endDate)
        {
            List<Film> films = FilmRepository.GetFilms();
            SortFilmsByTitle sortFilmsByTitle = new SortFilmsByTitle();
            films.Sort(sortFilmsByTitle);

            if (RentRepository.IsStartDateCorrect(startDate))
            {
                if (RentRepository.IsEndDateCorrect(endDate,startDate))
                {
                    ObjectId memberId = new ObjectId(id);
                    Member member = MemberRepository.GetMemberById(memberId);

                    ObjectId rentingFilmId = new ObjectId(filmId);
                    Film film = FilmRepository.GetFilmById(rentingFilmId);

                    Rent rent = new Rent(member, null, film, startDate, endDate);

                    if (FilmRepository.FilmIsFreeToRent(rent))
                    {

                        RentRepository.CreateRent(rent);
                        return Redirect($"/Rents/MemberRents/{id}");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('This film is not free to Rent in this entered date period. Please try another dates');</script>";
                        return View(films);
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You entered a date before rent start date. Please try a date after rent start date');</script>";
                    return View(films);
                }
            }
            else
            {
                TempData["textmsg"] = "<script>alert('You entered a date before today date. Please try a date after today date');</script>";
                return View(films);
            }  
        }


        /// <summary>
        /// Gets a list of all members from db, sorts the list and returns to view
        /// </summary>
        /// <returns> a list of all members </returns>
        [Route("/Rents/RentBookToMember/{id}")]
        public ActionResult RentBookToMember()
        {
            List<Member> members = MemberRepository.GetMembers();
            SortMemberByName sortMemberByName = new SortMemberByName();
            members.Sort(sortMemberByName);
            return View(members);
        }


        /// <summary>
        /// Gets value of all rent object, checks the dates and creates a rent if all is correct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>If dates are not correct return a list of members to view</returns>
        [HttpPost]
        [Route("/Rents/RentBookToMember/{id}")]
        public ActionResult RentBookToMember(string id, string memberId, DateTime startDate, DateTime endDate)
        {
            List<Member> members = MemberRepository.GetMembers();
            SortMemberByName sortMemberByName = new SortMemberByName();
            members.Sort(sortMemberByName);

            if (RentRepository.IsStartDateCorrect(startDate))
            {
                if (RentRepository.IsEndDateCorrect(endDate, startDate))
                {
                    ObjectId RentingMemberId = new ObjectId(memberId);
                    Member member = MemberRepository.GetMemberById(RentingMemberId);

                    ObjectId rentingBookId = new ObjectId(id);
                    Book book = BookRepository.GetBookById(rentingBookId);

                    Rent rent = new Rent(member, book, null, startDate, endDate);

                    if (BookRepository.BookIsFreeToRent(rent))
                    {
                        RentRepository.CreateRent(rent);
                        return Redirect($"/Rents/MemberRents/{memberId}");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('This book is not free to Rent in this entered date period. Please try another dates');</script>";
                        return View(members);
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You entered a date before rent start date. Please try a date after rent start date');</script>";
                    return View(members);
                }
            }
            else
            {
                TempData["textmsg"] = "<script>alert('You entered a date before today date. Please try a date after today date');</script>";
                return View(members);
            }
        }


        /// <summary>
        /// Gets a list of all members from db, sorts the list and returns to view
        /// </summary>
        /// <returns> a list of all members </returns>
        [Route("/Rents/RentFilmToMember/{id}")]
        public ActionResult RentFilmToMember()
        {
            List<Member> members = MemberRepository.GetMembers();
            SortMemberByName sortMemberByName = new SortMemberByName();
            members.Sort(sortMemberByName);
            return View(members);
        }

        /// <summary>
        /// Gets value of all rent object, checks the dates and creates a rent if all is correct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>If dates are not correct return a list of members to view</returns>
        [HttpPost]
        [Route("/Rents/RentFilmToMember/{id}")]
        public ActionResult RentFilmToMember(string id, string memberId, DateTime startDate, DateTime endDate)
        {
            List<Member> members = MemberRepository.GetMembers();
            SortMemberByName sortMemberByName = new SortMemberByName();
            members.Sort(sortMemberByName);

            if (RentRepository.IsStartDateCorrect(startDate))
            {
                if (RentRepository.IsEndDateCorrect(endDate, startDate))
                {
                    ObjectId RentingMemberId = new ObjectId(memberId);
                    Member member = MemberRepository.GetMemberById(RentingMemberId);

                    ObjectId rentingFilmId = new ObjectId(id);
                    Film film = FilmRepository.GetFilmById(rentingFilmId);

                    Rent rent = new Rent(member, null, film, startDate, endDate);

                    if (FilmRepository.FilmIsFreeToRent(rent))
                    {
                        RentRepository.CreateRent(rent);
                        return Redirect($"/Rents/MemberRents/{memberId}");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('This book is not free to Rent in this entered date period. Please try another dates');</script>";
                        return View(members);
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You entered a date before rent start date. Please try a date after rent start date');</script>";
                    return View(members);
                }
            }
            else
            {
                TempData["textmsg"] = "<script>alert('You entered a date before today date. Please try a date after today date');</script>";
                return View(members);
            }
        }

        /// <summary>
        /// Gets a rent by rent id from db and returns to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A rent as an object</returns>
        public ActionResult Details(string id)
        {
            ObjectId retnId = new ObjectId(id);
            Rent rent = RentRepository.GetRentById(retnId);
            return View(rent);
        }

        /// <summary>
        /// Gets a rent by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A rent as an object</returns>
        public ActionResult Delete(string id)
        {
            ObjectId retnId = new ObjectId(id);
            Rent rent = RentRepository.GetRentById(retnId);
            return View(rent);
        }

        /// <summary>
        /// Deletes a rent by rent id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>To index</returns>
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ObjectId retnId = new ObjectId(id);
                RentRepository.DeleteRentById(retnId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}