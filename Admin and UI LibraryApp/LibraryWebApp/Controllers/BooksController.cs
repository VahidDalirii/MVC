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
        /// <summary>
        /// Gets a list of all books, sorts the list and returns it to view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Book> books = BookRepository.GetBooks();
            Repository.Models.SortBooksByTitle sortBooksByTitle = new SortBooksByTitle();
            books.Sort(sortBooksByTitle);
            return View(books);
        }

        /// <summary>
        /// Gets a book by book id and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a book to view</returns>
        public ActionResult Details(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            return View(book);
        }

        /// <summary>
        /// Shows create form
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Gets all value of a new book and creates it in db
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="author"></param>
        /// <param name="publishedYear"></param>
        /// <param name="copies"></param>
        /// <returns>Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string title, string genre, string author, string publishedYear, int copies)
        {
            try
            {
                if (title!=null)
                {
                    if (author!=null)
                    {
                        Book book = new Book(title, genre, copies, author, publishedYear);
                        BookRepository.CreateBook(book);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('You have to select an author name');</script>";
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
                return RedirectToAction("Edit");
            }
        }

        /// <summary>
        /// Gets a film by id from db and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            return View(book);
        }

        /// <summary>
        /// Gets new values of the book and updates it in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="author"></param>
        /// <param name="publishedYear"></param>
        /// <param name="copies"></param>
        /// <returns>to index</returns>
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

        /// <summary>
        /// Gets a film by id from db and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            ObjectId bookId = new ObjectId(id);
            Book book = BookRepository.GetBookById(bookId);
            return View(book);
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