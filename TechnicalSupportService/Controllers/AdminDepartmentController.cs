using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using FluentValidation.Results;
using System.Web.Mvc;

namespace TechnicalSupportService.Controllers
{
    public class AdminDepartmentController : Controller
    {
        // GET: AdminDepartment

        DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());
        UserManager um = new UserManager();
        DepartmentValidator departmentvalidator = new DepartmentValidator();
        [Authorize (Roles="3")]
        public ActionResult Index()
        {
            var DepartmentValues = dm.GetList();
            return View(DepartmentValues);
        }
        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "3")]
        public ActionResult AddDepartment(Department p)
        {
            
            ValidationResult results = departmentvalidator.Validate(p);
            if (results.IsValid)
            {
                p.Is_Active = true;
                dm.DepartmentAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [Authorize(Roles = "3")]
        public ActionResult DeleteDepartment(int id)
        {
            var DepartmentValue = dm.GetById(id);
            dm.DepartmentDelete(DepartmentValue);
            return RedirectToAction("Index");
        }
        public ActionResult ActivateDepartment(int id)
        {
            var DepartmentValue = dm.GetById(id);
            dm.DepartmentActivate(DepartmentValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult EditDepartment(int id) 
        {
            var DepartmentValue = dm.GetById(id);
            //dm.DepartmentUpdate(DepartmentValue);
            return View(DepartmentValue);
        }

        [HttpPost]
        [Authorize(Roles = "3")]
        public ActionResult EditDepartment(Department p)
        {
            ValidationResult results = departmentvalidator.Validate(p);
            if (results.IsValid)
            {
                p.Is_Active = true;
                dm.DepartmentUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

    }

}