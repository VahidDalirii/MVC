using AnimalFarm.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalFarm
{
    public class Database
    {
        private const string ANIMALS_COLLECTION = "Animals";
        private const string PERSONS_COLLECTION = "Persons";

        private readonly IMongoDatabase _database;

        public Database(string dbName = "Animal-farm")
        {
            MongoClient dbClient = new MongoClient();

            _database = dbClient.GetDatabase(dbName);
        }

        internal List<Person> GetPersons()
        {
            var collection = _database.GetCollection<Person>(PERSONS_COLLECTION);

            var findResult = collection.Find(m => true);

            return findResult.ToList();
        }

        public void SaveAnimal(Animal animal)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);

            collection.InsertOne(animal);
        }

        internal void DeleteAnimalById(ObjectId animalId)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);
            collection.DeleteOne(a => a.Id == animalId);
        }

        internal Animal GetAnimalById(ObjectId animalId)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);
            Animal animal = collection.Find(a => a.Id == animalId).First();
            return animal;
        }

        internal void SavePerson(Person person)
        {
            var collection = _database.GetCollection<Person>(PERSONS_COLLECTION);
            collection.InsertOne(person);
        }

        internal void UpdateAnimal(ObjectId id, string name, string species, int age)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);
            UpdateDefinition<Animal> update = Builders<Animal>.Update
                .Set(a => a.Name, name)
                .Set(a => a.Species, species)
                .Set(a => a.Age, age);

            collection.UpdateOne(a => a.Id == id, update);
        }

        public List<Animal> GetAnimals()
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);

            var findResult = collection.Find(m => true);

            return findResult.ToList();
        }


    }
}
