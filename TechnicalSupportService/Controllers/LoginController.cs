using DataAccessLayer.Concrete;
using DataAccessLayer.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace TechnicalSupportService.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Tbl_Users p, string returnUrl)
        {
            //helpdeskEntities c = new helpdeskEntities();
            using (bati_serverEntities b = new bati_serverEntities())
            {


                var adminuserinfo = b.Tbl_Users.FirstOrDefault(x => x.username == p.username && x.password == p.password);
                if (adminuserinfo != null)
                {
                    FormsAuthentication.SetAuthCookie(adminuserinfo.username, false);
                    Session["UserInfo"] = adminuserinfo.id;
                    int w = adminuserinfo.id;
                    //= Convert.ToInt32((int)Session["id"]);
                    
                    if (adminuserinfo.PersonelTur == 3)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && returnUrl!= "/FAQ/MyRequests" && returnUrl != "/FAQ/AddNewRequestWithMessage")//burada returnurl'deki link admine yani personeltur=3 olana eşitse diye bir sorgu koymak lazım
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Requests", "Request", new { @Type = 1 });
                        }
                        
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/FAQ/AdminCategories" && returnUrl != "/FAQ/AdminQuestions")
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("AddNewRequestWithMessage", "FAQ");
                        }
                        //return RedirectToAction("UserScreen", "UserPanel");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }



            
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","FAQ");
        }
    }
}