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

        internal Person GetPersonById(ObjectId personId)
        {
            var collection = _database.GetCollection<Person>(PERSONS_COLLECTION);
            return collection.Find(p => p.Id == personId).FirstOrDefault();
        }

        internal void DeletePersonById(ObjectId personId)
        {
            var animalCollection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);
            UpdateDefinition<Animal> update = Builders<Animal>.Update
                .Set(a => a.OwnerId, null);
            animalCollection.UpdateMany(a => a.OwnerId == personId,update);

            var personCollection = _database.GetCollection<Person>(PERSONS_COLLECTION);
            personCollection.DeleteOne(p => p.Id == personId);
        }

        /// <summary>
        /// Saves an animal in the db
        /// </summary>
        /// <param name="animal">The animal to save</param>
        internal void SaveAnimal(Animal animal)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);
            collection.InsertOne(animal);
        }

        internal void UpdatePerson(Person person)
        {
            var collection = _database.GetCollection<Person>(PERSONS_COLLECTION);
            var update = Builders<Person>.Update
                .Set("Name", person.Name);

            collection.UpdateOne(p => p.Id == person.Id, update);

        }


        /// <summary>
        /// Gets an animal from the db
        /// </summary>
        /// <param name="animalId">The id of the animal to get</param>
        /// <returns>An animal object</returns>
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

        /// <summary>
        /// Gets all animals from the db
        /// </summary>
        /// <returns>A list of animals</returns>
        public List<Animal> GetAnimals()
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);

            var findResult = collection.Find(m => true);

            return findResult.ToList();
        }

        /// <summary>
        /// Updates an animal in the db
        /// </summary>
        /// <param name="id">The id of the animal to update</param>
        /// <param name="name">The new name of the animal</param>
        /// <param name="species">The new species of the animal</param>
        /// <param name="age">The new age of the animal</param>
        internal void UpdateAnimal(ObjectId id, string name, string species, int age)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);

            UpdateDefinition<Animal> update = Builders<Animal>.Update
                .Set(a => a.Name, name)
                .Set(a => a.Species, species)
                .Set(a => a.Age, age);

            collection.UpdateOne(a => a.Id == id, update);
        }

        internal void SetAnimalOwner(ObjectId id, ObjectId ownerId)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);

            UpdateDefinition<Animal> update = Builders<Animal>.Update
                .Set(a => a.OwnerId, ownerId);

            collection.UpdateOne(a => a.Id == id, update);
        }

        /// <summary>
        /// Deletes an animal from the db
        /// </summary>
        /// <param name="id">The id of the animal to delete</param>
        internal void DeleteAnimalById(ObjectId id)
        {
            var collection = _database.GetCollection<Animal>(ANIMALS_COLLECTION);
            collection.DeleteOne(a => a.Id == id);
        }
    }
}
