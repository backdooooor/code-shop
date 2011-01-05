using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace code_shop.Models
{
    public class User
    {
        public string username { get;set; } 
        public string  password {get;set;}
       
        public int id_user{get;set;}
        public User(){

        }
        //TODO:доделать функцию
        public String doLogin(String username,String password){
            if (username == null) return "";
            if (password == null) return "";
            if (username == "") return "";
            if (password == "") return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "CheckLogin";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@user_name", username);
            SqlParameter f1 = new SqlParameter("@user_name", SqlDbType.NVarChar);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            
            while (myReader.Read())
            {


                this.password = myReader.GetString(0);
                this.username = myReader.GetString(1);
                this.id_user = myReader.GetInt32(2);

            }


            myReader.Close();

            if (this.password == null) return "";
            Conn.Close();



            
            this.password = this.password.Replace(" ", string.Empty);
            if (String.Compare(this.password.ToString(), password.ToString())==0)
            {
                
                return this.id_user.ToString();
               
            }
            else
            {
                return "";
            }
            
            
        }
        public Boolean CheckAuth(){
            return false;
        }
        public String getLogin(int id_user){
            if (id_user == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetUser";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.NVarChar);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            object tmp;
            while (myReader.Read())
            {


                tmp = myReader.GetValue(2);
                result = Convert.ToString(tmp);


            }


            myReader.Close();


            Conn.Close();

            return result;
         
        }
        //TODO:допилить
        public String getID(String id_user){
            if (id_user == null) return "";
            if (id_user == "") return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetUser";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.NVarChar);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            object tmp;
            while (myReader.Read())
            {


                tmp = myReader.GetValue(1);
                result = Convert.ToString(tmp);


            }


            myReader.Close();


            Conn.Close();

            return result;

        }
        public String CheckBalance(int id_user=0){
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetUser";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.NVarChar);
     

            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
             String result="";
             object  tmp ;
                while (myReader.Read())
                {


                    tmp = myReader.GetValue(3);
                   result = Convert.ToString(tmp);
                   

                }


                myReader.Close();


            Conn.Close();

            return result;
        }
        public int CheckBalancee(int id_user){
            if (id_user == null) return 0;
            return Convert.ToInt32(this.CheckBalance(id_user));
        }
        public Boolean doAddUser( String username, String password)
        {
            if (username == null) return false;
            if (username == "") return false;
            if (password == "") return false;
            if (password == null) return false;
        SqlConnection Conn = null;
        Conn = new SqlConnection(@DataControll.StringConnect());
        string strSQL ="AddUser";
     SqlCommand Cmd = new SqlCommand(strSQL, Conn);
      Cmd.CommandType=CommandType.StoredProcedure;
      Cmd.Parameters.Add("@username",username);
      Cmd.Parameters.Add("@password",password);
     


      SqlParameter f1=new SqlParameter("@username", SqlDbType.NVarChar);
      SqlParameter f2=new SqlParameter("@password", SqlDbType.NVarChar);
      
      f1.Size=10;
      f2.Size=500;
      
      Conn.Open();
      Cmd.ExecuteNonQuery();

Conn.Close();

        
        
        return true;
    }
        public Boolean doRemoveUser(int id_user)
        {
            if (id_user == null) return false;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "DeleteUser";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.Int);
            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
            return true;
        }
        public Boolean doPlusUser(int id_user){
            if (id_user == null) return false;
            int usmoney = Convert.ToInt32(this.CheckBalance(id_user));
            usmoney++;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateMoney";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@usmoney", usmoney);
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@usmoney", SqlDbType.Float);
            SqlParameter f2 = new SqlParameter("@id_user", SqlDbType.Int);
            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
            return true;
        }
        public Boolean doMinusUser(int id_user)
        {
            if (id_user == null) return false;
            int usmoney = Convert.ToInt32(this.CheckBalance(id_user));
            usmoney--;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateMoney";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@usmoney", usmoney);
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@usmoney", SqlDbType.Float);
            SqlParameter f2 = new SqlParameter("@id_user", SqlDbType.Int);
            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
            return true;
        }
        public Boolean doUpdateMoney(int id_user, float usmoney)
        {
            if (id_user == null) return false;
            if (usmoney == null) return false;
            
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateMoney";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@usmoney", usmoney);
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@usmoney", SqlDbType.Float);
            SqlParameter f2 = new SqlParameter("@id_user", SqlDbType.Int);
            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
            return true;
        }
        public String doBuyItem(int id_user, int id_tovar)
        {
            if (id_user == null) return "";
            if (id_tovar == null) return "";
            int to_user = 0;
            int chena = 0;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "FromTovar";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_tovar", id_tovar);
            SqlParameter f1 = new SqlParameter("@id_tovar", SqlDbType.Int);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();

            while (myReader.Read())
            {
                to_user = myReader.GetInt32(0);
                chena= myReader.GetInt32(1);
                 }
             myReader.Close();
             Conn.Close();
             if (id_user == 0) return "0";
             //доходит до этого момента
             float balance_ot = this.CheckBalancee(id_user);
             float balance_to = this.CheckBalancee(to_user);
             if (balance_ot > chena)
             {
                 balance_ot -= chena;
                 this.doUpdateMoney(id_user, balance_ot);
                 balance_to += chena;
                 this.doUpdateMoney(to_user, balance_to);
                  Conn = null;
                 Conn = new SqlConnection(@DataControll.StringConnect());
                 strSQL = "AddBuy";
                  Cmd = new SqlCommand(strSQL, Conn);
                 Cmd.CommandType = CommandType.StoredProcedure;
                 Cmd.Parameters.Add("@id_user", id_user);
                 Cmd.Parameters.Add("@id_tovar", id_tovar);
                  f1 = new SqlParameter("@id_user", SqlDbType.Int);
                 SqlParameter f2 = new SqlParameter("@id_tovar", SqlDbType.Int);
                 Conn.Open();
                 Cmd.ExecuteNonQuery();
                 Conn.Close();
                 return "1";
             } else {
                 return "0";
             }
             return "";
        }

        public string getBuyItem(int user_id)
        {
            if (user_id == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "getBuyItems";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@user_id", user_id);
            SqlParameter f1 = new SqlParameter("@user_id", SqlDbType.Int);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            object tmp;
            while (myReader.Read())
            {


                tmp = myReader.GetValue(2);

               
                result=result + "<a href='../../files/"+ Convert.ToString(myReader.GetValue(3))  + "'> " +Convert.ToString(myReader.GetValue(2)) +  "</a><br> ";
            } 


            myReader.Close();


            Conn.Close();

            return result;
        }
        public String getProfile(int user_id)
        {
            if (user_id == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "doGetProfile";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@user_id", user_id);
            SqlParameter f1 = new SqlParameter("@user_id", SqlDbType.Int);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            object tmp;
            while (myReader.Read())
            {
                String comment_count = Convert.ToString(myReader.GetValue(1));
                String items_count = Convert.ToString(myReader.GetValue(0));
                String icq = myReader.GetValue(2).ToString();
                String twitter = myReader.GetValue(3).ToString();
                String skype = myReader.GetValue(4).ToString();
                String username = myReader.GetValue(5).ToString();
                String money = Convert.ToString(myReader.GetValue(6));
                result = "<h2>" + username + "</h2>Опубликовал товаров " + items_count + "<br>Написал комментариев " + comment_count + "<br> Рейтинг " + money + "<br>ICQ" + icq + "<br> twitter:" + twitter + "<br>skype:" + skype;   

            }


            myReader.Close();


            Conn.Close();

            return result;
           
        }
        public String doEditProfile(int id_user, String icq, String twitter, String skype)
        {
            if (id_user == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateContact";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@icq", icq);
            Cmd.Parameters.Add("@twitter", twitter);
            Cmd.Parameters.Add("@skype", skype);
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter parametrs = new SqlParameter("@icq", SqlDbType.NChar);
            parametrs = new SqlParameter("@twitter", SqlDbType.NChar);
            parametrs = new SqlParameter("@skype", SqlDbType.NChar);
                         parametrs = new SqlParameter("@id_user", SqlDbType.Int);
            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            


            myReader.Close();


            Conn.Close();

            return "1";
        }
        public String getContact(int id_user)
        {

            if (id_user == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetUser";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.Int);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            
            while (myReader.Read())
            {
               
                String icq = myReader.GetValue(4).ToString();
                String twitter = myReader.GetValue(6).ToString();
                String skype = myReader.GetValue(5).ToString();
              
                result = "<div id='error'></div><br><form action='#' method='post' onsubmit='doContactSave();return false;'>ICQ<br><input type=text value='"+icq+"' id='icq'><br>Skype<br><input type=text value='"+skype+"' id='skype'><br>Twitter<br>http://twitter.com/<input type=text value='"+twitter+"' id='twitter'><br><input type=submit value='Обновить'></form>";

            }


            myReader.Close();


            Conn.Close();

            return result;
            
        }
        


    }
}