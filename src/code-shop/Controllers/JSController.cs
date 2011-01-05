using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_shop.Controllers
{
    public class JSController : Controller
    {
        //
        // GET: /JS/

        public ActionResult Index()
        {
            return View();
        }
       
        //вывод списка категорий
        public String getCatalog()
        {
            try
            {   
                //получение данных  из модели 
                Models.Category category = new Models.Category();
                String text = category.doListCategory();
                return text;
            }
            catch
            {
                return "";
            }
        }
        // авторизация через веб-форму
        public String doAuth(String login, String password)
        {
            try
            {
                String id_user;
                if (login == null) return "0";
                if (password == null) return "0";
                Models.User usr = new Models.User();
                if (password != "" && login != "")
                {
                    id_user = usr.doLogin(login, password);
                    if (id_user != "")
                    {
                        Session["user_id"] = id_user;
                        Session["login"] = login;
                        Session["password"] = password;
                        return "1";
                    }
                }
                return "0";
            }
            catch
            {
                return "0";

            }
        }
        //проверка авторизации на основе сессии
        public String CheckAuth()
        {
            try
            {
                if (Session["login"] == null | Session["password"] == null) return "0";
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
                        return "1";
                    }
                }
                return "0";
            }
            catch
            {
                return "0";
            }
        }
        //получение списка товаров из  определенной категории
        public String getItemfromCat(int id)
        {
            try
            {
                if (id == null) return "";
                Models.Category category = new Models.Category();
                String result = category.getCategory(id);
                return result;
            }
            catch
            {
                return "Нет товаров в категории!";
            }
        }
        //вывод отдельно взятого товара
        public String getItem(int id)
        {
            try
            {
                if (id == null) return "";
                Models.Item it = new Models.Item();
                Models.Comment cm = new Models.Comment();
                String result = it.getItem(id);
                result = result + "<br>" + cm.getComment(id);
                return result;
            }
            catch
            {
                return "Товар не найден!";
            }
        }
        //добавление комментария
        public String addComment(int id, String text)
        {
            try
            {
                if (id == null) return "0";
                Models.Comment comment = new Models.Comment();

                if (text != "" && this.CheckAuth() == "1")
                {
                    comment.doAddComment(Convert.ToInt32(Session["user_id"]), id, text);
                    return text;
                }
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        //поднятия рейтинга
        public String doPlusUser(int id)
        {
            try
            {
                if (id == null) return "0";
                if (this.CheckAuth() != "1") return "0";
                Models.User usr = new Models.User();
                usr.doPlusUser(id);

                return "1";
            }
            catch
            {
                return "0";
            }
        }
        //минусование рейтинга
        public String doMinusUser(int id)
        {
            try
            {
                if (id == null) return "0";
                if (this.CheckAuth() != "1") return "0";
                Models.User usr = new Models.User();
                usr.doMinusUser(id);
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        
        public String doSelectCategory()
        {
            try
            {
                Models.Category category = new Models.Category();
                String result = category.doSelectCategory();
                return result;
            }
            catch
            {
                return "";
            }
        }
        public String doBuy(int id_tovar)
        {
            try
            {
                if (id_tovar == null) return "0";
                if (this.CheckAuth() != "1") return "0";
                int id_user = Convert.ToInt32(Session["user_id"]);
                Models.User usr = new Models.User();
                return usr.doBuyItem(id_user, id_tovar);
            }
            catch
            {
                return "Произошла ошибка!";
            }

        }
        public String getBuy()
        {
            try
            {
                if (this.CheckAuth() != "1") return "";
                int id_user = Convert.ToInt32(Session["user_id"]);
                Models.User usr = new Models.User();
                return usr.getBuyItem(id_user);
            }
            catch
            {
                return "Произошла ошибка!";
            }
        }
        //регистрация
        public String doRegister(String login,String password)
        {
            try
            {
                if (login == null) return "0";
                if (password == null) return "0";
                Models.User usr = new Models.User();
                if (login != "" && password != "")
                {
                    usr.doAddUser(login, password);
                    return "1";
                }
                else
                {
                    return "0";
                }

            }
            catch
            {
                return "0";
            }
        }
        //получение профиля
        public String getProfile(int id_user)
        {
            try
            {
                if (id_user == null) return "";
                Models.User usr = new Models.User();
                return usr.getProfile(id_user);
            }
            catch
            {
                return "Пользователь не найден!";
            }
        }
        //выход из системы
        public void doExit()
        {  
            Session["user_id"] = null;
            Session["login"] = null;
            Session["password"] = null;
        }
        //редактирование профиля
        public String doEditProfile(String icq,String twitter,String skype)
        {
            try
            {
                if (this.CheckAuth() != "1") return "";
                int id_user = Convert.ToInt32(Session["user_id"]);
                if (icq == null) return "";
                if (twitter == null) return "";
                if (skype == null) return "";
                Models.User usr = new Models.User();

                return usr.doEditProfile(id_user, icq, twitter, skype);
            }
            catch
            {

                return "Произошла ошибка!";
            }
        }
        //вывод редактирования
        public String edit_profile()
        {
            try
            {
                if (this.CheckAuth() != "1") return "";
                int id_user = Convert.ToInt32(Session["user_id"]);

                Models.User usr = new Models.User();

                return usr.getContact(id_user);
            }
            catch
            {
                return "Произошла ошибка!";
            }
        }

    }
}
