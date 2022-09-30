using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using FluentValidation.Results;
using System;
using System.Web.Mvc;
using TechnicalSupportService.Models;

namespace TechnicalSupportService.Controllers
{
    public class RequestCategoryController : Controller
    {
        RequestCategoryManager rcm = new RequestCategoryManager(new EfRequestCategoryDal());
        RequestCategoryValidator RequestCategoryvalidator = new RequestCategoryValidator();

        // GET: RequestCategory
        [Authorize(Roles = "3")]
        public ActionResult Index()
        {
            var RequestCategoryValues = rcm.GetRequestCategory();
            return View(RequestCategoryValues);
        }
       
        [Authorize(Roles = "3")]
        public ActionResult DeleteRequestCategory(int id)
        {
            var RequestCategoryValue = rcm.GetById(id);
            rcm.RequestCategoryDelete(RequestCategoryValue);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "3")]
        public ActionResult ActivateRequestCategory(int id)
        {
            var RequestCategoryValue = rcm.GetById(id);
            rcm.RequestCategoryActivate(RequestCategoryValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult EditRequestCategory(int id)
        {
            if (id==0)
            {
                RequestCategory ttt = new RequestCategory();
                return View(ttt);
            }
            else
            {
                var RequestCategoryValue = rcm.GetById(id);
                //dm.RequestCategoryUpdate(RequestCategoryValue);
                return View(RequestCategoryValue);
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "3")]
        public ActionResult EditRequestCategory(RequestCategory p)
        {
            if (p.RequestCategory_Id==0)
            {
                ValidationResult results = RequestCategoryvalidator.Validate(p);
                if (results.IsValid)
                {
                    p.IsActive = true;
                    p.Create_Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                    rcm.RequestCategoryAdd(p);
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
            else
            {
                ValidationResult results = RequestCategoryvalidator.Validate(p);
                if (results.IsValid)
                {
                    p.IsActive = true;
                    //p.Create_Date = DateTime.Now;
                    rcm.RequestCategoryUpdate(p);
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
}