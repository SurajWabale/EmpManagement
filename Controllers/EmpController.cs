using EmpManagement.DAL;
using EmpManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagement.Controllers
{
    public class EmpController : Controller
    {
        public readonly ApplicationDbContext _db;

        //pagination Functionality
        public int pageIndex = 1;
        public readonly int pageSize = 5;
        public int totalPages = 0;

        //Search Functionality
        public string search = "";
        public List<Emp> Emplist { get; set; }=new List<Emp>();
        public EmpController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? pageIndex, string? search)
        {
            IQueryable<Emp> query = _db.Employees;

            if (search != null)
            {
                this.search = search;
                query = query.Where(p => p.Name.Contains(search) || p.DepartmentName.Contains(search));
            }
            query = query.OrderBy(u => u.Id);

            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            this.pageIndex = (int)pageIndex;

            decimal count = query.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((this.pageIndex - 1) * pageSize)
                .Take(pageSize);

            Emplist = query.ToList();
            return View(Emplist);
        }
        public IActionResult Register()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Register(Login login)
        {
            if (ModelState.IsValid)
            {
               _db.Logins.Add(login);  
                _db.SaveChanges();
                TempData["success"] = "User Account Created Successfully";
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {

            return View();
}

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var userInDb = _db.Logins.FirstOrDefault(u => u.LoginId == login.LoginId && u.Password == login.Password);      
            if (userInDb.UserType.Equals("Admin"))
            {
                    List<Emp> empList = _db.Employees.ToList();
                    TempData["success"] = "Logged in successfully";
                    return View("Index", empList);  
            }
            else if(userInDb.UserType.Equals("Employee"))
            {
                
                Emp? emp = _db.Employees.FirstOrDefault(u => u.Name == userInDb.Name);
                return View("EmpDashBoard",emp);
            }
            else
            {
                return View();
            }
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Emp emp) 
        {
           if(ModelState.IsValid)
            {
                _db.Employees.Add(emp);
                _db.SaveChanges();
                TempData["success"] = "Employee Record Created Successfully";
            }
           return RedirectToAction("Index");
        }

        public IActionResult Update(int?  id) 
        {
            if(id == null || id== 0) 
            {
                return NotFound();
            }
            Emp? emp = _db.Employees.FirstOrDefault(x => x.Id == id);
            if(emp == null) 
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        public IActionResult Update(Emp emp)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(emp);
                _db.SaveChanges();
                TempData["success"] = "Employee Record Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Emp? emp = _db.Employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost ,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Emp? emp = _db.Employees.FirstOrDefault(x=>x.Id==id);
            if(emp == null)
            {
                return NotFound();
            }
                _db.Employees.Remove(emp);
                _db.SaveChanges();
            TempData["success"] = "Employee Record Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
