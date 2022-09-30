using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace TechnicalSupportService.Controllers
{
    public class RequestController : Controller
    {
        RequestManager rm = new RequestManager(new EfRequestDal());
        UserManager um = new UserManager();
        RequestCategoryManager rcm = new RequestCategoryManager(new EfRequestCategoryDal());        
        PriorityManager prm = new PriorityManager(new EfPriorityDal());
        //DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());

        // GET: Request
        [Authorize(Roles = "3")]
        public ActionResult Requests()
        {
            var RequestCategoryValues = rcm.GetRequestCategory();
            return View(RequestCategoryValues);
        }
        public ActionResult GetRequests(int Type = 1)
        {
           
            List<UserManager.UsersData> listAllUser = um.GetUsers();

            List<ReqMesClass> listeleme = (from request in rm.GetRequest().Where(x => x.IsActive == (Type == 1 ? true : false))
                                           join responsibleUser in listAllUser on request.Request_Undertaker_Id equals responsibleUser.Id into temp
                                           from res in temp.DefaultIfEmpty()
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
                                               Rate= request.Rate,
                                               //Department_Name = request.User.Department.Department_Name,
                                               Request_Priority_Name = (prio != null ? prio.Priority_Name : ""),
                                               RequestCategory_Id = (reqctg!=null ? reqctg.RequestCategory_Id : 0),
                                               Responsible_Id = request.Request_Undertaker_Id??0,
                                               Responsible_Name = (res != null ? res.NameSurname : "")
                                               //Responsible_Image = responsibleUser.Picture.ToString()

                                           }).ToList();



            return Json(new { data = listeleme }, JsonRequestBehavior.AllowGet);
            //return View();

            //var requestvalues = rm.GetRequest();
            //return View(requestvalues);
        }



        [HttpGet]
        public ActionResult AddRequest()
        {
            //List<SelectListItem> valuedepartment = (from x in dm.GetList()
            //                                        select new SelectListItem
            //                                        {
            //                                            Text=x.Department_Name,
            //                                            Value=x.Department_Id.ToString()
            //                                        }
            //                                        ).ToList();
            //ViewBag.vld = valuedepartment;
            return View();
        }



        [HttpPost]
        public ActionResult AddRequest(Request p)
        {
            p.IsActive = true;
            p.Create_Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            rm.RequestAdd(p);
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "3")]
        public ActionResult EditRequest(int id)
        {

            var RequestValue = rm.GetById(id);

            return View(RequestValue);
        }
        [ValidateInput(false)]
        [Authorize(Roles = "3")]
        public ActionResult TakeRequest(int id)
        {
            helpdeskEntities c = new helpdeskEntities();

            //string p = (string)Session["User_Mail"];
            var q = Convert.ToInt32(Session["UserInfo"]);
            //ViewBag.d= p;
            //var UserDepartmentIdInfo = Convert.ToInt32(c.Users.Where(x => x.username == p).Select(y => y.Department_Id).FirstOrDefault());

            var RequestValue = rm.GetById(id);
            RequestValue.Request_Undertaker_Id = q;
            //RequestValue.Department_Id = UserDepartmentIdInfo;    
            RequestValue.Request_Undertaken_Date = DateTime.Now;
            RequestValue.Request_Undertaken = true;
            RequestValue.IsActive = true;
            //RequestValue.Request_Status = false;
            rm.RequestUpdate(RequestValue);
            return RedirectToAction("MessagesByRequest", "Message", new { id });
        }
        [Authorize(Roles = "3")]
        public ActionResult BreakRequest(int id)
        {
            helpdeskEntities c = new helpdeskEntities();

            //string p = (string)Session["User_Mail"];
            //int q = Convert.ToInt32((int)Session["User_ID"]);
            //ViewBag.d= p;
            //var UserDepartmentIdInfo = Convert.ToInt32(c.Users.Where(x => x.User_Mail == p).Select(y => y.Department_Id).FirstOrDefault());

            var RequestValue = rm.GetById(id);
            RequestValue.Request_Undertaker_Id = null;
            RequestValue.Department_Id = null;
            RequestValue.Request_Undertaken_Date = null;
            RequestValue.Request_Undertaken = null;
            RequestValue.IsActive = true;
            RequestValue.Request_Status = null;
            rm.RequestUpdate(RequestValue);
            return RedirectToAction("MessagesByRequest", "Message", new { id });
        }
        [Authorize(Roles = "3")]
        public ActionResult SolvedRequest(int id)
        {
            var RequestValue = rm.GetById(id);
            RequestValue.Request_Status = true;
            RequestValue.End_Date = DateTime.Now;
            RequestValue.IsActive = false;
            rm.RequestUpdate(RequestValue);
            return RedirectToAction("MessagesByRequest", "Message", new { id });
        }

        [Authorize(Roles = "3")]
        public ActionResult NotResolvedRequest(int id)
        {

            var RequestValue = rm.GetById(id);
            RequestValue.Request_Status = false;
            RequestValue.End_Date = DateTime.Now;
            RequestValue.IsActive = false;
            rm.RequestUpdate(RequestValue);
            return RedirectToAction("MessagesByRequest", "Message", new { id });
        }
    }
}