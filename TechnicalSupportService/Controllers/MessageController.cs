using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BusinessLayer.Concrete.UserManager;

namespace TechnicalSupportService.Controllers
{
    public class MessageController : Controller
    {
        MessageManager msgmng = new MessageManager(new EfMessageDal());
        RequestManager request = new RequestManager(new EfRequestDal());
        DepartmentManager dm = new DepartmentManager(new EfDepartmentDal());
        UserManager um = new UserManager();
        RequestCategoryManager rcm = new RequestCategoryManager(new EfRequestCategoryDal());
        PriorityManager prm = new PriorityManager(new EfPriorityDal());
        [Authorize(Roles = "3")]
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "3")]
        public ActionResult MessagesByRequest(int id)
        {
            //var messagevalues = msgmng.GetListByRequestID(id);

            //                                         from messages in msgmng.GetList()   //mesaj tablosundan mesajları getir
            //                                       //join request in request.GetRequest() on messages.Request_Id equals request.Request_Id
            //                                       //join usertable in um.GetUsers() on usertable.user equals user.User_Id
            List<UsersData> listAllUser = um.GetUsers();
            List<ReqMesClass> listeleme = (from messages in msgmng.GetList()
                                           join request in request.GetRequest() on messages.Request_Id equals request.Request_Id
                                           join prio in prm.GetPriority() on request.Priority_ID equals prio.Priority_Id
                                           join reqctg in rcm.GetRequestCategory() on request.RequestCategory_ID equals reqctg.RequestCategory_Id
                                           join responsibleUser in listAllUser on request.Request_Undertaker_Id equals responsibleUser.Id into temp
                                           from res in temp.DefaultIfEmpty()
                                           join requesterUser in listAllUser on request.User_Id equals requesterUser.Id
                                           where request.Request_Id == id
                                           select new ReqMesClass
                                           {


                                               Request_Subject = request.Request_Subject,
                                               Request_Id = request.Request_Id,
                                               Receiving_Date = messages.Receiving_Date,
                                               Request_Priority_Id = (prio != null ? prio.Priority_Id : 0),
                                               Request_Priority_Name = (prio != null ? prio.Priority_Name : ""),
                                               Request_Status = request.Request_Status,
                                               Request_Undertaken = request.Request_Undertaken,
                                               Request_Undertaken_Date = request.Request_Undertaken_Date,
                                               RequestCategory_Id = (reqctg != null ? reqctg.RequestCategory_Id : 0),
                                               RequestCategory_Name = (reqctg != null ? reqctg.RequestCategory_Name : ""),
                                               Department_Name = requesterUser.Bolum,
                                               Message_Id = messages.Message_Id,
                                               Message_Status = messages.Message_Status,
                                               Message_Content = messages.Message_Content,
                                               Message_User_Id = messages.User_Id,
                                               Message_Image1 = messages.Message_Image1,
                                               Message_Image2 = messages.Message_Image2,
                                               Message_Image3 = messages.Message_Image3,
                                               Message_Image4 = messages.Message_Image4,
                                               Message_Image5 = messages.Message_Image5,
                                               Create_Date = request.Create_Date,
                                               Sent_Date = messages.Sent_Date,
                                               IsActive = request.IsActive,
                                               End_Date = request.End_Date,
                                               User_Id = request.User_Id,
                                               User_Name = requesterUser.NameSurname,
                                               Responsible_Id = request.Request_Undertaker_Id??0,
                                               Responsible_Name = (res != null ? res.NameSurname : "")



                                           }).ToList();

            return View(listeleme);
        }


        [HttpPost]
        [Authorize(Roles = "0,1,2,3")]
        [ValidateInput(false)]
        
        public ActionResult ReplyRequest(Message msg, string t, HttpPostedFileBase MessageImage)
        {
            
            var q = Convert.ToInt32(Session["UserInfo"]);
            //Tbl_Users ObjUsr = Session["UserInfo"] as Tbl_Users;

            if (MessageImage != null)
            {
                string filename = Path.GetFileName(MessageImage.FileName);
                string extension = Path.GetExtension(MessageImage.FileName);

                if (!Directory.Exists(Server.MapPath("/MessageImage/" + msg.Request_Id + "/")))
                {
                    Directory.CreateDirectory(Server.MapPath("/MessageImage/" + msg.Request_Id + "/"));
                }


                string filepath = "/MessageImage/" + msg.Request_Id + "/" + DateTime.Now.ToString("ddMMyyHHmm-") + filename  ;
                MessageImage.SaveAs(Server.MapPath(filepath));
                msg.Message_Image1 = "/MessageImage/" + msg.Request_Id + "/" + DateTime.Now.ToString("ddMMyyHHmm-") + filename;
            }

            msg.User_Id = q;

            msg.Message_Status = true;
            msg.Sent_Date = DateTime.Now;
            //p.Request_Id = 3;
            msg.Receiving_Date = DateTime.Now;
            int id = Convert.ToInt32(msg.Request_Id);
            //p.Message_Content = "sasdas";
            msgmng.MessageAdd(msg);
            var user=um.GetById(q);
            if (user.PersonelTur==3)
            {
                return RedirectToAction("MessagesByRequest", "Message", new { id });
            }
            else
            {
                return RedirectToAction("MyRequestDetail", "FAQ", new { id });
            }
            //return View("MessagesByRequest" + p.Request_Id);

        }



    }


}