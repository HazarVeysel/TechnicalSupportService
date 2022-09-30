using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace TechnicalSupportService.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        MessageManager msgmng = new MessageManager(new EfMessageDal());
        UserManager um = new UserManager();
        DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());
        RequestManager request = new RequestManager(new EfRequestDal()); 
        RequestCategoryManager rcm = new RequestCategoryManager(new EfRequestCategoryDal());
        PriorityManager prm = new PriorityManager(new EfPriorityDal());
        UserValidator uservalidator = new UserValidator();
        MessageValidator messagevalidator = new MessageValidator();
        [Authorize(Roles = "3")]
        public ActionResult Index()
        {
            List<UserManager.UsersData> listAllUser = um.GetUsers();

            return View(listAllUser);
        }
        [Authorize(Roles = "3")]
        public ActionResult GetAllUsers()
        {

            List<UserManager.UsersData> listAllUser = um.GetUsers();

            return Json(new { data = listAllUser }, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "3")]
        public ActionResult PassiveUsers()
        {
            var UserValues = um.GetUsers();

            return View(UserValues);
        }
        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult AddUser()
        {

            List<SelectListItem> valuedepartment = (from x in dm.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Department_Name,
                                                        Value = x.Department_Id.ToString()
                                                    }
                                                       ).ToList();
            ViewBag.vld = valuedepartment;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "3")]
        public ActionResult AddUser(Tbl_Users p)
        {
            ValidationResult results = uservalidator.Validate(p);
            if (results.IsValid)
            {
                List<SelectListItem> valuedepartment = (from x in dm.GetList()
                                                        select new SelectListItem
                                                        {
                                                            Text = x.Department_Name,
                                                            Value = x.Department_Id.ToString()
                                                        }
                                                       ).ToList();
                ViewBag.vld = valuedepartment;


                //p.Create_Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PersonelTur = 3;
                p.Status = 0;
                //p.Department_Id = 1;
                um.UserAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    List<SelectListItem> valuedepartment = (from x in dm.GetList()
                                                            select new SelectListItem
                                                            {
                                                                Text = x.Department_Name,
                                                                Value = x.Department_Id.ToString()
                                                            }
                                                       ).ToList();
                    ViewBag.vld = valuedepartment;

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult EditUser(int id)
        {
            var uservalue = um.GetById(id);
            List<SelectListItem> valuedepartment = (from x in dm.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Department_Name,
                                                        Value = x.Department_Id.ToString()
                                                    }
                                                       ).ToList();
            ViewBag.vld = valuedepartment;

            //List<SelectListItem> valueactive = (from y in um.GetUsers()
            //                                    select new SelectListItem
            //                                    {
            //                                        Text = y.Is_Active.(),
            //                                        Value = y.Is_Active.ToString()
            //                                    }).ToList();
            //ViewBag.vlactive = valueactive;
            return View(uservalue);
        }
        [HttpPost]
        [Authorize(Roles = "3")]
        public ActionResult EditUser(Tbl_Users p)
        {
            //var uservalue = um.GetById(p.User_Id);
            //int id = Convert.ToInt32(p.User_Id);
            //    ValidationResult results = uservalidator.Validate(p);
            //if (results.IsValid)
            //{
            List<SelectListItem> valuedepartment = (from x in dm.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Department_Name,
                                                        Value = x.Department_Id.ToString()
                                                    }
                                                   ).ToList();
            ViewBag.vld = valuedepartment;

            p.BaslamaTarihi = DateTime.Now.ToString();
            p.PersonelTur = 4;
            //p.Unvan = 1;
            p.Status = 0;
            um.UserUpdate(p);
            return RedirectToAction("Index");
            //}
            //else
            //{
            //    foreach (var item in results.Errors)
            //    {
            //        List<SelectListItem> valuedepartment = (from x in dm.GetList()
            //                                                select new SelectListItem
            //                                                {
            //                                                    Text = x.Department_Name,
            //                                                    Value = x.Department_Id.ToString()
            //                                                }
            //                                           ).ToList();
            //        ViewBag.vld = valuedepartment;

            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}

            //return View();
        }
        [Authorize(Roles = "3")]
        public ActionResult DeleteUser(int id)
        {
            var UserValue = um.GetById(id);
            um.UserDelete(UserValue);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "3")]
        public ActionResult ActiveUser(int id)
        {
            var UserValue = um.GetById(id);
            um.UserActive(UserValue);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "3")]
        [HttpGet]
        public ActionResult RequestsByUser(int id)
        {
            List<UserManager.UsersData> listAllUser = um.GetUsers();
            List<ReqMesClass> listeleme = (from request in request.GetRequest().Where(x => x.User_Id == id)
                                           join responsibleUser in listAllUser on request.Request_Undertaker_Id equals responsibleUser.Id
                                           join requesterUser in listAllUser on request.User_Id equals requesterUser.Id
                                           join prio in prm.GetPriority() on request.Priority_ID equals prio.Priority_Id
                                           join reqctg in rcm.GetRequestCategory() on request.RequestCategory_ID equals reqctg.RequestCategory_Id
                                           //where request.Request_Undertaker_Id == usertable.id
                                           select new ReqMesClass
                                           {

                                               User_Name = requesterUser.NameSurname,
                                               Request_Subject = request.Request_Subject,
                                               Request_Id = request.Request_Id,

                                               //Request_Priority = request.Priority.Priority_Id,
                                               Request_Status = request.Request_Status,
                                               Request_Undertaken = request.Request_Undertaken,
                                               Request_Undertaken_Date = request.Request_Undertaken_Date,
                                               RequestCategory_Name = (reqctg != null ? reqctg.RequestCategory_Name : ""),
                                               Create_Date = request.Create_Date,
                                               Department_Name = requesterUser.Bolum,

                                               IsActive = request.IsActive,
                                               End_Date = request.End_Date,
                                               User_Id = request.User_Id,

                                               //Department_Name = request.User.Department.Department_Name,
                                               Request_Priority_Name = (prio != null ? prio.Priority_Name : ""),
                                               RequestCategory_Id = (reqctg != null ? reqctg.RequestCategory_Id : 0),
                                               Responsible_Id = request.Request_Undertaker_Id,
                                               Responsible_Name = responsibleUser.NameSurname,
                                               //Responsible_Image = responsibleUser.Picture.ToString()

                                           }).ToList();



            return Json(new { data = listeleme }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "3")]
        public ActionResult UsersRequests(int id)
        {
            return View();
        }
    }
}
