using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
namespace code_shop.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public String id_user = "0";
        public ActionResult Index()
        {
            //ViewData["Message"] = "Приветствую тебя странник";
            //Models.User usr = new Models.User();
           
            

            

            
            

            return View("Ajax");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult enter()
        {
            return View("Enter");
        }
        [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult enter(Models.User usr)
        {
            if (usr.password != "" & usr.username != "")
            {
                id_user=usr.doLogin(usr.username, usr.password);
                if (id_user != "")
                {
                    Session["user_id"] = id_user;
                    ViewData["Message"] = "Успешно авторизовался!";
                    return View("BlogIndex");
                }
            }
            return View("Enter");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult register()
        {
            return View("Register");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult register(Models.User usr)
        {
            if (usr.password != "" & usr.username != "")
            {
                usr.doAddUser(usr.username, usr.password);
                ViewData["Message"] = "Успешно зарегистрировались!";
                return View("BlogIndex");
            }
            else
            {
                return View("Register");
            }
            
            
        }
        public ActionResult catalog(String id=null)
        {
            Models.Category cat = new Models.Category();
            if (id == null)
            {
                ViewData["Message"] = cat.doListCategory();
            }
            else
            {
                int id_cat = Convert.ToInt32(id);
                ViewData["Message"] = cat.getCategory(id_cat);

            }

            return View("Catalog");
        }
          [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult view(int id)
        {
            Models.Item it = new Models.Item();
            Models.Comment cm = new Models.Comment();
            ViewData["Message"] = it.getItem(id);
            ViewData["Comments"] = cm.getComment(id);
            return View("Item");
        }
          [AcceptVerbs(HttpVerbs.Post)]
          public ActionResult view(int id,Models.Comment  cm)
          {
              if (cm.text != "" & Session["user_id"]!="")
              {
                  cm.doAddComment(Convert.ToInt32(Session["user_id"]), id, cm.text);
              }
              Models.Item it = new Models.Item();
             
              ViewData["Message"] = it.getItem(id);
              ViewData["Comments"] = cm.getComment(id);
              return View("Item");
          }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult test()
        {
            ViewData["Message"] = Session["user_id"].ToString();
            return View("BlogIndex");
        }
        public ActionResult exit()
        {
            Session["user_id"] = "";
            return View("BlogIndex");
        }
    }
}
