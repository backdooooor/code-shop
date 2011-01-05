using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace code_shop.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View("BlogIndex");
        }
        public Boolean CheckAuth()
        {
            if (Session["login"] == null | Session["password"] == null) return false;
            String login = Session["login"].ToString();
            String password = Session["password"].ToString();
            if (password != "" && login != "" && password != null && login != null)
            {
                String id_user;
                Models.User usr = new Models.User();
                id_user = usr.doLogin(login, password);
                if (id_user != "")
                {
                    Session["user_id"] = id_user;
                    Session["login"] = login;
                    Session["password"] = password;
                    return true;
                }
            }
            return false;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult additem()
        {
            try
            {
                if (this.CheckAuth())
                {
                    ViewData["Message"] = "Заполните пожалуйста поля";
                    Models.Category category = new Models.Category();
                    ViewData["category"] = category.doSelectCategory();
                    return View("AddItem");
                }
                else
                {   
                    return View("Ajax");

                }
            }
            catch
            {
                ViewData["Message"] = "Заполните пожалуйста поля";
                return View("AddItem");
            }
        }
        [HttpPost]
        public ActionResult additem(HttpPostedFileBase file, HttpPostedFileBase screenshot, Models.Item it)
        {
           
                Models.Category category = new Models.Category();
                if (file.ContentLength > 0 && it.description != "" && screenshot.ContentLength > 0 && it.title != "" && this.CheckAuth())
                {
                    if (screenshot.ContentType != "image/jpeg" && screenshot.ContentType != "image/png" && screenshot.ContentType != "image/gif")
                    {
                        ViewData["Message"] = "Скриншот должен быть только jpg,gif,png ";
                        
                        ViewData["category"] = category.doSelectCategory();
                        return View("AddItem");
                    }
                    int id_user = Convert.ToInt32(Session["user_id"]);
                    var fileName = Path.GetFileName(file.FileName);

                    var screenname = Path.GetFileName(screenshot.FileName);
                    var path = Path.Combine(Server.MapPath("~/files"), fileName);
                    file.SaveAs(path);
                    path = Path.Combine(Server.MapPath("~/url"), screenname);

                    screenshot.SaveAs(path);
                    string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                    screenname = baseUrl + "/url/" + screenname;
                    it.doAddItem(it.title, it.description, screenname, it.id_category, id_user, it.value, fileName);

                    //ViewData["Message"] = it.title;
                    ViewData["Message"] = "Товар успешно добавлен";
                
                    ViewData["category"] = category.doSelectCategory();
                    return View("AddItem");
                }
                ViewData["Message"] = "Вы заполнили не все поля!";
                
                ViewData["category"] = category.doSelectCategory();
                return View("AddItem");
            
          
        }
        public ActionResult buy(int id_tovar,int id_user)
        {
            try
            {
                if (String.Compare(id_user.ToString(), Session["user_id"].ToString()) == 0)
                {


                    Models.User usr = new Models.User();

                    ViewData["Message"] = "Товар Куплен! " + usr.doBuyItem(id_user, id_tovar); ;

                }
                return View("BlogIndex");
            }
            catch
            {
                return View("BlogIndex");

            }
        }

    }
}
