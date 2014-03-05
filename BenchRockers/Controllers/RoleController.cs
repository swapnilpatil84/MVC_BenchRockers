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
    public class RoleController : Controller
    {
        private BenchRockersDbContext db = new BenchRockersDbContext();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        public ActionResult Roles()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RolesList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var roleCount = db.Roles.Count();
                var roles = GetRoles(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = roles, TotalRecordCount = roleCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public List<Roles> GetRoles(int startIndex, int count, string sorting)
        {
            IEnumerable<Roles> query = db.Roles;

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(r => r.RoleName);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(r => r.RoleName);
            }
            else
            {
                query = query.OrderBy(r => r.RoleName); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        [HttpPost]
        public JsonResult CreateRole(Roles role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var addedRole = db.Roles.Add(role);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = addedRole });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateRole(Roles role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteRole(int roleId)
        {
            try
            {
                Thread.Sleep(50);
                Roles role = db.Roles.Find(roleId);
                db.Roles.Remove(role);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public List<Roles> GetAllRoles()
        {
            return db.Roles.OrderBy(r => r.RoleName).ToList();
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id = 0)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roles);
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles roles = db.Roles.Find(id);
            db.Roles.Remove(roles);
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