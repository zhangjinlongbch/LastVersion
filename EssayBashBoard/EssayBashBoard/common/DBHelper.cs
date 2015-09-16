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
        private SqlConnection con;
        public DBHelper()
        {
           string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["BCH_MVC_SQL"].ConnectionString;
           con = new SqlConnection(ConStr);
        }
        //执行Excute操作并返回受影响的行数
        public int SqlExcute( string Cmd)
        {
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

        public int SqlReader(string CmdText)
        {
            var i = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand(CmdText, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
            }
            con.Close();
            return i;
        }
        //返回文章列表
        public List<Essay> GetEssayList()
        {
            List<Essay> EssayList = new List<Essay>();
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

        public void UpdateEssay(string uid,string old_title, string old_content, string new_title, string new_content)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Essay set EssayTitle = '" + new_title + "', EssayContent = '" + new_content + "'" +
                " where UserID = '" + uid + "' and EssayTitle = '" + old_title + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //根据用户名返回评论列表
        public List<CommentsViewModels> GetCommentListByUid(string uid)
        {
            List<CommentsViewModels> commentsList = new List<CommentsViewModels>();

            con.Open();
            string cmdText = "select * from Comment where UserId = '" + uid + "'";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CommentsViewModels comment = new CommentsViewModels();
                comment.UserId = uid;
                comment.title = reader["Title"].ToString();
                comment.Content = reader["CommentContent"].ToString();
                commentsList.Add(comment);
            }
            con.Close();
            return commentsList;
        }
        //根据文章名返回评论列表
        public List<CommentsViewModels> GetCommentListByTitle(string Title)
        {
            List<CommentsViewModels> commentsList = new List<CommentsViewModels>();

            con.Open();
            string cmdText = "select * from Comment where Title = '" + Title + "'";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CommentsViewModels comment = new CommentsViewModels();
                comment.UserId = reader["UserId"].ToString();
                comment.title = Title;
                comment.Content = reader["CommentContent"].ToString();
                commentsList.Add(comment);
            }
            con.Close();
            return commentsList;
        }
        //根据用户名和标题获取日志
        public Essay GetEssyByUid_Title(string uid, string title)
        {
            con.Open();
            Essay essay = new Essay();
            string cmdText = "select * from Essay where UserId = '" + uid + "'and EssayTitle = '" + title + "'";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                essay.UserID = uid;
                essay.Title = title;
                essay.Content = reader["EssayContent"].ToString();
            }
            con.Close();
            return essay;
        }

    }
}