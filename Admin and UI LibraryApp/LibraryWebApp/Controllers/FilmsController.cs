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
    public class FilmsController : Controller
    {
        /// <summary>
        /// Gets all films from db, sorts the list and returns it to view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Film> films = FilmRepository.GetFilms();
            Repository.Models.SortFilmsByTitle sortFilmsByTitle = new SortFilmsByTitle();
            films.Sort(sortFilmsByTitle);
            return View(films);
        }

        /// <summary>
        /// Gets a film by id from db and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            return View(film);
        }

        /// <summary>
        /// Shows create form
        /// </summary>
        /// <returns>A form</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Gets all value of a film object and creates it in db
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="director"></param>
        /// <param name="releasedYear"></param>
        /// <param name="copies"></param>
        /// <returns>To index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string title, string genre, string director, string releasedYear, int copies)
        {
            try
            {
                if (title!=null)
                {
                    if (director!=null)
                    {
                        Film film = new Film(title, genre, copies, director, releasedYear);
                        FilmRepository.CreateFilm(film);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('You have to select a director name');</script>";
                        return View();
                    }
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('You have to select a title name');</script>";
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Gets a film by id and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A film as an  object</returns>
        public ActionResult Edit(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            return View(film);
        }

        /// <summary>
        /// Gets all new values of the film and updates it in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="director"></param>
        /// <param name="releasedYear"></param>
        /// <param name="copies"></param>
        /// <returns>To index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string title, string genre, string director, string releasedYear, int copies)
        {
            try
            {
                ObjectId filmId = new ObjectId(id);
                Film film = new Film(title, genre, copies, director, releasedYear);
                film.Id = filmId;
                FilmRepository.UpdateFilm(film);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        /// <summary>
        /// gets a film by film id and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            return View(film);
        }

        /// <summary>
        /// Deletes the film from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>To index</returns>
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ObjectId filmId = new ObjectId(id);
                FilmRepository.DeleteFilmById(filmId);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete");
            }
        }
    }
}