using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssayBashBoard.Models;
using EssayBashBoard.common;

namespace EssayBashBoard.Controllers
{
    public class EssayController : Controller
    {

        public ActionResult CreateEssay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEssay(Essay model)
        {
            Session["UserId"] = "123";
            DBHelper tmpDBHelper = new DBHelper();
            var title = model.Title;
            var content = model.Content;
            var userID = Session["UserId"].ToString();

            tmpDBHelper.SqlExcute("insert into Essay(UserId,EssayTitle,EssayContent) values('" + userID + "','" + title + "','" + content + "')");
            return Redirect("../Home/Index");
        }

        public ActionResult ManageEssay()
        {
            Session["UserId"] = "123";
            var uid = Session["UserId"].ToString();
            List<Essay> EssayList = GetEssayList(uid);
            ViewBag.list = EssayList;

            return View();
        }
        public ActionResult UpdateEssay(string title, string content)
        {
            ViewBag.title = title;
            ViewBag.content = content;
            Session["CurrentTitle"] = title;
            Session["CurrentContent"] = content;

            return View();
        }

        [HttpPost]
        public ActionResult UpdateEssay(Essay model)
        {
            DBHelper tmpDBHelper = new DBHelper();
            var uid = Session["UserId"].ToString();
            var new_title = model.Title;
            var new_content = model.Content;
            string old_title = (string)Session["CurrentTitle"];
            string old_content = (string)Session["CurrentContent"];

            tmpDBHelper.UpdateEssay(uid, old_title, old_content, new_title, new_content);

            return View();
        }

        public ActionResult DeleteEssay(string uid, string title)
        {
            DBHelper tmpDBHelper = new DBHelper();
            tmpDBHelper.SqlExcute("delete from Essay where UserID = '" + uid + "'and EssayTitle = '" + title + "'");

            return Redirect("ManageEssay");
        }
        public ActionResult Detail(string title, string content)
        {

            ViewBag.title = title;
            ViewBag.content = content;
            return View();
        }
       
        public List<Essay> GetEssayList(string uid)
        {
            DBHelper db = new DBHelper();
            return db.GetEssayList(uid);
            ///////
        }
    }
}