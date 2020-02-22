using MongoDB.Bson;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class MemberRepository
    {
        /// <summary>
        /// Creates a new member in db
        /// </summary>
        /// <param name="newMember"></param>
        public static void CreateMember(Member newMember)
        {
            Database db = new Database();
            db.CreateMember(newMember);
        }

        /// <summary>
        /// Gets att members from db
        /// </summary>
        /// <returns>returens a list of all members in db</returns>
        public static List<Member> GetMembers()
        {
            Database db = new Database();
            List<Member> allMembers= db.GetMembers();
            return allMembers;
        }

        /// <summary>
        /// Gets a member id and deletes the member with this id from db
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteMemberById(ObjectId id)
        {
            Database db = new Database();
            db.DeleteMemberById(id);
        }

        /// <summary>
        /// Gets a updated member properties as an object and updates member in db
        /// </summary>
        /// <param name="updatedMember"></param>
        public static void UpdateMember(Member updatedMember)
        {
            Database db = new Database();
            db.UpdateMember(updatedMember);
        }

        /// <summary>
        /// Gets a new password and updates member's password in db
        /// </summary>
        /// <param name="member"></param>
        /// <param name="newPassword"></param>
        public static void ChangePassword(Member member,string newPassword)
        {
            Database db = new Database();
            db.ChangePassword(member, newPassword);
        }

        /// <summary>
        /// Gets a member by id and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A founded member</returns>
        public static Member GetMemberById(ObjectId id)
        {
            Database db = new Database();
            return db.GetMemberById(id);
        }

    }
}
