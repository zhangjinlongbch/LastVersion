using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssayBashBoard.Models;

namespace EssayBashBoard.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            DBHelper db = new DBHelper();
            //var uid = Session["UserId"].ToString();
            var uid = "18964985806";
            ViewBag.CommentList = db.GetCommentListByUid(uid);
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult Create(string uid,string title)
        {
            DBHelper db = new DBHelper();
            //var uid = Session["UserId"].ToString();
            uid = "18964985806";
            title = "123";
            //**************************************************************
            ViewBag.CommentList = db.GetCommentListByTitle(title);
            ViewBag.Essay = db.GetEssyByUid_Title(uid,title);
            ViewBag.Uid = uid;
            ViewBag.Title = title;
            //**************************************************************
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(string content)
        {
            DBHelper db = new DBHelper();
            var uid = "18964985806";
            var title = "123";
            //**************************************************************
            ViewBag.Essay = db.GetEssyByUid_Title(uid, title);
            try
            {
                // TODO: Add insert logic here
                
                db.SqlExcute(CommentCreateSql(content));
                ViewBag.CommentList = db.GetCommentListByTitle(title);
                Response.Write("<script>alert('评论成功！')</script>");
                return View();
            }
            catch
            {
                return View();
            }
        }
        private string CommentCreateSql(string content)
        {
            //session["UserId"]
            var uid = "12345678912";
            //session["Title"]
            var title = "123";
            string CommandText = "Insert into Comment(UserId,Title,CommentContent)values('"+uid+"','"+title+"','"+content+"')";
            return CommandText;
        }
        public ActionResult Delete(string uid,string title)
        {
            try
            {
                // TODO: Add delete logic here
                DBHelper db = new DBHelper();
                var DelStr = CommentDelStr(uid, title);
                db.SqlExcute(DelStr);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private string CommentDelStr(string uid,string title)
        {
            return "delete from Comment where UserId = '"+uid+"' and Title ='"+title+"'";
        }
    }
}
