using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace code_shop.Models
{
    public class Item
    {
        public string title { get; set; }
        public string description { get; set; }
        public string screenshot { get; set; }
        public int id_category{get;set;}
        public int id_item {get;set;}
        public int value { get; set; }
        public string filename{get;set;}
        public  Boolean doAddItem(String title,String description,String screenshot,int id_category,int id_user,int value,String file)
        {
            if (title == null) return false;
            if (description == null) return false;
            if (screenshot == null) return false;
            if (id_category == null) return false;
            if (id_user == null) return false;
            if (value == null) return false;
            if (file == null) return false;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "addTovar";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            Cmd.Parameters.Add("@title", title);
            Cmd.Parameters.Add("@description", description);
            Cmd.Parameters.Add("@screen", screenshot);
            Cmd.Parameters.Add("@chena", value);
            Cmd.Parameters.Add("@files", file);
            Cmd.Parameters.Add("@id_cat", id_category);




            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.Int);
            SqlParameter f2 = new SqlParameter("@title", SqlDbType.NVarChar);
            SqlParameter f3 = new SqlParameter("@description", SqlDbType.Text);
            SqlParameter f4 = new SqlParameter("@screen", SqlDbType.NVarChar);
            SqlParameter f5 = new SqlParameter("@chena", SqlDbType.Int);
            SqlParameter f6 = new SqlParameter("@files", SqlDbType.NVarChar);
            SqlParameter f7 = new SqlParameter("@id_cat", SqlDbType.Int);
            
           

            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();

        
       

            return false;
        }
        public Boolean doEditItem(int id_item,String title,String description,String screenshot,String file)
        {
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateTovar";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_tovar", id_item);
            Cmd.Parameters.Add("@title", title);
            Cmd.Parameters.Add("@description", description);
            Cmd.Parameters.Add("@screen", screenshot);
            Cmd.Parameters.Add("@chena", value);
            Cmd.Parameters.Add("@files", file);
            Cmd.Parameters.Add("@id_cat", id_category);




            SqlParameter f1 = new SqlParameter("@id_tovar", SqlDbType.Int);
            SqlParameter f2 = new SqlParameter("@title", SqlDbType.NVarChar);
            SqlParameter f3 = new SqlParameter("@description", SqlDbType.Text);
            SqlParameter f4 = new SqlParameter("@screen", SqlDbType.NVarChar);
            SqlParameter f5 = new SqlParameter("@chena", SqlDbType.Int);
            SqlParameter f6 = new SqlParameter("@files", SqlDbType.NVarChar);
            SqlParameter f7 = new SqlParameter("@id_cat", SqlDbType.Int);



            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();

            return true;
        }
        public Boolean doRemoveItem(int id_item){
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "DeleteTovar";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_tovar", id_item);
            SqlParameter f1 = new SqlParameter("@id_tovar", SqlDbType.Int);
       



            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();
            return true;
        }
        public string getItem(int id_item){
            if (id_item == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetTovarFromID_TOVAR";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_tovar", id_item);
            SqlParameter f1 = new SqlParameter("@id_tovar", SqlDbType.Int);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            object tmp;
            while (myReader.Read())
            {


                tmp = myReader.GetValue(2);
                result = Convert.ToString(tmp);
                result = "" + Convert.ToString(myReader.GetValue(2)) + "<br> Описание <br>" + Convert.ToString(myReader.GetValue(3)) + "<br> Изображение <img src='" + Convert.ToString(myReader.GetValue(4)) + "' width=150px height=150px><br><a href='#' onclick='doPlus(" + myReader.GetValue(1).ToString() + ")'>Понравился </a> &nbsp;&nbsp;<a href='#' onclick='doMinus(" + myReader.GetValue(1).ToString() + ")'>Не Понравился</a><br><a href='#' onclick='doBuy(" + myReader.GetValue(0).ToString() + ")'>Купить товар</a><br><a href='#' onclick='getProfile(" + Convert.ToString(myReader.GetValue(1)) + ")'>Профиль пользователя</a><hr>"; 

            }


            myReader.Close();


            Conn.Close();

            return result;
        }
        public string getItemcat(int id_item)
        {
            if (id_item == null) return "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetTovarFromID_TOVAR";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_tovar", id_item);
            SqlParameter f1 = new SqlParameter("@id_tovar", SqlDbType.Int);


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            String result = "";
            object tmp;
            while (myReader.Read())
            {


                tmp = myReader.GetValue(2);
                result = Convert.ToString(tmp);
                result = "<a href='#' onclick='getItem(" + myReader.GetInt32(0).ToString() + ");return false;'> " + Convert.ToString(myReader.GetValue(2)) + "</a><br>";

            }


            myReader.Close();


            Conn.Close();

            return result;
        }
        
    }
}