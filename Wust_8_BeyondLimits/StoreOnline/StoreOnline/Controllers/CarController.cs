using NLog;
using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnline.Controllers
{
    /// <summary>
    /// 购物车控制器
    /// </summary>
    public class CarController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        //logger.Info(() => "record logger info");
        //logger.Log(LogLevel.Warn, "warning");

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
        public ActionResult HasPay()
        {
            return View();
        }

        /// <summary>
        /// 为已付款页面翻页动作提供服务
        /// </summary>
        /// <param name="start">起始项</param>
        /// <param name="length">每页长度</param>
        /// <returns></returns>
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
        

        /// <summary>
        /// 为购物车显示   翻页动作提供服务
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 为关注页面的翻页动作提供服务
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 为买书动作提供服务
        /// </summary>
        /// <param name="id">输的id</param>
        /// <param name="num">购买数量</param>
        /// <returns></returns>
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


        /// <summary>
        /// 为将书籍加入“关注”提供服务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 从购物车删除书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarDelete(int id)
        {
            Order o = Data.Data.GetData().GetOrderData().Where(item => item.No == id).First();
            Data.Data.GetData().GetOrderData().Remove(o);
            return RedirectToAction("CarShow");
        }

        /// <summary>
        /// 从购物车买书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarBuy(int id)
        {
            Data.Data.GetData().GetOrderData().Where(item => item.No == id).First().HasPay = true;
            return RedirectToAction("CarShow");
        }


        /// <summary>
        /// 从关注页面删除书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AttentionDelete(int id)
        {
            Order o = Data.Data.GetData().GetOrderData().Where(item => item.No == id).First();
            Data.Data.GetData().GetOrderData().Remove(o);
            return RedirectToAction("CarShow");
        }
        
        /// <summary>
        /// 从关注页面买书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AttentionBuy(int id)
        {
            Data.Data.GetData().GetOrderData().Where(item => item.No == id).First().IsBuy = true;
            return RedirectToAction("Attention");
        }


        /// <summary>
        /// 批量由关主页将书籍将书籍加入待付款页
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 批量删除已经付款页面的书籍
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 批量将书籍从关注页加入待付款页面
        /// </summary>
        /// <returns></returns>
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