using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TechnicalSupportService.Controllers
{
    public class UserPanelController : Controller
    {
        MessageManager msgmng = new MessageManager(new EfMessageDal());
        RequestManager rm = new RequestManager(new EfRequestDal());
        DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());
        PriorityManager pm = new PriorityManager(new EfPriorityDal());
        RequestCategoryManager rcm = new RequestCategoryManager(new EfRequestCategoryDal());
        UserManager um = new UserManager();
        UserValidator uservalidator = new UserValidator();
        MessageValidator messagevalidator = new MessageValidator();
        // GET: UserPanel
        //public ActionResult MessagesByRequestByUser()
        //{

        //    var values = msgmng.GetListByUser();
        //    return View(values);
        //}
        //[Authorize]
        //public ActionResult UserScreen()
        //{
        //    //var messagevalues = msgmng.GetListByRequestID(id);
        //    Context c = new Context();

        //    string p = (string)Session["User_Mail"];
        //    int UserIdInfo = (int)Session["User_ID"];
        //    //ViewBag.d= p;
        //    //var UserIdInfo = Convert.ToInt32(c.Users.Where(x => x.User_Mail == p).Select(y => y.User_Id).FirstOrDefault());
        //    List<UserReqMesClass> listeleme = (

        //                                       // from messages in msgmng.GetList()   //mesaj tablosundan mesajları getir
        //                                       //join request in request.GetRequest() on messages.Request_Id equals request.Request_Id
        //                                       //join usertable in um.GetUsers() on usertable.user equals user.User_Id



        //                                       from messages in msgmng.GetListByUser(UserIdInfo)
        //                                       join request in request.GetRequest() on messages.Request_Id equals request.Request_Id
        //                                       join user in um.GetUsers() on request.User_Id equals user.User_Id


        //                                       where request.User_Id == user.User_Id
        //                                       select new UserReqMesClass
        //                                       {
        //                                           User_Id = request.User_Id,
        //                                           Message_Id = messages.Message_Id,
        //                                           Request_Subject = request.Request_Subject,
        //                                           Request_Id = messages.Request_Id,
        //                                           Receiving_Date = messages.Receiving_Date,
        //                                           Request_Priority = request.Request_Priority,
        //                                           Request_Status = request.Request_Status,
        //                                           Request_Undertaken = request.Request_Undertaken,
        //                                           Request_Undertaken_Date = request.Request_Undertaken_Date,
        //                                           Message_Status = messages.Message_Status,
        //                                           Message_Content = messages.Message_Content,
        //                                           Create_Date = request.Create_Date,
        //                                           Department_Id = request.Department_Id,
        //                                           Sent_Date = messages.Sent_Date,
        //                                           IsActive = request.IsActive,
        //                                           End_Date = request.End_Date,

        //                                           User_Name = request.User.User_Name,
        //                                           Department_Name = request.User.Department.Department_Name


        //                                       }).ToList();

        //    return View(listeleme);
        //}


        //Burası artık kullanılmıyor. Silinecek

        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]
        public ActionResult UserPanelMessagesByRequest(int? id)
        {
            //var messagevalues = msgmng.GetListByRequestID(id);
            helpdeskEntities c = new helpdeskEntities();
            string p = (string)Session["User_Mail"];
            int UserIdInfo = (int)Session["User_ID"];

            //var MyRequests = request.GetRequestsByUserId(UserIdInfo);

            //return View(MyRequests);
            var RequestValue = rm.GetById(id??0);
            if (RequestValue.User_Id == UserIdInfo)
            {

                userpanelrequest pnl = new userpanelrequest();
                List<UserManager.UsersData> listAllUser = um.GetUsers();
                List<ReqMesClass> listeleme = (from messages in msgmng.GetList()
                                               join request in rm.GetRequest() on messages.Request_Id equals request.Request_Id
                                               join usertable in listAllUser on request.Request_Undertaker_Id equals usertable.Id
                                               into temp
                                               from t in temp.DefaultIfEmpty()
                                               where request.Request_Id == id
                                               select new ReqMesClass
                                               {

                                                   Message_Id = messages.Message_Id,
                                                   Request_Subject = request.Request_Subject,
                                                   Request_Id = request.Request_Id,
                                                   Receiving_Date = messages.Receiving_Date,
                                                   Request_Priority_Id = request.Priority.Priority_Id,
                                                   Request_Priority_Name = request.Priority.Priority_Name,
                                                   Request_Status = request.Request_Status,
                                                   Request_Undertaken = request.Request_Undertaken,
                                                   Request_Undertaken_Date = request.Request_Undertaken_Date,
                                                   Message_Status = messages.Message_Status,
                                                   Message_Content = messages.Message_Content,
                                                   Message_Image1 = messages.Message_Image1,
                                                   Message_Image2 = messages.Message_Image2,
                                                   Message_Image3 = messages.Message_Image3,
                                                   Message_Image4 = messages.Message_Image4,
                                                   Message_Image5 = messages.Message_Image5,
                                                   Create_Date = request.Create_Date,
                                                   Department_Id = request.Department_Id,
                                                   Sent_Date = messages.Sent_Date,
                                                   IsActive = request.IsActive,
                                                   End_Date = request.End_Date,
                                                   User_Id = request.User_Id,
                                                   User_Name = messages.User.Name_Surname,
                                                   Department_Name = request.User.Department.Department_Name,
                                                   Message_User_Id = messages.User_Id,
                                                   Responsible_Id = request.Request_Undertaker_Id,
                                                   Responsible_Name = temp.Min(x => x.NameSurname),
                                                   //Responsible_Image = temp.Min(x => x.Picture).ToString(),
                                                   RequestCategory_Name = request.RequestCategory.RequestCategory_Name

                                               }).ToList();

                pnl.RequestList = listeleme;

                return View(pnl);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "0,1,2")]
        public ActionResult AddRequestWithMessage(Request request, Message msg, string p, HttpPostedFileBase MessageImage)
        {
            helpdeskEntities c = new helpdeskEntities();
            p = (string)Session["User_Mail"];            
            var UserIdInfo = Convert.ToInt32(c.Users.Where(x => x.EmailUsername == p).Select(y => y.id).FirstOrDefault());

            ValidationResult results = messagevalidator.Validate(msg);
            if (results.IsValid)
            {
                List<SelectListItem> valuecategory = (from x in rcm.GetRequestCategory()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.RequestCategory_Name,
                                                          Value = x.RequestCategory_Id.ToString()
                                                      }
                                                       ).ToList();
                ViewBag.vlc = valuecategory;

                List<SelectListItem> valuepriority = (from x in pm.GetPriority()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.Priority_Name,
                                                          Value = x.Priority_Id.ToString()
                                                      }
                                                           ).ToList();
                ViewBag.vlp = valuepriority;
                
                if (MessageImage != null)
                {
                    string filename = Path.GetFileName(MessageImage.FileName);
                    string extension = Path.GetExtension(MessageImage.FileName);
                    string filepath = "/MessageImage/" + filename;
                    MessageImage.SaveAs(Server.MapPath(filepath));
                    msg.Message_Image1 = "/MessageImage/" + filename;
                }

                request.IsActive = true;
                //request.End_Date= DateTime.Parse(DateTime.Now.ToString());
                //request.Request_Undertaken_Date=DateTime.Parse(DateTime.Now.ToString());

                request.Create_Date = DateTime.Parse(DateTime.Now.ToString());
                request.User_Id = UserIdInfo;
                request.Request_Undertaken = false;
                //request.Request_Status = false;
                int id = Convert.ToInt32(UserIdInfo);
                //rm.RequestAdd(request);

                msg.Request = request;
                msg.Request_Id = request.Request_Id;
                msg.User_Id = UserIdInfo;
                msg.Receiving_Date = DateTime.Now;
                msg.Sent_Date = DateTime.Now;
                msg.Message_Status = true;
                msgmng.MessageAdd(msg);
                
                

                return RedirectToAction("MyRequests", "UserPanel", new { id });
            }
            else
            {
                foreach(var item in results.Errors)
                {
                    List<SelectListItem> valuecategory = (from x in rcm.GetRequestCategory()
                                                          select new SelectListItem
                                                          {
                                                              Text = x.RequestCategory_Name,
                                                              Value = x.RequestCategory_Id.ToString()
                                                          }
                                                       ).ToList();
                    ViewBag.vlc = valuecategory;

                    List<SelectListItem> valuepriority = (from x in pm.GetPriority()
                                                          select new SelectListItem
                                                          {
                                                              Text = x.Priority_Name,
                                                              Value = x.Priority_Id.ToString()
                                                          }
                                                               ).ToList();
                    ViewBag.vlp = valuepriority;
                   

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [ValidateInput(false)]
        [Authorize(Roles = "0,1,2")]
        [HttpGet]
        public ActionResult AddRequestWithMessage()
        {

            List<SelectListItem> valuecategory = (from x in rcm.GetRequestCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.RequestCategory_Name,
                                                      Value = x.RequestCategory_Id.ToString()
                                                  }
                                                       ).ToList();
            ViewBag.vlc = valuecategory;

            List<SelectListItem> valuepriority = (from x in pm.GetPriority()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Priority_Name,
                                                      Value = x.Priority_Id.ToString()
                                                  }
                                                       ).ToList();
            ViewBag.vlp = valuepriority;
            return View();


        }


        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "0,1,2")]
        public ActionResult UserPanelMessagesByRequest(Message msg, string p, HttpPostedFileBase MessageImage)
        {
            helpdeskEntities c = new helpdeskEntities();
            bati_serverEntities b = new bati_serverEntities();
            p = (string)Session["EmailUsername"];
            //ViewBag.d= p;
            var UserIdInfo = Convert.ToInt32(c.Users.Where(x => x.EmailUsername == p).Select(y => y.id).FirstOrDefault());
            userpanelrequest pnl = new userpanelrequest();
            List<UserManager.UsersData> listAllUser = um.GetUsers();
            List<ReqMesClass> listeleme = (from messages in msgmng.GetList()
                                           join request in rm.GetRequest() on messages.Request_Id equals request.Request_Id
                                           join usertable in listAllUser on request.Request_Undertaker_Id equals usertable.Id
                                           into temp
                                           from t in temp.DefaultIfEmpty()
                                           where request.Request_Id == msg.Request_Id
                                           select new ReqMesClass
                                           {

                                               Message_Id = messages.Message_Id,
                                               Request_Subject = request.Request_Subject,
                                               Request_Id = request.Request_Id,
                                               Receiving_Date = messages.Receiving_Date,
                                               Request_Priority_Id = request.Priority.Priority_Id,
                                               Request_Priority_Name = request.Priority.Priority_Name,
                                               Request_Status = request.Request_Status,
                                               Request_Undertaken = request.Request_Undertaken,
                                               Request_Undertaken_Date = request.Request_Undertaken_Date,
                                               Message_Status = messages.Message_Status,
                                               Message_Content = messages.Message_Content,
                                               Message_Image1 = messages.Message_Image1,
                                               Message_Image2 = messages.Message_Image2,
                                               Message_Image3 = messages.Message_Image3,
                                               Message_Image4 = messages.Message_Image4,
                                               Message_Image5 = messages.Message_Image5,
                                               Create_Date = request.Create_Date,
                                               Department_Id = request.Department_Id,
                                               Sent_Date = messages.Sent_Date,
                                               IsActive = request.IsActive,
                                               End_Date = request.End_Date,
                                               User_Id = request.User_Id,
                                               User_Name = messages.User.Name_Surname,
                                               Department_Name = request.User.Department.Department_Name,
                                               Message_User_Id = messages.User_Id,
                                               Responsible_Id = request.Request_Undertaker_Id,
                                               Responsible_Name = temp.Min(x => x.NameSurname),
                                               //Responsible_Image = temp.Min(x => x.Picture).ToString(),
                                               RequestCategory_Name = request.RequestCategory.RequestCategory_Name

                                           }).ToList();

            pnl.RequestList = listeleme;


            var dd = rm.GetById(msg.Request_Id);
            int id = Convert.ToInt32(msg.Request_Id);
            msg.Request = dd;
            msg.User = b.Tbl_Users.Where(x => x.EmailUsername == p).FirstOrDefault();

            ValidationResult results = messagevalidator.Validate(msg);
            if (results.IsValid)
            {

                //var x = request.GetById(p.Request_Id);
                //var y = dm.GetById(p.Department.Department_Id);

                if (MessageImage != null)
                {
                    string filename = Path.GetFileName(MessageImage.FileName);
                    string extension = Path.GetExtension(MessageImage.FileName);
                    string filepath = "/MessageImage/" + filename;
                    MessageImage.SaveAs(Server.MapPath(filepath));
                    msg.Message_Image1 = "/MessageImage/" + filename;
                }
                
                msg.Request = dd;
                msg.Message_Status = true;
                msg.Sent_Date = DateTime.Now;
                //p.Request_Id = 3;
                msg.Receiving_Date = DateTime.Now;
                msg.User_Id = UserIdInfo;
                
                //p.Message_Content = "sasdas";
                msgmng.MessageAdd(msg);


                var req = rm.GetById(msg.Request_Id);

                req.IsActive = true;
                req.Request_Status = false;
                req.End_Date = null;
                rm.RequestUpdate(req);

              
                //return View("MessagesByRequest" + p.Request_Id);
            }
            else
            {
                foreach (var item in results.Errors)
                {
                   
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(pnl);
        }
        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]
        public ActionResult SolvedMyRequest(int id)
        {
            helpdeskEntities c = new helpdeskEntities();
            string p = (string)Session["EmailUsername"];
            //ViewBag.d= p;
            var UserIdInfo = Convert.ToInt32(c.Users.FirstOrDefault(x => x.EmailUsername == p).id);
            var RequestValue = rm.GetById(id); //***********************
            int usid = Convert.ToInt32(UserIdInfo);

            RequestValue.Request_Status = true;
            RequestValue.End_Date = DateTime.Now;
            RequestValue.IsActive = false;
            RequestValue.Request_Undertaken = false;
            rm.RequestUpdate(RequestValue);
            return RedirectToAction("MyRequests", "UserPanel", new { id = usid });
        }
        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]

        public ActionResult MyRequests(int id)
        {
            helpdeskEntities c = new helpdeskEntities();
            
            //var MyRequests = request.GetRequestsByUserId(UserIdInfo);

            //return View(MyRequests);

                List<UserManager.UsersData> listAllUser = um.GetUsers();
                List<ReqMesClass> listeleme = (from request in rm.GetRequest()
                                           join usertable in listAllUser on request.Request_Undertaker_Id equals usertable.Id
                                           into temp
                                           from t in temp.DefaultIfEmpty()
                                           where (request.User_Id == id)
                                           select new ReqMesClass
                                           {

                                               User_Name = request.User.User_Name,
                                               Request_Subject = request.Request_Subject,
                                               Request_Id = request.Request_Id,

                                               //Request_Priority = request.Priority.Priority_Id,
                                               Request_Status = request.Request_Status,
                                               Request_Undertaken = request.Request_Undertaken,
                                               Request_Undertaken_Date = request.Request_Undertaken_Date,

                                               Create_Date = request.Create_Date,
                                               Department_Id = request.Department_Id,

                                               IsActive = request.IsActive,
                                               End_Date = request.End_Date,
                                               User_Id = request.User_Id,

                                               Department_Name = request.User.Department.Department_Name,
                                               Request_Priority_Name = request.Priority.Priority_Name,
                                               Responsible_Id = request.Request_Undertaker_Id,
                                               Responsible_Name = temp.Min(x => x.NameSurname),
                                               //Responsible_Image = temp.Min(x => x.Picture).ToString(),
                                               RequestCategory_Name = request.RequestCategory.RequestCategory_Name

                                           }).ToList();


            return View(listeleme);
            


        }
        public class userpanelrequest
        {
            public List<ReqMesClass> RequestList { get; set; }
            public string Message_Content { get; set; }

        }
        //[Authorize]
        //public ActionResult UserProfile(string p)
        //{
        //    //var messagevalues = msgmng.GetListByRequestID(id);
        //    Context c = new Context();

        //    p = (string)Session["User_Mail"];
        //    //ViewBag.d= p;
        //    var UserIdInfo = Convert.ToInt32(c.Users.Where(x => x.User_Mail == p).Select(y => y.User_Id).FirstOrDefault());

        //}
    }
}