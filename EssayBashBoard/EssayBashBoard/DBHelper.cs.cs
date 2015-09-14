using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EssayBashBoard
{
    public class DBHelper
    {
        private SqlConnection con;
        public DBHelper()
        {
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
            con = new SqlConnection(ConStr);
        }
        //执行SqlExcute方法，并返回受影响的行数
        public int SqlExcute(string CommandText)
        {
            int n = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand(CommandText,con);
            n = cmd.ExecuteNonQuery();
            con.Close();
            return n;
        }
    }
}