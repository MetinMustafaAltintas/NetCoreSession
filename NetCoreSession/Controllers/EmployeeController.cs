using Microsoft.AspNetCore.Mvc;
using NetCoreSession.ExtensionMethods;
using NetCoreSession.Models.ContextClasses;
using NetCoreSession.Models.Entities;

namespace NetCoreSession.Controllers
{
    // Bir Emplooyeer ismi ve soyisimi girip SignIn Action'ina post yapsın.. Eğer o isimde ve soy isimde bir Employee varsa bu employee nesnensi session'a eklensin ve onu 3.Action'a yönlendirerek Sesion bilgisinddeki ismi ve soyismi yazdırsın eğer Employee yoksa ViewBag.Message'da böyle bir çalışan yoktur diye mesaj çıkartıp SignIn sayfasında kalmasını sağlayın.
    public class EmployeeController : Controller
    {
        NorthwindContext _db;
        public EmployeeController(NorthwindContext db)
        {
            _db = db;
        }

        public IActionResult GetEmployee()
        {
            return View(_db.Employees.ToList());
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();

            return RedirectToAction("GetEmployee");
        }

        public IActionResult UpdateEmployee(int id)
        {
            return View(_db.Employees.Find(id));
        }
        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            Employee original = _db.Employees.Find(employee.EmployeeId);
            original.FirstName = employee.FirstName;
            original.LastName = employee.LastName;
            _db.SaveChanges();
            return RedirectToAction("GetEmployee");

        }

        public IActionResult DeleteEmployee(int id)
        {
            _db.Employees.Remove(_db.Employees.Find(id));
            _db.SaveChanges();
            return RedirectToAction("GetEmployee");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(Employee item)
        {
            Employee e = _db.Employees.FirstOrDefault(x => x.FirstName == item.FirstName && x.LastName == item.LastName);
            if (e == null)
            {
                ViewBag.Message = "Calısan bulunamadı";
                return View();
            }
            HttpContext.Session.SetObject("cagri", e);
            return RedirectToAction("GetSessionData");
        }

        public IActionResult GetSessionData()
        {
            return View();
        }
    }
}