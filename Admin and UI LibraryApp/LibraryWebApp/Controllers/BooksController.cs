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
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            List<Book> books = BookRepository.GetBooks();
            Repository.Models.SortBooksByTitle sortBooksByTitle = new SortBooksByTitle();
            books.Sort(sortBooksByTitle);
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string title, string genre, string author, string publishedYear, int copies)
        {
            try
            {
                Book book = new Book(title, genre, copies, author, publishedYear);
                BookRepository.CreateBook(book);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string title, string genre, string author, string publishedYear, int copies)
        {
            try
            {
                ObjectId bookId = new ObjectId(id);
                Book book = new Book(title, genre, copies, author, publishedYear);
                book.Id = bookId;
                BookRepository.UpdateBook(book);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ObjectId bookId = new ObjectId(id);
                BookRepository.DeleteBookById(bookId);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}