using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalSupportService.Models;
using static BusinessLayer.Concrete.UserManager;

namespace TechnicalSupportService.Controllers
{
    public class FAQController : Controller
    {
        RequestManager rm = new RequestManager(new EfRequestDal());
        UserManager um = new UserManager();
        MessageManager msgmng = new MessageManager(new EfMessageDal());
        RequestCategoryManager rcm = new RequestCategoryManager(new EfRequestCategoryDal());
        PriorityManager prm = new PriorityManager(new EfPriorityDal());
        MessageValidator messagevalidator = new MessageValidator();

        public helpdeskEntities Repository { get; set; } = new helpdeskEntities();
        public FAQController()
        {
            ViewBag.Repository = Repository;
        }
        FAQManager help = new FAQManager();
        public ActionResult Index()
        {
            IndexModel indexModel = new IndexModel();
            indexModel.Category = new Categories();
            indexModel.CategoryList = help.ListAllCategory();
            indexModel.Question = new Questions();
            indexModel.QuestionList = help.ListAllQuestion();
            return View(indexModel);
            //return RedirectToAction("AdminCategories");
        }
        [Authorize(Roles = "3")]
        public ActionResult AdminCategories()
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            Models.categoryListViewModel ttt = new Models.categoryListViewModel();
            ttt.CategoryList = help.ListAllCategory();
            ttt.Category = new Categories();
            return View(ttt);
        }
        [Authorize(Roles = "3")]
        public ActionResult AdminQuestions()
        {
            List<QuestionCategory> Listeleme =
               (from question in help.ListAllQuestion()
                join category in help.ListAllCategory() on question.Category_ID equals category.Category_ID
                select new QuestionCategory
                {
                    Question_ID = question.Question_ID,
                    Question_Category_Name = category.Category_Name,
                    Question_Title = question.Question_Title,
                    Question_Details = question.Question_Details,
                    Answer_Content = question.Answer_Content,
                    Category_ID = question.Category_ID,
                    Create_Date = question.Create_Date,
                    IsActive = question.IsActive,
                    Reading_Count = question.Reading_Count
                }).ToList();
            return View(Listeleme);
        }

        [Authorize(Roles = "3")]
        public JsonResult ActivePassiveQuestion(int ID)
        {


            bool QuestionValue = help.UpdateQuestionWithID(ID);



            return Json(QuestionValue, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "3")]
        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            if (id == 0)
            {
                Questions questions = new Questions();

                ViewBag.vlc = valuecategory;
                return View(questions);
            }
            else
            {
                var questionValue = help.GetQuestion(id);

                ViewBag.vlc = valuecategory;

                return View(questionValue);
            }

        }
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditQuestion(Questions question)
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            if (question.Question_ID == 0)
            {

                ViewBag.vlc = valuecategory;

                question.IsActive = true;
                question.Create_Date = DateTime.Parse(DateTime.Now.ToString());
                question.Reading_Count = Convert.ToInt32("0");
                //question.Category_ID = 1;
                help.AddQuestion(question);
                return RedirectToAction("AdminQuestions");
            }
            else
            {
                var questionValue = help.GetQuestion(question.Question_ID);
                ViewBag.vlc = valuecategory;
                if (question.TopLevel == null)
                {
                    question.TopLevel = false;
                }

                help.UpdateQuestion(question);

                return RedirectToAction("AdminQuestions");
            }

        }
        public ActionResult Index1()
        {
            return View();
        }
        //------------------------------------------------Categories--------------------------------------------------------------------
        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult AddNewCategory()
        {
            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "3")]
        public ActionResult AddNewCategory(Categories category, HttpPostedFileBase Img_Url, string Category_Name, string Category_Description, int? Category_Parent_ID)
        {


            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            if (Img_Url != null)
            {
                string filename = Path.GetFileName(Img_Url.FileName);
                string extension = Path.GetExtension(Img_Url.FileName);
                string filepath = "/CategoryImage/" + filename;
                Img_Url.SaveAs(Server.MapPath(filepath));
                category.Img_Url = "/CategoryImage/" + filename;
            }
            if (category.Category_ID == 0)
            {

                category.Category_Name = Category_Name;
                category.Category_Description = Category_Description;
                category.IsActive = true;
                category.Create_Date = DateTime.Parse(DateTime.Now.ToString());
                category.Category_Parent_ID = Category_Parent_ID;
                //question.Category_ID = 1;
                help.AddCategory(category);
                return RedirectToAction("AdminCategories");
            }
            else
            {
                var categoryValue = help.GetCategory(category.Category_ID);
                categoryValue.Category_Name = category.Category_Name;
                categoryValue.Category_Description = category.Category_Description;
                categoryValue.Category_Parent_ID = category.Category_Parent_ID;


                help.UpdateCategory(categoryValue);

                return RedirectToAction("AdminCategories");
            }
        }

        [Authorize(Roles = "3")]
        public JsonResult OnClickedCategory(int id = 0)
        {

            Categories Obj = help.GetCategory(id);
            Obj.Categories1 = null;
            Obj.Questions = null;

            List<SelectListItem> valuecategory = (from x in help.ListAllCategory()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Category_Name,
                                                      Value = x.Category_ID.ToString(),
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return Json(Obj, JsonRequestBehavior.AllowGet);
            //return PartialView("/Views/FAQ/_edit.cshtml", Obj);

        }
        [Authorize(Roles = "3")]
        public JsonResult ActivePassive(int ID)
        {


            bool CategoryValue = help.UpdateCategorywithID(ID);



            return Json(CategoryValue, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(string word)
        {
            ViewBag.word = word;
            var values = from x in help.ListAllQuestion() select x;
            if (!string.IsNullOrEmpty(word))
            {
                values = values.Where(y => y.Answer_Content.Contains(word) || y.Question_Details.Contains(word) || y.Question_Title.Contains(word));
            }

            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult CategoryDetails(int id)
        {

            //id = 1;
            //List<CatIDQuestions> Listeleme =
            //   (from question in help.ListAllQuestion()
            //    join category in help.ListAllCategory() on question.Category_ID equals category.Category_ID
            //    join parentcategory in help.ListAllCategory() on category.Category_Parent_ID equals parentcategory.Category_ID
            //    where parentcategory.Category_ID==id
            //    select new CatIDQuestions
            //    {
            //        Question_ID = question.Question_ID,
            //        Question_Category_Name = category.Category_Name,
            //        Question_Title = question.Question_Title,
            //        Question_Details = question.Question_Details,
            //        Answer_Content = question.Answer_Content,
            //        Question_Category_ID = question.Category_ID,
            //        Category_ID = category.Category_ID,
            //        Question_Category_Parent_ID = id,
            //        Question_Category_Parent_Name=parentcategory.Category_Name,
            //        Create_Date = question.Create_Date,
            //        IsActive = question.IsActive,
            //        Reading_Count = question.Reading_Count
            //    }).ToList();
            //return View(Listeleme);
            Models.IndexModel indexModel = new Models.IndexModel();
            indexModel.Category = help.GetCategory(id);
            indexModel.CategoryList = help.ListSubCategoriesByID(indexModel.Category.Category_ID);
            indexModel.QuestionList = help.ListAllQuestion();

            return View(indexModel);

        }

        public ActionResult Categories()
        {
            Models.categoryListViewModel ttt = new Models.categoryListViewModel();
            ttt.CategoryList = help.ListAllCategory();
            ttt.Category = new Categories();
            return View(ttt);
        }
        [HttpGet]

        public ActionResult QuestionDetails(int id)
        {
            Questions questions = help.GetQuestion(id);
            questions.Reading_Count++;
            help.UpdateQuestion(questions);
            return View(questions);
        }

        public ActionResult Questions()
        {
            List<QuestionCategory> Listeleme =
               (from question in help.ListAllQuestion()
                join category in help.ListAllCategory() on question.Category_ID equals category.Category_ID
                select new QuestionCategory
                {
                    Question_ID = question.Question_ID,
                    Question_Category_Name = category.Category_Name,
                    Question_Title = question.Question_Title,
                    Question_Details = question.Question_Details,
                    Answer_Content = question.Answer_Content,
                    Category_ID = question.Category_ID,
                    Create_Date = question.Create_Date,
                    IsActive = question.IsActive,
                    Reading_Count = question.Reading_Count
                }).ToList();
            return View(Listeleme);
        }
        [HttpGet]
        public ActionResult MyRequest(int id)
        {

            using (helpdeskEntities c = new helpdeskEntities())
            {

                //var MyRequests = request.GetRequestsByUserId(UserIdInfo);

                //return View(MyRequests);



                List<UserManager.UsersData> listAllUser = um.GetUsers();
                List<ReqMesClass> listeleme = (from request in rm.GetRequest().Where(x => x.User_Id == id)
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

                                                   //Department_Name = request.User.Department.Department_Name,
                                                   Request_Priority_Name = (prio != null ? prio.Priority_Name : ""),
                                                   RequestCategory_Id = (prio != null ? prio.Priority_Id : 0),
                                                   Responsible_Id = request.Request_Undertaker_Id ?? 0,
                                                   Responsible_Name = (res != null ? res.NameSurname : "")
                                                   //Responsible_Image = responsibleUser.Picture.ToString()

                                               }).OrderByDescending(x => x.Create_Date).ToList();



                return Json(new { data = listeleme }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]
        public ActionResult MyRequests()
        {
            var UserIdInfo = Convert.ToInt32(Session["UserInfo"]);
            if (UserIdInfo != 0)
            {
                return View();

            }
            else
                return RedirectToAction("Index", "Login");


        }

        [Authorize(Roles = "1")]
        [ValidateInput(false)]
        public ActionResult MyRequestDetail(int id)
        {
            var UserIdInfo = Convert.ToInt32(Session["UserInfo"]);
            
            List<ReqMesClass> listeleme = null;
            using (helpdeskEntities db = new helpdeskEntities())
            {


                listeleme = (from messages in db.Messages
                             join request in db.Requests on messages.Request_Id equals request.Request_Id
                             join prio in db.Priorities on request.Priority_ID equals prio.Priority_Id
                             join reqctg in db.RequestCategories on request.RequestCategory_ID equals reqctg.RequestCategory_Id
                             //into temp from t in temp.DefaultIfEmpty()
                             where request.Request_Id == id && request.User_Id == UserIdInfo
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
                                 //RequestCategory_Id = request.RequestCategory_ID,
                                 RequestCategory_Name = (reqctg != null ? reqctg.RequestCategory_Name : ""),
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
                                 Rate = request.Rate,
                                 Comment = request.Comment,
                                 RateDate = request.RateDate,
                                 Responsible_Id = request.Request_Undertaker_Id ?? 0



                             }).ToList();


            }


            
            if (listeleme.Count == 0)
            {
                return RedirectToAction("MyRequests", "FAQ");
            }
            else
            {
                return View(listeleme);
            }

        }
        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult AddNewRequestWithMessage()
        {
            var UserIdInfo = Convert.ToInt32(Session["UserInfo"]);
            if (UserIdInfo != 0)
            {



                List<SelectListItem> valuecategory = (from x in rcm.GetRequestCategory()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.RequestCategory_Name,
                                                          Value = x.RequestCategory_Id.ToString()
                                                      }
                                                           ).ToList();
                ViewBag.vlc = valuecategory;

                List<SelectListItem> valuepriority = (from x in prm.GetPriority()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.Priority_Name,
                                                          Value = x.Priority_Id.ToString()
                                                      }
                                                           ).ToList();
                ViewBag.vlp = valuepriority;

                Message msg = new Message();
                msg.Request = new Request();
                return View(msg);
            }
            else
                return RedirectToAction("Index", "Login");

        }

        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNewRequestWithMessage(Request request, Message msg, string p, HttpPostedFileBase MessageImage)
        {
            using (helpdeskEntities c = new helpdeskEntities())
            {
                var UserIdInfo = Convert.ToInt32(Session["UserInfo"]);

                //ValidationResult results = messagevalidator.Validate(msg);
                //if (results.IsValid)
                //{
                List<SelectListItem> valuecategory = (from x in rcm.GetRequestCategory()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.RequestCategory_Name,
                                                          Value = x.RequestCategory_Id.ToString()
                                                      }
                                                       ).ToList();
                ViewBag.vlc = valuecategory;

                List<SelectListItem> valuepriority = (from x in prm.GetPriority()
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



                return RedirectToAction("MyRequests", "FAQ");
            }
            //}
            //else
            //{
            //    foreach (var item in results.Errors)
            //    {
            //        List<SelectListItem> valuecategory = (from x in rcm.GetRequestCategory()
            //                                              select new SelectListItem
            //                                              {
            //                                                  Text = x.RequestCategory_Name,
            //                                                  Value = x.RequestCategory_Id.ToString()
            //                                              }
            //                                           ).ToList();
            //        ViewBag.vlc = valuecategory;

            //        List<SelectListItem> valuepriority = (from x in pm.GetPriority()
            //                                              select new SelectListItem
            //                                              {
            //                                                  Text = x.Priority_Name,
            //                                                  Value = x.Priority_Id.ToString()
            //                                              }
            //                                                   ).ToList();
            //        ViewBag.vlp = valuepriority;


            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}
            //return View();
        }
        [Authorize(Roles = "0,1,2")]
        [ValidateInput(false)]
        public ActionResult SolvedMyRequest(int id)
        {
            using (helpdeskEntities c = new helpdeskEntities())
            {
                var UserIdInfo = Convert.ToInt32(Session["UserInfo"]);
                //string p = (string)Session["EmailUsername"];
                ////ViewBag.d= p;
                //var UserIdInfo = Convert.ToInt32(c.Users.FirstOrDefault(x => x.EmailUsername == p).id);
                var RequestValue = rm.GetById(id); //***********************


                RequestValue.Request_Status = true;
                RequestValue.End_Date = DateTime.Now;
                RequestValue.IsActive = false;
                RequestValue.Request_Undertaken = false;
                rm.RequestUpdate(RequestValue);
                return RedirectToAction("MyRequests", "FAQ");
            }
        }
        public JsonResult RateRequest(Request req)
        {
            var RequestValue = rm.GetById(req.Request_Id);

            RequestValue.Comment = req.Comment;
            RequestValue.Rate = req.Rate;
            if (req.Rate == null)
            {
                RequestValue.Rate = 1;
            }
            RequestValue.RateDate = DateTime.Now;
            rm.RequestUpdate(RequestValue);



            return Json("OK", JsonRequestBehavior.AllowGet);

            //return RedirectToAction("MyRequestDetail", "FAQ", new { id });
        }

        public PartialViewResult CommentPartial(int id)
        {
            Request Obj = rm.GetById(id);



            return PartialView("/Views/FAQ/_commentModal.cshtml", Obj);
        }
    }
}
