using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EssayBashBoard.Models;

namespace EssayBashBoard.common
{
    public class DBHelper
    {
        private string ConStr;
        public DBHelper()
        {
            ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["BCH_MVC_SQL"].ConnectionString;
        }
        //执行Excute操作并返回受影响的行数
        public int SqlExcute( string Cmd)
        {
            SqlConnection con = new SqlConnection(ConStr);
            var number = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand(Cmd, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number++;
            }
            con.Close();
            return number;
        }
        //查询是否存在用户
        public bool UserExist(string uid)
        {
            bool IsExist = false;
            string cmd = "select * from UserAccount where UserId = '"+uid+"'";
            IsExist = (SqlExcute(cmd) != 0) ? true : false;
            return IsExist;
        }
        //判断用户名密码
        public bool UserCorrect(string uid , string pwd)
        {
            bool IsCorrect = false;
            string cmd = "select * from UserAccount where UserId = '" + uid + "' and Pwd = '"+pwd+"'";
            IsCorrect = (SqlExcute(cmd) != 0) ? true : false;
            return IsCorrect;
        }
        //返回文章列表
        public List<Essay> GetEssayList()
        {
            List<Essay> EssayList = new List<Essay>();
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Essay", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Essay essay = new Essay();
                essay.UserID = reader["UserId"].ToString();
                essay.Title = reader["EssayTitle"].ToString();
                essay.Content = reader["EssayContent"].ToString();
                EssayList.Add(essay);
            }
            con.Close();
            return EssayList;
        }
        //返回个人文章列表
        public List<Essay> GetEssayList(string uid)
        {
            List<Essay> EssayList = new List<Essay>();
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Essay where UserId = '"+uid+"'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Essay essay = new Essay();
                essay.UserID = reader["UserId"].ToString();
                essay.Title = reader["EssayTitle"].ToString();
                essay.Content = reader["EssayContent"].ToString();
                EssayList.Add(essay);
            }
            con.Close();
            return EssayList;
        }
        //返回个人评论列表
        public List<Comment> GetCommentList(string uid)
        {
            List<Comment> CommentList = new List<Comment>();
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Essay where UserId = '"+uid+"'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Comment comment = new Comment();
                comment.UserID = reader["UserId"].ToString();
                comment.Content = reader["CommentContent"].ToString();
                comment.CurrentTime = reader["CurrentTime"].ToString();
                CommentList.Add(comment);
            }
            con.Close();
            return CommentList;
        }

        public void UpdateEssay(string uid,string old_title, string old_content, string new_title, string new_content)
        {
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("update Essay set EssayTitle = '" + new_title + "', EssayContent = '" + new_content + "'" +
                " where UserID = '" + uid + "' and EssayTitle = '" + old_title + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}