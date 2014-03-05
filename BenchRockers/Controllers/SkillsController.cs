using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BenchRockers.Models;

namespace BenchRockers.Controllers
{
    public class SkillsController : Controller
    {
        private BenchRockersDbContext db = new BenchRockersDbContext();

        //
        // GET: /Skills/

        public ActionResult Index()
        {
            return View(db.Skill.ToList());
        }

        public ActionResult Skills()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SkillsList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var skillCount = db.Skill.Count();
                var skills = GetSkills(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = skills, TotalRecordCount = skillCount});
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public List<Skills> GetSkills(int startIndex, int count, string sorting)
        {
            IEnumerable<Skills> query = db.Skill;

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(s => s.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(s => s.Name);
            }
            else
            {
                query = query.OrderBy(s => s.Name); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        [HttpPost]
        public JsonResult CreateSkill(Skills skill)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var addedSkill = db.Skill.Add(skill);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = addedSkill });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateSkill(Skills skill)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteSkill(int skillId)
        {
            try
            {
                Thread.Sleep(50);
                Skills skill = db.Skill.Find(skillId);
                db.Skill.Remove(skill);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public List<Skills> GetAllSkills()
        {
            return db.Skill.OrderBy(s => s.Name).ToList();
        }


        //
        // GET: /Skills/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Skills skills = db.Skill.Find(id);
        //    if (skills == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(skills);
        //}

        //
        // GET: /Skills/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //
        // POST: /Skills/Create

        //[HttpPost]
        //public ActionResult Create(Skills skills)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Skill.Add(skills);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(skills);
        //}

        //
        // GET: /Skills/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Skills skills = db.Skill.Find(id);
        //    if (skills == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(skills);
        //}

        //
        // POST: /Skills/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Skills skills)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(skills).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(skills);
        //}

        //
        // GET: /Skills/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Skills skills = db.Skill.Find(id);
        //    if (skills == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(skills);
        //}

        //
        // POST: /Skills/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Skills skills = db.Skill.Find(id);
        //    db.Skill.Remove(skills);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}