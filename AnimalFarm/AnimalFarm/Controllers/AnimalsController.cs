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
        public IActionResult Index()
        {
            Database db = new Database();
            List<Animal> animals = db.GetAnimals(); 

            return View(animals);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            ObjectId animalId = new ObjectId(id);
            Database db = new Database();
            db.DeleteAnimalById(animalId);

            return Redirect("/Animals");
        }

        public IActionResult Edit(string id)
        {
            ObjectId animalId = new ObjectId(id);
            Database db = new Database();
            Animal animal = db.GetAnimalById(animalId);

            return View(animal);
        }

        [HttpPost]
        public IActionResult Edit(string id, string name,string species,int age)
        {
            ObjectId animalId = new ObjectId(id);
            Database db = new Database();
            db.UpdateAnimal(animalId, name, species, age);

            return Redirect($"/Animals/Show/{id}");
        }
        

        public IActionResult Show(string id) 
        {
            ObjectId animalId = new ObjectId(id);
            Database db = new Database();
            Animal animal= db.GetAnimalById(animalId);

            return View(animal);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string species, int age)
        {
            Database db = new Database();
            db.SaveAnimal(new Animal()
            {
                Name = name,
                Species = species,
                Age=age
            });
            return Redirect("/Animals");
        }
    }
}