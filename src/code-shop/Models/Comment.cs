using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace code_shop.Models
{
    public class Comment
    {
        public string text { get; set; }
        public int id_user { get; set; }
        public int id_item { get; set; }
        public int id_comment { get; set; }
        public Boolean doAddComment(int id_user,int id_item,string text)
        {
            if (id_user == null) return false;
            if (id_item == null) return false;
            if (text == null) return false;
            if (text == "") return false;

            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "addComment";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_user", id_user);
            Cmd.Parameters.Add("@id_tovar", id_item);
            Cmd.Parameters.Add("@texts", text);





            SqlParameter f1 = new SqlParameter("@id_user", SqlDbType.Int);
            SqlParameter f2 = new SqlParameter("@id_tovar", SqlDbType.Int);
            SqlParameter f3 = new SqlParameter("@texts", SqlDbType.Text);

            

            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();
            return true;
        }
        public Boolean doEditComment(int id_comment,string text)
        {
            if (id_comment == null) return false;
            if (text == null) return false;
            if (text == "") return false;
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "UpdateComment";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_comments", id_comment);
            Cmd.Parameters.Add("@texts", text);





            SqlParameter f1 = new SqlParameter("@id_comments", SqlDbType.Int);
        
            SqlParameter f3 = new SqlParameter("@texts", SqlDbType.Text);



            Conn.Open();
            Cmd.ExecuteNonQuery();

            Conn.Close();
            return true;
            
        }
        public String getComment(int id_item)
        {
            if (id_item == null) return "";
            String result = "";
            SqlConnection Conn = null;
            Conn = new SqlConnection(@DataControll.StringConnect());
            string strSQL = "GetCommentsByIDTOVAR";
            SqlCommand Cmd = new SqlCommand(strSQL, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@id_tovar", id_item);

            SqlParameter f0 = new SqlParameter("@id_tovar", SqlDbType.Int);



            Conn.Open();
            SqlDataReader myReader = Cmd.ExecuteReader();
            int id_tovar = 0;
            while (myReader.Read())
            {


                id_tovar = myReader.GetInt32(0);
                String id_user = Convert.ToString(myReader.GetValue(1));
                Models.User usr=new Models.User();
                String username=usr.getLogin(myReader.GetInt32(1));
                id_tovar = 0;
                result = result + "<a href='#' onclick='getProfile("+id_user+")'><b> " + username + "</b></a> :" + myReader.GetString(3) + "<br>"; 


            }

            Conn.Close();
            return result;
        }
    }
}