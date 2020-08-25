using MongoDB.Driver;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository
    {
        public static void CreateUser(string username, string password)
        {
            User user = new User()
            {
                Username = username,
                Password = password
            };

            var db = new Database();
            db.UserCollection.InsertOne(user);
        }

        public static List<User> GetUsers()
        {
            Database db = new Database();
            return db.UserCollection.Find(u => true).ToList();
        }

        public static List<User> TestLogin(string username)
        {
            Database db = new Database();
            return db.UserCollection.Find(u => u.Username == username).ToList();
        }

        public static List<User> GetUsersWithSameUsername(string username)
        {
            Database db = new Database();
            return db.UserCollection.Find(u => u.Username.ToLower() == username.ToLower()).ToList();
        }

        public static void DeleteAllUsers()
        {
            Database db = new Database();
            db.UserCollection.DeleteMany(u => true);
        }

        public static void SaveManyUsers(List<User> users)
        {
            Database db = new Database();
            db.UserCollection.InsertMany(users);
        }

        
        public static string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
