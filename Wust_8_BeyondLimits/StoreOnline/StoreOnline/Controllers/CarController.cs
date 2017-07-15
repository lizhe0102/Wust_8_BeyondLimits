using NLog;
using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnline.Controllers
{
    public class CarController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        //logger.Info(() => "record logger info");
        //logger.Log(LogLevel.Warn, "warning");

        public ActionResult HasPay()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HasPay(int start = 0, int length = 10)
        {
            List<Order> Return;
            List<Order> o = Data.Data.GetData().GetOrderData().
                Where(i => i.UserName == HttpContext.Session["User"].ToString() && i.IsBuy == true && i.HasPay == true).ToList();
            if ((start + length) > o.Count)
            {
                Return = o.GetRange(start, o.Count - start);
            }
            else
            {
                Return = o.GetRange(start, length);
            }
            string htmlDelete;
            string htmlSelect;
            foreach (var item in Return)
            {
                htmlSelect = "<input  type=\"checkbox\" name=\"SelectedBuy\" value=\"" + item.No + "\"/>";
                htmlDelete = "<a href=\"/Car/CarDelete?id=" + item.No + "\">删除</a>";

                item.Delete = htmlDelete;
                item.Select = htmlSelect;
            }
            ReturnOrderData ROD = new ReturnOrderData();
            ROD.data = Return;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(ROD);
        }


        public ActionResult CarShow()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CarShow(int start = 0, int length=10)
        {
            List<Order> Return;
            List<Order> o = Data.Data.GetData().GetOrderData().
                Where(i => i.UserName == HttpContext.Session["User"].ToString()&&i.IsBuy==true&&i.HasPay==false).ToList();
            if ((start+length)>o.Count)
            {
                Return = o.GetRange(start, o.Count - start);
            }
            else
            {
                Return = o.GetRange(start, length);
            }
            string htmlDelete;
            string htmlBuy;
            string htmlSelect;
            foreach (var item in Return)
            {
                htmlSelect = "<input  type=\"checkbox\" name=\"SelectedBuy\" value=\""+item.No+"\"/>";
                htmlDelete = "<a href=\"/Car/CarDelete?id=" + item.No + "\">删除</a>";
                htmlBuy = "<a href=\" /Car/CarBuy?id=" + item.No + "\">购买</a>";
               
                item.Delete = htmlDelete;
                item.Buy = htmlBuy;
                item.Select = htmlSelect;
            }
            ReturnOrderData ROD = new ReturnOrderData();
            ROD.data = Return;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(ROD);
        }

        public ActionResult Attention()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Attention(int start = 0, int length = 10)
        {
            List<Order> Return;
            List<Order> o = Data.Data.GetData().GetOrderData().
                Where(i => i.UserName == HttpContext.Session["User"].ToString() && i.IsBuy == false&&i.HasPay==false).ToList();
            if ((start + length) > o.Count)
            {
                Return = o.GetRange(start, o.Count - start);
            }
            else
            {
                Return = o.GetRange(start, length);
            }

            string htmlDelete;
            string htmlBuy;
            string htmlSelect;
            foreach (var item in Return)
            {
                htmlSelect = "<input  type=\"checkbox\" name=\"SelectedBuy\" value=\"" + item.No + "\"/>";
                htmlDelete = "<a href=\"/Car/AttentionDelete?id=" + item.No + "\">删除</a>";
                htmlBuy = "<a href=\"/Car/AttentionBuy?id=" + item.No + "\">购买</a>";
                item.Delete = htmlDelete;
                item.Buy = htmlBuy;
                item.Select = htmlSelect;
            }
            ReturnOrderData ROD = new ReturnOrderData();
            ROD.data = Return;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(ROD);
        }

        [HttpPost]
        public ActionResult BuyBook(string id,int num)
        {
            ReturnMessage rm = new ReturnMessage();
            Order o = new Order();
            Book book = Data.Data.GetData().GetBook().Where(i => i.Id == id).First();

            //找出Order表中序号最大项
            if(HttpContext.Session["User"]!=null)
            {
                if(Data.Data.GetData().GetOrderData().Count!=0)
                {
                    o.No = Data.Data.GetData().GetOrderData().Max(i => i.No) + 1;
                }
                else
                {
                    o.No = 1;
                }
                //找到书名
                o.BookName = Data.Data.GetData().GetBook().Where(item => item.Id == id).First().BookName;

                rm.flag = true;
                rm.Message = "成功添加至购物车!";
                o.BookId = id;
                o.sum = num;
                o.Time = DateTime.Now.ToShortTimeString();
                o.IsBuy = true;
                o.HasPay = false;
                o.Price = num * book.Price;
                o.UserName = HttpContext.Session["User"].ToString();
                Data.Data.GetData().GetOrderData().Add(o);
            }
            else
            {
                rm.flag = false;
                rm.Message = "您还未登陆，请先登录！";
            }
            
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);
        }

        public ActionResult AddToAttention(string id)
        {
            ReturnMessage rm = new ReturnMessage();
            Order o = new Order();
            Book book = Data.Data.GetData().GetBook().Where(i => i.Id == id).First();

            //找出Order表中序号最大项
            if (HttpContext.Session["User"] != null)
            {
                if (Data.Data.GetData().GetOrderData().Count != 0)
                {
                    o.No = Data.Data.GetData().GetOrderData().Max(i => i.No) + 1;
                }
                else
                {
                    o.No = 1;
                }
                //找到书名
                o.BookName = Data.Data.GetData().GetBook().Where(item => item.Id == id).First().BookName;
                rm.flag = false;
                rm.Message = "成功关注!";
                o.BookId = id;
                o.Time = DateTime.Now.ToShortTimeString();
                o.IsBuy = false;
                o.HasPay = false;
                o.UserName = HttpContext.Session["User"].ToString();
                Data.Data.GetData().GetOrderData().Add(o);
            }
            else
            {
                rm.flag = false;
                rm.Message = "您还未登陆，请先登录！";
            }

            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(rm);
        }

        public ActionResult CarDelete(int id)
        {
            Order o = Data.Data.GetData().GetOrderData().Where(item => item.No == id).First();
            Data.Data.GetData().GetOrderData().Remove(o);
            return RedirectToAction("CarShow");
        }

        
        public ActionResult CarBuy(int id)
        {
            Data.Data.GetData().GetOrderData().Where(item => item.No == id).First().HasPay = true;
            return RedirectToAction("CarShow");
        }

        public ActionResult AttentionDelete(int id)
        {
            Order o = Data.Data.GetData().GetOrderData().Where(item => item.No == id).First();
            Data.Data.GetData().GetOrderData().Remove(o);
            return RedirectToAction("CarShow");
        }
        
        public ActionResult AttentionBuy(int id)
        {
            Data.Data.GetData().GetOrderData().Where(item => item.No == id).First().IsBuy = true;
            return RedirectToAction("Attention");
        }

        [HttpPost]
        public ActionResult AddToPay()
        {
            Order o;
            string[] Selected = Request.Params.GetValues("SelectedBuy");
            if(Selected!=null)
            {
                foreach (var item in Selected)
                {
                    o = Data.Data.GetData().GetOrderData().Where(i => i.No.ToString() == item).First();
                    o.HasPay = true;
                }
            }
            return RedirectToAction("CarShow");
        }

        [HttpPost]
        public ActionResult HasBuyDelete()
        {
            Order o;
            string[] Selected = Request.Params.GetValues("SelectedBuy");
            if (Selected != null)
            {
                foreach (var item in Selected)
                {
                    o = Data.Data.GetData().GetOrderData().Where(i => i.No.ToString() == item).First();
                    Data.Data.GetData().GetOrderData().Remove(o);
                }
            }
                
            return RedirectToAction("HasPay");
        }

        [HttpPost]
        public ActionResult AddToCar()
        {
            Order o;
            string[] Selected = Request.Params.GetValues("SelectedBuy");
            if (Selected != null)
            {
                foreach (var item in Selected)
                {
                    o = Data.Data.GetData().GetOrderData().Where(i => i.No.ToString() == item).First();
                    o.IsBuy = true;
                }
            }
               
            return RedirectToAction("Attention");
        }
    }
}