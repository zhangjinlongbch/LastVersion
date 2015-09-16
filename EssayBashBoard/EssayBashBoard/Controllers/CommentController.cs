using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssayBashBoard.Models;
using EssayBashBoard.common;

namespace EssayBashBoard.Controllers
{
    public class CommentController : Controller
    {
        static string essay_uid;
        static string essay_title;
            
        // GET: Comment
        public ActionResult Index()
        {
            DBHelper db = new DBHelper();
            var uid = Session["UserId"].ToString();
            ViewBag.CommentList = db.GetCommentListByUid(uid);
            return View();
        }
        public ActionResult Create(string uid, string title)
        {
            DBHelper db = new DBHelper();
            ViewBag.Essay = db.GetEssyByUid_Title(uid, title);
            ViewBag.CommentList = db.GetCommentListByTitle(title);
            essay_uid = uid;
            essay_title = title;
            return View();
            //**************************************************************

        }

        [HttpPost]
        // POST: Comment/Create
        public ActionResult Create(string content)
        {
            DBHelper db = new DBHelper();
            var uid = Session["UserId"].ToString();
            ViewBag.Essay = db.GetEssyByUid_Title(essay_uid, essay_title);

            //**************************************************************
            try
            {
                // TODO: Add insert logic here
                db.SqlExcute(CommentCreateSql(essay_title, content));
                ViewBag.CommentList = db.GetCommentListByTitle(essay_title);
                Response.Write("<script>alert('评论成功！')</script>");
                return View();
            }
            catch
            {
                return View();
            }
        }
        private string CommentCreateSql(string title,string content)
        {
            var uid = Session["UserId"].ToString();
            string CommandText = "Insert into Comment(UserId,Title,CommentContent)values('" + uid + "','" + title + "','" + content + "')";
            return CommandText;
        }
        public ActionResult Delete(string uid, string title, string content)
        {

         // TODO: Add delete logic here
         DBHelper db = new DBHelper();
         var DelStr = CommentDelStr(uid, title, content);
         db.SqlExcute(DelStr);
         return RedirectToAction("Index");
        }
        private string CommentDelStr(string uid, string title, string content)
        {
            return "delete from Comment where UserId = '" + uid + "' and Title ='" + title + "' and CommentContent like '" + content+"'";
        }
    }
}
