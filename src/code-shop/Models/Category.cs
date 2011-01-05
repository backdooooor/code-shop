using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace code_shop.Models
{
    public class Category
    {
        public string title { get; set; }
        public int id_category { get; set; }
        public Boolean doAddCategory(string title)
        {
            if (title == null) return false;
            if (title == "") return false;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "AddCategory";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@title", title);
            SqlParameter f1 = new SqlParameter("@title", SqlDbType.Text);
           

            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();



            return true;
        }
        public Boolean doEditCategory(string title, int id_category)
        {
            if (id_category == null) return false;
            if (title == null) return false;
            if (title == "") return false;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateCategory";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_cat", id_category);
            Cmd.Parameters.Add("@title", title);
            SqlParameter f0 = new SqlParameter("@id_cat", SqlDbType.Int);
            SqlParameter f1 = new SqlParameter("@title", SqlDbType.Text);


            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();



            return true;
        }
        //получение товаров из категории, взаимодействие с  моделью Item
        public String doListCategory()
        {
            String result = "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "ListCategory";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
        



            Conn.Open();

            SqlDataReader myReader = Cmd.ExecuteReader();
            
            String title = "";
            int id_cat = 0;
           
            while (myReader.Read())
            {


                id_cat = myReader.GetInt32(1);
                title = myReader.GetString(0);
                result = result + "<a href='#' onclick='getCategory(" + id_cat.ToString() + ")'>" + title + "</a><br>";


            }
            Conn.Close();

            return result;
        }
        public string getCategory(int id_category)
        {
            if (id_category == null) return "";
            String result = "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetTovarfromcat";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_cat", id_category);
            
            SqlParameter f0 = new SqlParameter("@id_cat", SqlDbType.Int);
           


            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            int id_tovar = 0;
            while (myReader.Read())
            {


                id_tovar = myReader.GetInt32(0);
                Models.Item it = new Models.Item();
                result = result + " " + it.getItemcat(id_tovar);


            }

            Conn.Close();
            return  result;
        }
        public String doSelectCategory()
        {
            String result = "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "ListCategory";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;




            Conn.Open();

            SqlDataReader myReader = Cmd.ExecuteReader();

            String title = "";
            int id_cat = 0;

            while (myReader.Read())
            {


                id_cat = myReader.GetInt32(1);
                title = myReader.GetString(0);
                result = result + "<option value='" + id_cat.ToString() + "'>" + title + "</option>";


            }
            Conn.Close();

            return result;
        }
    }
}