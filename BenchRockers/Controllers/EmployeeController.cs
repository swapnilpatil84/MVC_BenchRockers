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
    public class EmployeeController : Controller
    {
        private BenchRockersDbContext db = new BenchRockersDbContext();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Employees()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EmployeeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                var employeeCount = db.Employees.Count();
                var skills = GetEmployees(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = skills, TotalRecordCount = employeeCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public List<Employee> GetEmployees(int startIndex, int count, string sorting)
        {
            IEnumerable<Employee> query = db.Employees;

            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(e => e.Name);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(e => e.Name);
            }
            else if (sorting.Equals("RoleId ASC"))
            {
                query = query.OrderBy(e => e.RoleId);
            }
            else if (sorting.Equals("RoleId DESC"))
            {
                query = query.OrderByDescending(e => e.RoleId);
            }
            else if (sorting.Equals("Account ASC"))
            {
                query = query.OrderBy(e => e.Account);
            }
            else if (sorting.Equals("Account DESC"))
            {
                query = query.OrderByDescending(e => e.Account);
            }
            else if (sorting.Equals("TotalExp ASC"))
            {
                query = query.OrderBy(e => e.TotalExp);
            }
            else if (sorting.Equals("TotalExp DESC"))
            {
                query = query.OrderByDescending(e => e.TotalExp);
            }
            else if (sorting.Equals("Location ASC"))
            {
                query = query.OrderBy(e => e.Location);
            }
            else if (sorting.Equals("Location DESC"))
            {
                query = query.OrderByDescending(e => e.Location);
            }
            else
            {
                query = query.OrderBy(e => e.Name); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        [HttpPost]
        public JsonResult CreateEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var addedEmployee = db.Employees.Add(employee);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = addedEmployee });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteEmployee(int empId)
        {
            try
            {
                Thread.Sleep(50);
                Employee employee = db.Employees.Find(empId);
                employee.IsOnBench = false;
                //db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #region Role actions

        [HttpPost]
        public JsonResult GetRoles()
        {
            try
            {
                var roles = new RoleController().GetAllRoles().Select(r => new { DisplayText = r.RoleName, Value = r.RoleId }); ;
                return Json(new { Result = "OK", Options = roles });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        [HttpPost]
        public List<Employee> GetAllEmployees()
        {
            return db.Employees.OrderBy(e => e.Name).ToList();
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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