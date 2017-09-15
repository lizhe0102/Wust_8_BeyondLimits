using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnline.Data;
using System.Security.Cryptography;

namespace StoreOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }


        /// <summary>
        /// 修改用户信息（该函数未完成）
        /// </summary>
        /// <param name="u">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UsreAndAdress u)
        {
            ReturnMessage rm = new ReturnMessage();
            rm.Message = "修改成功！";
            return Json(rm);
        }


        /// <summary>
        /// 为获取用户信息提供服务
        /// </summary>
        /// <returns>用户信息的json串</returns>
        [HttpPost]
        public ActionResult GetUserAndAdress()
        {
            if(HttpContext.Session["User"]!=null)
            {
                UsreAndAdress data = new UsreAndAdress();
                User user = Data.Data.GetData().GetUserData().
                    Where(item => item.UserName.Equals(HttpContext.Session["User"].ToString())).First();
                data.UserName = user.UserName;
                data.Password = user.Password;
                data.Email = user.Email;
                //获取地址
                if(Data.Data.GetData().GetAdressData(). 
                    Where(item => item.UserName.Equals(HttpContext.Session["User"].ToString())).ToList().Count>0)
                {
                    Adress a = Data.Data.GetData().GetAdressData().
                   Where(item => item.UserName.Equals(HttpContext.Session["User"].ToString())).First();
                    data.Tel = a.Tel;
                    data.Adr1 = a.Adr1;
                    data.Adr2 = a.Adr2;
                }
                else
                {
                    data.Tel = "您还未提交电话号码！";
                    data.Adr1 = "您还未提交过地址！";
                    data.Adr2 = "您还未提交过地址！";
                }
                HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                return Json(data);
            }
            else
            {
                bool data = false;
                HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                return Json(data);
            }
           
        }


        /// <summary>
        /// 为注销动作提供服务
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            if (HttpContext.Session["User"] != null)
            {
                HttpContext.Session["User"] = null;
            }
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// 为注册动作提供服务
        /// </summary>
        /// <param name="user">用户，包括用户名，密码等等</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User user, string code)
        {
            ReturnMessage rm = new ReturnMessage();
            rm.flag = false;
            string ErrorMessage = "";
            if (HttpContext.Session["Code"] != null && HttpContext.Session["Code"].ToString().ToLower() == code)
            {
                if (Data.Data.GetData().GetUserData().Where(i => i.UserName.Equals(user.UserName)).ToList().Count > 0)
                {
                    ErrorMessage = "用户已存在，请重新新输入！";
                }
                else
                {
                    string hashPassword = Encipher.Encrypt(HashAlgorithm.Create(),user.Password);
                    user.Password = hashPassword;
                    user.Entry = Admit.VIP;
                    HttpContext.Session["User"] = user.UserName;
                    HttpContext.Session["Entry"] = Enum.GetName(typeof(Admit), user.Entry);
                    Data.Data.GetData().GetUserData().Add(user);
                    rm.flag = true;
                    rm.Message = "/Home/Index";
                }
            }
            else
            {
                ErrorMessage = "验证码错误,请重新新输入！";
            }
            if (!rm.flag) rm.Message = ErrorMessage;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);

        }

        /// <summary>
        /// 未登录动作提供服务
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string UserName, string Password, string code)
        {
            ReturnMessage rm = new ReturnMessage();
            rm.flag = false;
            if(HttpContext.Session["User"]!=null&& HttpContext.Session["User"].ToString()==UserName)
            {
                rm.Message = "您已登录，不能重复登陆！";
            }
            //验证码是否通过
            else if (HttpContext.Session["Code"] != null && HttpContext.Session["Code"].ToString().ToLower() == code)
            {
                User user = new Models.User() { UserName="",Password=""};
                //查看用户是否存在
                foreach (var u in Data.Data.GetData().GetUserData())
                {
                    if (u.UserName == UserName)
                    {
                        user.UserName = u.UserName;
                        user.Password = u.Password;
                        user.Entry = u.Entry;
                        break;
                    }
                }
                //判断用户和密码是否正确
                if (user.UserName.Equals(UserName) && Encipher.IsHashMatch(HashAlgorithm.Create(),user.Password,Password))
                {
                    HttpContext.Session["User"] = user.UserName;
                    HttpContext.Session["Entry"] = Enum.GetName(typeof(Admit),user.Entry);
                    rm.flag = true;
                    rm.Message = "/Home/Index";
                }
                else
                {
                    rm.Message = "用户名或者密码错误！";
                }
            }
            else
            {
                rm.Message = "验证码错误！";
            }
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult  Show()
        {
            return View();
        }
        

        /// <summary>
        /// 提供验证码函数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCode()
        {
            int width = 75; //ConverterHelper.ObjToInt(Request.Params["width"], 100);
            int height = 35; //ConverterHelper.ObjToInt(Request.Params["height"], 40);
            int fontsize = 20; //ConverterHelper.ObjToInt(Request.Params["fontsize"], 20);
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, width, height, fontsize);
            HttpContext.Session["Code"] = code.ToLower();
            return File(bytes, @"image/jpeg");
            //Random a = new Random();
            //string b = null;
            //for (int i = 0; i < 4; i++)
            //{
            //    b = b + a.Next(0, 10);
            //}
            //HttpContext.Session["Code"] = b;
            //HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            //return Json(b);
        }
    }
}