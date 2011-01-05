using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace code_shop.Models
{
    public class DataControll
    {
        public static String StringConnect()
        {
            return @"Data Source=.\SQLEXPRESS;AttachDbFilename=H:\code-shop\code-shop\App_Data\codeshop.mdf;Integrated Security=True;User Instance=True";
        }
    }
}