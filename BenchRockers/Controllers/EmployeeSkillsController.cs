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
    public class EmployeeSkillsController : Controller
    {
        private BenchRockersDbContext db = new BenchRockersDbContext();

        //
        // GET: /EmployeeSkills/

        public ActionResult Index()
        {
            var employeeskill = db.EmployeeSkill.Include(e => e.Employee).Include(e => e.Skills);
            return View(employeeskill.ToList());
        }

        public ActionResult EmployeeSkills()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EmployeeSkillsList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var skillCount = db.EmployeeSkill.Count();
                var skills = GetEmployeeSkills(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = skills, TotalRecordCount = skillCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public List<EmployeeSkills> GetEmployeeSkills(int startIndex, int count, string sorting)
        {
            IEnumerable<EmployeeSkills> query = db.EmployeeSkill;

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(es => es.Employee.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(es => es.Employee.Name);
            }
            else
            {
                query = query.OrderBy(es => es.Employee.Name); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        #region Skill actions

        [HttpPost]
        public JsonResult GetSkills()
        {
            try
            {
                var skills = new SkillsController().GetAllSkills().Select(s => new { DisplayText = s.Name, Value = s.SkillId }); ;
                return Json(new { Result = "OK", Options = skills });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        #region Employee actions

        [HttpPost]
        public JsonResult GetEmployees()
        {
            try
            {
                var employees = new EmployeeController().GetAllEmployees().Select(e => new { DisplayText = e.Name, Value = e.EmpId }); ;
                return Json(new { Result = "OK", Options = employees });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        //
        // GET: /EmployeeSkills/Details/5

        public ActionResult Details(int id = 0)
        {
            EmployeeSkills employeeskills = db.EmployeeSkill.Find(id);
            if (employeeskills == null)
            {
                return HttpNotFound();
            }
            return View(employeeskills);
        }

        //
        // GET: /EmployeeSkills/Create

        public ActionResult Create()
        {
            ViewBag.EmpId = new SelectList(db.Employees, "EmpId", "Name");
            ViewBag.SkillId = new SelectList(db.Skill, "SkillId", "Name");
            return View();
        }

        //
        // POST: /EmployeeSkills/Create

        [HttpPost]
        public ActionResult Create(EmployeeSkills employeeskills)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeSkill.Add(employeeskills);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpId = new SelectList(db.Employees, "EmpId", "Name", employeeskills.EmpId);
            ViewBag.SkillId = new SelectList(db.Skill, "SkillId", "Name", employeeskills.SkillId);
            return View(employeeskills);
        }

        //
        // GET: /EmployeeSkills/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EmployeeSkills employeeskills = db.EmployeeSkill.Find(id);
            if (employeeskills == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.Employees, "EmpId", "Name", employeeskills.EmpId);
            ViewBag.SkillId = new SelectList(db.Skill, "SkillId", "Name", employeeskills.SkillId);
            return View(employeeskills);
        }

        //
        // POST: /EmployeeSkills/Edit/5

        [HttpPost]
        public ActionResult Edit(EmployeeSkills employeeskills)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeskills).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.Employees, "EmpId", "Name", employeeskills.EmpId);
            ViewBag.SkillId = new SelectList(db.Skill, "SkillId", "Name", employeeskills.SkillId);
            return View(employeeskills);
        }

        //
        // GET: /EmployeeSkills/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EmployeeSkills employeeskills = db.EmployeeSkill.Find(id);
            if (employeeskills == null)
            {
                return HttpNotFound();
            }
            return View(employeeskills);
        }

        //
        // POST: /EmployeeSkills/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSkills employeeskills = db.EmployeeSkill.Find(id);
            db.EmployeeSkill.Remove(employeeskills);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}