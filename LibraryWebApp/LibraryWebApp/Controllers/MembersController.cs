using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LibraryWebApp.Controllers
{
    public class MembersController : Controller
    {
        /// <summary>
        /// Gets all members from db, sorts the list and returns the list to view
        /// </summary>
        /// <returns>A list of members</returns>
        public ActionResult Index()
        {
            List<Member> members= MemberRepository.GetMembers();
            Repository.Models.SortMemberByName sortMemberByName = new SortMemberByName();
            members.Sort(sortMemberByName);
            return View(members);
        }

        /// <summary>
        /// Gets a member by id from db and returns to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A member</returns>
        public ActionResult Details(string id)
        {
            ObjectId memberId = new ObjectId(id);
            Member member = MemberRepository.GetMemberById(memberId);
            return View(member);
        }

        /// <summary>
        /// Show a cretae form
        /// </summary>
        /// <returns>The form view</returns>
        public ActionResult Create()
        {
            return View();
        }

         
        /// <summary>
        /// Gets all value of a member object and creates in db
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="telNumber"></param>
        /// <param name="password"></param>
        /// <returns>To index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string address, string telNumber, string password)
        {
            try
            {
                if (MemberRepository.IfNameIsUnique(name))
                {
                    if (password!=null)
                    {
                        Member member = new Member(name, address, telNumber, password);
                        MemberRepository.CreateMember(member);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["textmsg"] = "<script>alert('You have to select a password');</script>";
                        return View();
                    }  
                }
                else
                {
                    TempData["textmsg"] = "<script>alert('This name already exists as a member. Please try another name');</script>";
                    return View();
                }
                
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Gets a member by id from db and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A member as an object</returns>
        public ActionResult Edit(string id)
        {
            ObjectId memberId = new ObjectId(id);
            Member member = MemberRepository.GetMemberById(memberId);
            return View(member);
        }

        /// <summary>
        /// gets new value of member and updates it in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="telNumber"></param>
        /// <param name="password"></param>
        /// <returns>Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string name, string address, string telNumber, string password)
        {
            try
            {
                ObjectId memberId = new ObjectId(id);
                Member member = new Member(name, address, telNumber, password);
                member.Id = memberId;
                MemberRepository.UpdateMember(member);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Gets a member from db and returns it to view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            ObjectId memberId = new ObjectId(id);
            Member member = MemberRepository.GetMemberById(memberId);
            return View(member);
        }

        /// <summary>
        /// Deletes the member from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>to index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ObjectId memberId = new ObjectId(id);
                MemberRepository.DeleteMemberById(memberId);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete");
            }
        }
    }
}