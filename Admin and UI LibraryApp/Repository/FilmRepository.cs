﻿using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class FilmRepository
    {
        /// <summary>
        /// Gets a new film object and saves in db
        /// </summary>
        /// <param name="newFilm"></param>
        public static void CreateFilm(Film newFilm)
        {
            Database db = new Database();
            db.CreateFilm(newFilm);
        }
        
        /// <summary>
        /// Gets all films from db and returns them
        /// </summary>
        /// <returns>returns a list of all films</returns>
        public static List<Film> GetFilms()
        {
            Database db = new Database();
            List<Film> allFilms = db.GetFilms();
            return allFilms;
        }

        public static Film GetFilmById(ObjectId id)
        {
            Database db = new Database();
            return db.GetFilmById(id);
        }

        /// <summary>
        /// Updates amount vopies of an object in db
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="newCopies"></param>
        public static void UpdateCopies(Type type, ObjectId id, int newCopies)
        {
            Database db = new Database();
            db.UpdateCopies(typeof(Film), id, newCopies);
        }

        /// <summary>
        /// Gets a film id and deletes it from db
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteFilmById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteFilmById(id);
        }

        public static void UpdateFilm(Film film)
        {
            Database db = new Database();
            db.UpdateFilm(film);
        }

        /// <summary>
        /// Checks if film if free to rent between entered start and end rent's date 
        /// </summary>
        /// <param name="film"></param>
        /// <returns>True if film if free to rent and false if not</returns>
        public static bool FilmIsFreeToRent(Film film)
        {
            List<Rent> allRents = RentRepository.GetRents();
            List<Rent> allSameRentedFilm = RentRepository.GetAllSameFilmRented(film);
            int rentedCopies = 0;
            for (int i = 0; i < allRents.Count; i++)
            {
                if (allRents[i].RentedFilm != null && allRents[i].RentedFilm.Id == film.Id)
                {
                    for (int j = 0; j < allSameRentedFilm.Count; j++)
                    {
                        if (allRents[i].StartDate >= allSameRentedFilm[j].StartDate && allRents[i].StartDate <= allSameRentedFilm[j].EndDate)
                        {
                            rentedCopies++;
                        }
                    }
                }
            }
            if (rentedCopies > film.Copies)
            {
                return false;
            }
            return true;
        }
    }
}
