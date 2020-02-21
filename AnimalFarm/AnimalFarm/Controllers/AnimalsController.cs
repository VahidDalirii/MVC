using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalFarm.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AnimalFarm.Controllers
{
    public class AnimalsController : Controller
    {
        /// <summary>
        /// GET: /Animals
        /// Shows a list of all animals
        /// </summary>
        /// <returns>View with a list of all animals</returns>
        public IActionResult Index()
        {
            Database db = new Database();

            List<Animal> animals = db.GetAnimals();

            return View(animals);
        }

        /// <summary>
        /// GET: /Animals/Show/{id}
        /// Shows a specific animal
        /// </summary>
        /// <param name="id">THe id of the animal to show</param>
        /// <returns>A view showing the animal</returns>
        public IActionResult Show(string id)
        {
            ObjectId animalId = new ObjectId(id);

            Database db = new Database();
            Animal animal = db.GetAnimalById(animalId);

            return View(animal);
        }

        /// <summary>
        /// GET: /Animals/Create
        /// Shows a form for creating an animal
        /// </summary>
        /// <returns>A view for creating an animal</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /Animals/Create
        /// Creates an animal
        /// </summary>
        /// <param name="name">The name of the animal</param>
        /// <param name="species">THe anilam species</param>
        /// <param name="age">The age of the animal</param>
        /// <returns>A redirect to all animals list</returns>
        [HttpPost]
        public IActionResult Create(string name, string species, int age)
        {
            Database db = new Database();
            db.SaveAnimal(new Animal()
            {
                Name = name,
                Species = species,
                Age = age
            });

            return Redirect("/Animals");
        }
        
        /// <summary>
        /// GET: /Animals/Edit/{id}
        /// Shows a form for editing an animal
        /// </summary>
        /// <param name="id">The id of the animal to edit</param>
        /// <returns>A view with e prefilled form for editing the animal</returns>
        public IActionResult Edit(string id)
        {
            ObjectId animalId = new ObjectId(id);

            Database db = new Database();
            Animal animal = db.GetAnimalById(animalId);

            return View(animal);
        }

        /// <summary>
        /// POST: /Animals/Edit
        /// Updates an existing animal 
        /// </summary>
        /// <param name="id">The id of the animal to update</param>
        /// <param name="name">The new name of the animal</param>
        /// <param name="species">The new species of the animal</param>
        /// <param name="age">The new age of the animal</param>
        /// <returns>A redirect to the updated animal</returns>
        [HttpPost]
        public IActionResult Edit(string id, string name, string species, int age)
        {
            ObjectId animalId = new ObjectId(id);

            Database db = new Database();
            db.UpdateAnimal(animalId, name, species, age);

            return Redirect($"/Animals/Show/{id}");
        }

        /// <summary>
        /// POST: /Animals/Delete
        /// Deletes an existing animal
        /// </summary>
        /// <param name="id">The id of the animal to delete</param>
        /// <returns>A redirect to the list of all animals</returns>
        [HttpPost]
        public IActionResult Delete(string id)
        {
            ObjectId animalId = new ObjectId(id);

            Database db = new Database();
            db.DeleteAnimalById(animalId);

            return Redirect("/Animals");
        }

        [Route("/Animals/{id}/SetOwner")]
        public IActionResult SetOwner(string id)
        {
            Database db = new Database();
            List<Person> persons = db.GetPersons();
            return View(persons);
        }

        [HttpPost]
        [Route("/Animals/{id}/SetOwner")]
        public IActionResult SetOwner(string id, string personId)
        {
            ObjectId animalId = new ObjectId(id);
            ObjectId ownerId = new ObjectId(personId);

            Database db = new Database();

            db.SetAnimalOwner(animalId, ownerId);

            return Redirect("/Animals");
        }
    }
}