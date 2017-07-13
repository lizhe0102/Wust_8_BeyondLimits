﻿using StoreOnline.Models;
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

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            if (HttpContext.Session["User"] != null)
            {
                HttpContext.Session["User"] = null;
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Register(User user, string code)
        {
            ReturnMessage rm = new ReturnMessage();
            rm.flag = false;
            string ErrorMessage = "";
            if (HttpContext.Session["Code"] != null && HttpContext.Session["Code"].ToString() == code)
            {
                if (Data.Data.GetData().GetUserData().Where(i => i.UserName.Equals(user.UserName)).ToList().Count > 0)
                {
                    ErrorMessage = "用户已存在，请重新新输入！";
                }
                else
                {
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
                ErrorMessage = "验证码重复,请重新新输入！";
            }
            if (!rm.flag) rm.Message = ErrorMessage;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);

        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password, string code)
        {

            ReturnMessage rm = new ReturnMessage();
            rm.flag = false;
            if (HttpContext.Session["User"] != null && HttpContext.Session["User"].ToString() == UserName)
            {
                rm.Message = "您已登录，不能从新登陆！";
            }
            //验证码是否通过
            else if (HttpContext.Session["Code"] != null && HttpContext.Session["Code"].ToString() == code)
            {
                User user = new Models.User();
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
                if (user.UserName.Equals(UserName) && user.Password.Equals(Password))
                {
                    HttpContext.Session["User"] = user.UserName;
                    HttpContext.Session["Entry"] = Enum.GetName(typeof(Admit), user.Entry);
                    rm.flag = true;
                    rm.Message = "/Home/Index";
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

        public ActionResult Show()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UsreAndAdress UA)
        {
            return View();
        }

        public ActionResult GetCode(string code)
        {
            Random a = new Random();
            string b = null;
            for (int i = 0; i < 4; i++)
            {
                b = b + a.Next(0, 10);
            }
            HttpContext.Session["Code"] = b;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(b);
        }
    }
}