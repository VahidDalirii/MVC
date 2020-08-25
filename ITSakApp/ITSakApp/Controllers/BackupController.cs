using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository;
using Repository.Models;

namespace ITSakApp.Controllers
{
    public class BackupController : Controller
    {
        private static string BackupFilePath = @"C:\Users\Vahid Daliri\Desktop\User backups";
        // GET: Backups
        public ActionResult Index()
        {
            string[] filesInPaths = Directory.GetFiles(BackupFilePath, "*.json");
            Backup backup = new Backup();
            backup.FileNames = new List<string>();

            foreach (var file in filesInPaths)
            {
                backup.FileNames.Add(Path.GetFileName(file));
            }
            return View(backup);
        }

        // GET: Backup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Backup/Create
        public ActionResult Create()
        {
            List<User> users = UserRepository.GetUsers();
            string jsonUsers = JsonConvert.SerializeObject(users);
            using (StreamWriter sw = new StreamWriter(BackupFilePath + "\\Backup " + DateTime.Now.ToString("dddd, dd MMMM yyyy, HHmm") + ".json", false))
            {
                sw.WriteLine(jsonUsers);
            }

            TempData["textmsg"] = "<script>alert('Backup created successfully.');</script>";
            return RedirectToAction(nameof(Index));
        }

        // POST: Backup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Backup/Edit/5
        public ActionResult Edit(string fileName)
        {
            string[] filePaths = Directory.GetFiles(BackupFilePath, "*.json");
            List<string> fileNames = new List<string>();

            for (int i = 0; i < filePaths.Length; i++)
            {
                fileNames.Add(Path.GetFileName(filePaths[i]));
            }

            var UsersDeserialized = "";
            for (int i = 0; i < fileNames.Count; i++)
            {
                if (fileNames[i] == fileName)
                {
                    using (StreamReader reader = new StreamReader(filePaths[i]))
                    {
                        UsersDeserialized = reader.ReadToEnd();
                        break;
                    }
                }
            }
            var restoreUsers = JsonConvert.DeserializeObject<List<User>>(UsersDeserialized);
            UserRepository.DeleteAllUsers();
            UserRepository.SaveManyUsers(restoreUsers);

            TempData["textmsg"] = "<script>alert('Backup restored successfully.');</script>";
            return RedirectToAction(nameof(Index));
        }

        // POST: Backup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Backup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Backup/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}