using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnline.Data;

namespace StoreOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user,string code)
        {
            ReturnMessage rm = new ReturnMessage();
            rm.flag = false;
            string ErrorMessage = "";
            if (HttpContext.Session["Code"] != null && HttpContext.Session["Code"].ToString() == code)
            {
                if ( Data.Data.GetData().GetUserData().Where(i => i.UserName.Equals(user.UserName)).ToList().Count>0)
                {
                    ErrorMessage = "用户已存在，请重新新输入！";
                }
                else
                {
                    HttpContext.Session["User"] = user.UserName;
                    Data.Data.GetData().GetUserData().Add(user);
                    rm.flag = true;
                    rm.Message = "/User/Index";
                }
            }
            else
            {
                ErrorMessage = "验证码重复,请重新新输入！";
            }
            if (!rm.flag) rm.Message = ErrorMessage;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);
            
        }
        [HttpPost]
        public ActionResult Login(string UserName,string Password,string code)
        {
            
            ReturnMessage rm = new ReturnMessage();
            rm.flag = false;
            rm.Message = UserName + Password + code;
            //验证码是否通过
            if(HttpContext.Session["Code"]!=null&&HttpContext.Session["Code"].ToString()==code)
            {
                User user = new Models.User();
                //查看用户是否存在
               foreach(var u in Data.Data.GetData().GetUserData())
                {
                    if (u.UserName == UserName)
                    {
                        user.UserName = u.UserName;
                        user.Password = u.Password;
                        break;
                    }
                }
                if(user.UserName.Equals(UserName) &&user.Password.Equals(Password))
                {
                    HttpContext.Session["User"] = user.UserName;
                    rm.flag = true;
                    rm.Message = "/User/Index";
                }
                else
                {
                    rm.Message = "yong户名或者密码错误！";
                }
            }
            else
            {
                rm.Message = "验证码错误！";
            }
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);
        }
        
        public ActionResult GetCode(string code)
        {
            Random a = new Random();
            string b = null;
            for(int i=0;i<4;i++)
            {
                b=b + a.Next(0, 10);
            }
            HttpContext.Session["Code"] = b;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(b);
        }
    }
}
