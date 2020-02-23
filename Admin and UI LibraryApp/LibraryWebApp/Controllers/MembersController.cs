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
        // GET: Members
        public ActionResult Index()
        {
            List<Member> members= MemberRepository.GetMembers();
            Repository.Models.SortMemberByName sortMemberByName = new SortMemberByName();
            members.Sort(sortMemberByName);
            return View(members);
        }

        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            ObjectId memberId = new ObjectId(id);
            Member member = MemberRepository.GetMemberById(memberId);
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string address, string telNumber, string password)
        {
            try
            {
                Member member = new Member(name, address, telNumber, password);
                MemberRepository.CreateMember(member);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            ObjectId memberId = new ObjectId(id);
            Member member = MemberRepository.GetMemberById(memberId);
            return View(member);
        }

        // POST: Members/Edit/5
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

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            ObjectId memberId = new ObjectId(id);
            Member member = MemberRepository.GetMemberById(memberId);
            return View(member);
        }

        // POST: Members/Delete/5
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