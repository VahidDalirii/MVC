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
        // GET: Films
        public ActionResult Index()
        {
            List<Film> films = FilmRepository.GetFilms();
            Repository.Models.SortFilmsByTitle sortFilmsByTitle = new SortFilmsByTitle();
            films.Sort(sortFilmsByTitle);
            return View(films);
        }

        // GET: Films/Details/5
        public ActionResult Details(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string title, string genre, string director, string releasedYear, int copies)
        {
            try
            {
                Film film = new Film(title, genre, copies, director, releasedYear);
                FilmRepository.CreateFilm(film);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Films/Edit/5
        public ActionResult Edit(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            return View(film);
        }

        // POST: Films/Edit/5
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

        // GET: Films/Delete/5
        public ActionResult Delete(string id)
        {
            ObjectId filmId = new ObjectId(id);
            Film film = FilmRepository.GetFilmById(filmId);
            return View(film);
        }

        // POST: Films/Delete/5
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