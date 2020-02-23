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
        // GET: Rents
        public ActionResult Index()
        {
            List<Rent> rents = RentRepository.GetRents();
            return View(rents);
        }

        public ActionResult MemberRents(string id)
        {
            ObjectId memberId = new ObjectId(id);
            List<Rent> rents = RentRepository.GetRentsByMemberId(memberId);
            return View(rents);
        }

        [Route("/Rents/RentBook/{id}")]
        public ActionResult RentBook(string id)
        {
            List<Book> books = BookRepository.GetBooks();
            return View(books);
        }

        [HttpPost]
        [Route("/Rents/RentBook/{id}")]
        public ActionResult RentBook(string id, string bookId, DateTime startDate, DateTime endDate)
        {
            List<Book> books = BookRepository.GetBooks();

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

        [Route("/Rents/RentFilm/{id}")]
        public ActionResult RentFilm()
        {
            List<Film> films = FilmRepository.GetFilms();
            return View(films);
        }

        [HttpPost]
        [Route("/Rents/RentFilm/{id}")]
        public ActionResult RentFilm(string id, string filmId, DateTime startDate, DateTime endDate)
        {
            List<Film> films = FilmRepository.GetFilms();

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

        // GET: Rents/Details/5
        public ActionResult Details(string id)
        {
            ObjectId retnId = new ObjectId(id);
            Rent rent = RentRepository.GetRentById(retnId);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public ActionResult Delete(string id)
        {
            ObjectId retnId = new ObjectId(id);
            Rent rent = RentRepository.GetRentById(retnId);
            return View(rent);
        }

        // POST: Rents/Delete/5
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