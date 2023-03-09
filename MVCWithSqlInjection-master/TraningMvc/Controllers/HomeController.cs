using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MODEL.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Web;
using Newtonsoft.Json;

namespace TraningMvc.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext db;
        public HomeController(MyDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(List<Employee> employees)
        {
            employees = db.Employees.Include(x => x.Departman).ToList();
            List<Departman> departmans = db.Departmans.ToList();
            return View((new Employee(), employees, departmans, new Employee()));
        }

        [HttpPost]
        public IActionResult Index([Bind(Prefix = "Item1")] Employee employee)
        {
            if (employee.FirstName == null) employee.FirstName = "";
            if (employee.LastName == null) employee.LastName = "";
        var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", employee.FirstName ),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@DepartmanID", employee.DepartmanID)
            };
            List<Employee> employees = db.Set<Employee>().FromSqlRaw("EXEC ASPTraining.dbo.iisp_employee_getir_by_filter @FirstName, @LastName, @DepartmanID",parameters.ToArray()).ToList();
            return RedirectToAction("Index", "Home",employees);
        }

        [HttpPost]
        public IActionResult Create([Bind(Prefix = "Item1")] Employee employee)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@Lastname", employee.LastName),
                new SqlParameter("@DepartmanID", employee.Departman.DepartmanID)
            };
            db.Database.ExecuteSqlRaw("EXEC AddEmployee @Firstname, @LastName, @DepartmanID", parameters.ToArray());
            return RedirectToAction("Index", "Home");
        }

        public JsonResult Filter(string input1, string input2, string input3)
        {
            if (input1 == null) input1 = ""; if (input2 == null) input2 = ""; if (input3 == null) input3 = "";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FirstName", input1 ),
                new SqlParameter("@LastName", input2),
                new SqlParameter("@DepartmanID", input3)
            };
            List<FilterProcedure> filterProcedures = db.Set<FilterProcedure>().FromSqlRaw("EXEC ASPTraining.dbo.iisp_employee_getir_by_filter @FirstName, @LastName, @DepartmanID", parameters.ToArray()).ToList();
            return Json(JsonConvert.SerializeObject(filterProcedures));
        }

         
        public IActionResult Delete(int id)
        {
            db.Employees.Remove(db.Employees.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
