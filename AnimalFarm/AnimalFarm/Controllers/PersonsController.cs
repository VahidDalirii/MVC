using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalFarm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AnimalFarm.Controllers
{
    public class PersonsController : Controller
    {
        // GET: Persons
        public ActionResult Index()
        {
            Database db = new Database();
            List<Person> persons = db.GetPersons();

            return View(persons);
        }

        // GET: Persons/Details/5
        public ActionResult Details(string id)
        {
            Database db = new Database();
            ObjectId personId = new ObjectId(id);

            Person person = db.GetPersonById(personId);
            return View(person);
        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        [HttpPost]
        public ActionResult Create(string name, string ssn)
        {
            Person person = new Person() { Name = name, SSN = ssn };

            Database db = new Database();
            db.SavePerson(person);

            return Redirect("/Persons");
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(string id)
        {
            Database db = new Database();
            ObjectId personId = new ObjectId(id);

            Person person = db.GetPersonById(personId);
            return View(person);
        }

        // POST: Persons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Person person)
        {
            try
            {
                Database db = new Database();
                ObjectId personId = new ObjectId(id);
                person.Id = personId;

                db.UpdatePerson(person);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(string id)
        {
            Database db = new Database();
            ObjectId personId = new ObjectId(id);
            Person person = db.GetPersonById(personId);

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Database db = new Database();
                ObjectId personId = new ObjectId(id);
                db.DeletePersonById(personId);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete");
            }
        }
    }
}