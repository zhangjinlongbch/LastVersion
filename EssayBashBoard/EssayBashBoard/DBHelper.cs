using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EssayBashBoard.Models;

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
            SqlCommand cmd = new SqlCommand(CmdText, con);
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
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
        //根据用户名返回评论列表
        public List<CommentsViewModels> GetCommentListByUid(string uid)
        {
            List<CommentsViewModels> commentsList = new List<CommentsViewModels>();
            
            con.Open();
            string cmdText = "select * from Comment where UserId = '"+uid+"'";
            SqlCommand cmd = new SqlCommand(cmdText,con);
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
        public EssayViewModels GetEssyByUid_Title(string uid,string title)
        {
            con.Open();
            EssayViewModels essay = new EssayViewModels();
            string cmdText = "select * from Essay where UserId = '" + uid + "'and EssayTitle = '"+title+"'";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                essay.UserId = uid;
                essay.Title = title;
                essay.Content = reader["EssayContent"].ToString();
            }
            con.Close();
            return essay;
        }

    }
}