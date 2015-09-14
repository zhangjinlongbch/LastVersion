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
        public int SqlExcute(string CmdText)
        {
            var i = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand(CmdText,con);
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int SqlReader(string CmdText)
        {
            var i = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand(CmdText,con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
            }
            con.Close();
            return i;
        }
    }
}